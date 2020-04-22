namespace FindPath.Core
{
    public class Node
    {
        public Node Parent;
        public int Value;
        public Operation Opt;
        public int TargetParent;

        public Node(Node parent, int value, Operation operation, int targetParent)
        {
            Parent = parent;
            Value = value;
            Opt = operation;
            TargetParent = targetParent;
        }
    }
}