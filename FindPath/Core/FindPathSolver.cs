using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace FindPath.Core
{
    public class FindPathSolver
    {
        private int start;
        private int target;
        private Node tail;
        private HashSet<int> visited = new HashSet<int>();

        public int MaxDistance { get; private set; }

        public bool Solve(int start, int target)
        {
            this.start = start;
            this.target = target;
            int distance = 0;
            MaxDistance = 0;
            visited.Clear();
            tail = null;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(new Node(null, start, -1, 0));

            while (q.Count != 0)
            {
                distance++;
                Node top = q.Dequeue();
                for (int i = 0; i < 6; i++)
                {
                    var num = Choose(top.Value, i);
                    if (visited.Contains(num))
                    {
                        continue;
                    }

                    visited.Add(num);
                    var node = new Node(top, num, i, distance);
                    if (num == target)
                    {
                        MaxDistance = distance;
                        tail = node;
                        return true;
                    }
                    q.Enqueue(node);
                }
            }

            return false;
        }

        public string Output()
        {
            Stack<int> path = new Stack<int>();
            Node cur = tail;
            while (cur != null)
            {
                if (cur.Operation != -1)
                {
                    path.Push(cur.Operation);
                }
                cur = cur.Parent;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(start);
            foreach (var opt in path)
            {
                sb.Insert(0, "(");
                switch (opt)
                {
                    case 0:
                        sb.Append("+1");
                        break;
                    case 1:
                        sb.Append("-1");
                        break;
                    case 2:
                        sb.Append("x2");
                        break;
                    case 3:
                        sb.Append("/2");
                        break;
                    case 4:
                        sb.Append("^2");
                        break;
                    case 5:
                        sb.Append("^3");
                        break;
                }
                sb.Append(")");
            }

            sb.Append("=" + target);
            return sb.ToString();
        }

        private int Choose(int number, int operation)
        {
            switch (operation)
            {
                case 0:
                    number++;
                    break;
                case 1:
                    number--;
                    break;
                case 2:
                    number *= 2;
                    break;
                case 3:
                    number /= 2;
                    break;
                case 4:
                    number *= number;
                    break;
                case 5:
                    number = number * number * number;
                    break;
            }

            return number;
        }
    }

    public class Node
    {
        public Node Parent;
        public int Value;
        public int Operation;
        public int Distance;

        public Node(Node parent, int value, int operation, int distance)
        {
            Parent = parent;
            Value = value;
            Operation = operation;
            Distance = distance;
        }
    }
}