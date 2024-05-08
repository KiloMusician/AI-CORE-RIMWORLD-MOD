import requests
import argparse
import logging
from concurrent.futures import ThreadPoolExecutor

def setup_logger():
    """Set up logging configuration."""
    logging.basicConfig(level=logging.INFO, format='%(asctime)s - %(levelname)s - %(message)s')

def get_repo_contents(user, repo, path="", auth=None):
    """
    Recursively fetches contents of a GitHub repository at a given path.
    Uses ThreadPoolExecutor to manage API requests concurrently.
    """
    url = f"https://api.github.com/repos/{user}/{repo}/contents/{path}"
    headers = {'Authorization': f'token {auth}'} if auth else {}
    files = []

    while url:
        try:
            response = requests.get(url, headers=headers)
            response.raise_for_status()  # Raises stored HTTPError, if one occurred.
        except requests.exceptions.HTTPError as err:
            logging.error(f"HTTP error occurred: {err}")
            return []
        except requests.exceptions.RequestException as err:
            logging.error(f"Request failed: {err}")
            return []

        data = response.json()

        if isinstance(data, dict) and data.get('message') == 'Not Found':
            logging.error(f"Repository {user}/{repo} not found.")
            return []

        with ThreadPoolExecutor() as executor:
            futures = []
            for item in data:
                if item['type'] == 'file':
                    files.append(item['path'])
                elif item['type'] == 'dir':
                    # Recursively get more files
                    futures.append(executor.submit(get_repo_contents, user, repo, item['path'], auth))
            for future in futures:
                files.extend(future.result())

        # Handling pagination
        if 'Link' in response.headers:
            links = response.headers['Link'].split(', ')
            url = None
            for link in links:
                if 'rel="next"' in link:
                    url = link[link.index('<')+1:link.index('>')]
        else:
            url = None

    return files

def parse_args():
    """Parse command line arguments."""
    parser = argparse.ArgumentParser(description='List all files in a GitHub repository.')
    parser.add_argument('user', type=str, help='GitHub username')
    parser.add_argument('repo', type=str, help='GitHub repository name')
    parser.add_argument('--token', type=str, help='GitHub API token for authentication')
    parser.add_argument('--output', type=str, help='Output file to write the list of files')
    return parser.parse_args()

def main():
    args = parse_args()
    setup_logger()
    files = get_repo_contents(args.user, args.repo, auth=args.token)
    if args.output:
        with open(args.output, 'w') as f:
            for file in files:
                f.write(file + '\n')
    else:
        for file in files:
            print(file)

if __name__ == "__main__":
    main()
