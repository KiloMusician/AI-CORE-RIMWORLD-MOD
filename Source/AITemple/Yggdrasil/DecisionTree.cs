using System;
using System.Collections.Generic;

namespace Source.AITemple.Yggdrasil
{
    public class DecisionTree
    {
        private DecisionNode rootNode;

        public DecisionTree()
        {
            // Initialize the decision tree with a more complex root decision
            rootNode = new DecisionNode(
                () => Console.WriteLine("Evaluating root decision."),
                new List<DecisionNode>
                {
                    new DecisionNode(() => Console.WriteLine("Performing action for condition A")),
                    new DecisionNode(() => Console.WriteLine("Performing action for condition B"))
                }
            );
        }

        public void MakeDecision()
        {
            rootNode.Evaluate();
        }
    }

    public class DecisionNode
    {
        private Action action;
        private List<DecisionNode> children;

        // Optional: A condition to evaluate which child node to proceed with
        public Func<bool> Condition { get; set; }

        public DecisionNode(Action action, List<DecisionNode> children = null)
        {
            this.action = action;
            this.children = children ?? new List<DecisionNode>();
        }

        public void Evaluate()
        {
            action.Invoke();

            // Example condition evaluation: proceed with the first child if the condition is true
            if (Condition != null && Condition.Invoke() && children.Count > 0)
            {
                children[0].Evaluate();
            }
            else if (children.Count > 1)
            {
                // If there's no condition or it's false, proceed with the second child
                children[1].Evaluate();
            }
        }

        // Method to add a child node
        public void AddChild(DecisionNode node)
        {
            children.Add(node);
        }
    }
}
