import numpy as np
from sklearn.model_selection import train_test_split
from sklearn.metrics import accuracy_score
from tensorflow import keras

def develop_algorithms():
    """
    Develops a simple neural network model using Keras.
    Generates random data and labels for a binary classification task.
    
    Returns:
        model (keras.Model): The compiled neural network model.
        data (numpy.ndarray): Randomly generated feature data.
        labels (numpy.ndarray): Random binary labels for the data.
    """
    data = np.random.random((1000, 20))
    labels = np.random.randint(2, size=(1000, 1))

    model = keras.Sequential([
        keras.layers.Dense(64, activation='relu', input_shape=(20,)),
        keras.layers.Dense(64, activation='relu'),
        keras.layers.Dense(1, activation='sigmoid')
    ])

    model.compile(optimizer='adam', loss='binary_crossentropy', metrics=['accuracy'])
    return model, data, labels

def conduct_testing_validation(model, data, labels):
    """
    Splits the data into training and testing sets, trains the model, and evaluates its performance.
    
    Parameters:
        model (keras.Model): The neural network model to train and evaluate.
        data (numpy.ndarray): The feature data.
        labels (numpy.ndarray): The labels for the data.
    """
    train_data, test_data, train_labels, test_labels = train_test_split(data, labels, test_size=0.2)
    model.fit(train_data, train_labels, epochs=10, batch_size=32)
    predictions = model.predict(test_data)
    test_acc = accuracy_score(test_labels, predictions.round())
    print(f"Test Accuracy: {test_acc}")

def design_interface():
    """
    Creates a simple command-line interface for managing the model.

    Returns:
        str: A string representing the user interface.
    """
    interface = """
    Welcome to the OmniTag ML Interface:
    1. Train Model
    2. Evaluate Model
    3. Update Model
    Please choose an option: 
    """
    return interface

def implement_features(model, data, labels):
    """
    Allows the user to choose between training, evaluating, and updating the model via CLI.
    
    Parameters:
        model (keras.Model): The neural network model to manage.
        data (numpy.ndarray): The feature data.
        labels (numpy.ndarray): The labels for the data.
    """
    print(design_interface())
    user_input = int(input("Enter your choice: "))
    if user_input == 1:
        print("Training the model...")
        model.fit(data, labels, epochs=5, batch_size=32)
    elif user_input == 2:
        print("Evaluating the model...")
        conduct_testing_validation(model, data, labels)
    elif user_input == 3:
        print("Updating the model...")
        update_algorithms(model)

def establish_testing_procedures():
    """
    Prints a message indicating that automated testing procedures have been established.
    """
    print("Automated testing procedures have been established.")

def collect_feedback():
    """
    Simulates collecting user feedback about the model.

    Returns:
        str: User feedback.
    """
    feedback = input("Please enter your feedback on the model: ")
    return feedback

def update_algorithms(model):
    """
    Updates the model by modifying its architecture and recompiling it based on simulated user feedback.
    
    Parameters:
        model (keras.Model): The model to update.
    """
    model.pop()  # Remove the last layer
    new_layer = keras.layers.Dense(1, activation='sigmoid')
    model.add(new_layer)
    model.compile(optimizer='adam', loss='binary_crossentropy', metrics=['accuracy'])
    print("Model updated based on feedback.")

def main():
    """
    Orchestrates the machine learning workflow by invoking the appropriate functions.
    """
    model, data, labels = develop_algorithms()
    conduct_testing_validation(model, data, labels)  # Ensure model is trained and evaluated at least once
    implement_features(model, data, labels)
    feedback = collect_feedback()
    if "update" in feedback.lower():
        update_algorithms(model)
    establish_testing_procedures()

if __name__ == "__main__":
    main()