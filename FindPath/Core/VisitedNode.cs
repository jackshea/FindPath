namespace FindPath.Core
{
    public class VisitedNode
    {
        public int Value;
        public int TargetIndex;
        public int Operation;

        public VisitedNode(int value, int targetIndex, int operation)
        {
            Value = value;
            TargetIndex = targetIndex;
            Operation = operation;
        }

        protected bool Equals(VisitedNode other)
        {
            return Value == other.Value && TargetIndex == other.TargetIndex && Operation == other.Operation;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((VisitedNode) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Value;
                hashCode = (hashCode * 10) + TargetIndex;
                hashCode = (hashCode * 10) + Operation;
                return hashCode;
            }
        }
    }
}