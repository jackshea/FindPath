using System;
using System.Collections.Generic;
using System.Text;

namespace FindPath.Core
{
    public class FindPathSolver
    {
        private int start;
        private int target;
        private Node head;
        private HashSet<int> visited = new HashSet<int>();

        public int MaxDistance { get; private set; }
        private string[] OptNames = new[] { "", "+1", "-1", "x2", "/2", "^2", "^3" };

        // 反向搜索，从Target 找到Start.
        // 好处是防止平方立方溢出，且如果开方等操作会产出小数，则可以不进行开方操作，适当剪枝。
        public void Solve(int start, int target, Model model)
        {
            this.start = start;
            this.target = target;
            int distance = 0;
            MaxDistance = 0;
            visited.Clear();
            head = null;
            Queue<Node> q = new Queue<Node>();

            q.Enqueue(new Node(null, target, Operation.None, 0));

            while (q.Count != 0)
            {
                distance++;
                Node top = q.Dequeue();
                foreach (var op in Enum.GetValues(typeof(Operation)))
                {
                    Operation opt = (Operation)op;
                    if (model.HasFlag(Model.NoRepeat) && top.Opt == opt)
                    {
                        continue;
                    }

                    if (!CanAntiOperate(top.Value, opt))
                    {
                        continue;
                    }

                    var num = AntiOperate(top.Value, opt);

                    if (visited.Contains(num))
                    {
                        continue;
                    }

                    visited.Add(num);
                    var node = new Node(top, num, opt, distance);
                    if ((!model.HasFlag(Model.Disturb) && num == start) ||
                        model.HasFlag(Model.Disturb) && num == start + distance)
                    {
                        MaxDistance = distance;
                        head = node;
                        return;
                    }
                    q.Enqueue(node);
                }
            }
        }

        public string Output()
        {
            StringBuilder sb = new StringBuilder();
            Node cur = head;
            sb.Append(start);
            while (cur != null)
            {
                if (cur.Opt != Operation.None)
                {
                    sb.Append(" " + OptNames[(int)cur.Opt]);
                }

                cur = cur.Parent;
            }
            sb.Append("=" + target);
            return sb.ToString();
        }

        /// 执行反向操作
        private int AntiOperate(int number, Operation opt)
        {
            switch (opt)
            {
                case Operation.Increase:
                    number--;
                    break;
                case Operation.Decrease:
                    number++;
                    break;
                case Operation.Double:
                    number /= 2;
                    break;
                case Operation.Half:
                    number *= 2;
                    break;
                case Operation.Square:
                    number = Utils.IntSqrt(number);
                    break;
                case Operation.Cube:
                    number = Utils.IntCubeRoot(number);
                    break;
            }

            return number;
        }

        private bool CanAntiOperate(int number, Operation opt)
        {
            switch (opt)
            {
                case Operation.Increase:
                    return number > int.MinValue;
                case Operation.Decrease:
                    return number < int.MaxValue;
                case Operation.Double:
                    //number /= 2;
                    return number % 2 == 0;
                case Operation.Half:
                    //number *= 2;
                    return number < int.MaxValue / 2 && number > int.MinValue / 2;
                case Operation.Square:
                    if (number < 0)
                    {
                        return false;
                    }
                    int sqrt = Utils.IntSqrt(number);
                    return sqrt * sqrt == number;
                case Operation.Cube:
                    int cubeRoot = Utils.IntCubeRoot(number);
                    return cubeRoot * cubeRoot * cubeRoot == number;
            }

            return false;
        }

    }
}