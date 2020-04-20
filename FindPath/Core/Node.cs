namespace FindPath.Core
{
    public class Node
    {
        public Node Parent;
        public int Value;
        public Operation Opt;
        public int Distance;

        public Node(Node parent, int value, Operation operation, int distance)
        {
            Parent = parent;
            Value = value;
            Opt = operation;
            Distance = distance;
        }
    }
}