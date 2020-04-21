using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FindPath.Core
{
    public class FindPathSolver
    {
        private int[] starts;
        private int[] targets;
        private Node head;
        private HashSet<VisitedNode> visited = new HashSet<VisitedNode>();

        public int MaxDistance { get; private set; }
        private string[] OptNames = new[] { "", "+1", "-1", "x2", "/2", "^2", "^2", "^3" };

        // 反向搜索，从Target 找到Start.
        // 好处是防止平方立方溢出，且如果开方等操作会产出小数，则可以不进行开方操作，适当剪枝。
        public void Solve(int[] starts, int[] targets, Model model)
        {
            this.starts = starts;
            this.targets = targets;
            int distance = 0;
            MaxDistance = 0;
            visited.Clear();
            head = null;

            Node pre = null;
            for (int i = 0; i < targets.Length; i++)
            {
                var node = new Node(pre, targets[0], Operation.None, 0);
                pre = node;
            }

            Queue<Node> q = new Queue<Node>();
            q.Enqueue(pre);
            while (q.Count != 0)
            {
                distance++;
                int count = q.Count;
                for (int i = 0; i < count; i++)
                {
                    Node top = q.Dequeue();
                    int targetIndex = (top.TargetParent + 1) % targets.Length;
                    Node sameTargetNode = top;
                    while (sameTargetNode != null && sameTargetNode.TargetParent != targetIndex)
                    {
                        sameTargetNode = sameTargetNode.Parent;
                    }

                    var preNumber = targets[targetIndex];
                    if (sameTargetNode != null)
                    {
                        preNumber = sameTargetNode.Value;
                    }
                    else
                    {
                        ;
                    }

                    foreach (var op in Enum.GetValues(typeof(Operation)))
                    {
                        Operation opt = (Operation)op;
                        if (model.HasFlag(Model.NoRepeat) &&
                            (top.Opt == opt ||
                             top.Opt == Operation.Square1 && opt == Operation.Square2 ||
                             top.Opt == Operation.Square2 && opt == Operation.Square1))
                        {
                            continue;
                        }

                        if (!CanAntiOperate(preNumber, opt))
                        {
                            continue;
                        }

                        var num = AntiOperate(preNumber, opt);

                        //var visitedNode = new VisitedNode(num, targetIndex, (int)opt);
                        //if (visited.Contains(visitedNode))
                        //{
                        //    continue;
                        //}

                        //visited.Add(visitedNode);

                        var node = new Node(top, num, opt, targetIndex);
                        if ((!model.HasFlag(Model.Disturb) && num == starts[targetIndex]) ||
                            model.HasFlag(Model.Disturb) && num == starts[targetIndex] + distance)
                        {
                            node.IsReached = true;
                            if (top.IsReached)
                            {
                                MaxDistance = distance;
                                head = node;
                                return;
                            }
                        }

                        q.Enqueue(node);
                        //Console.Out.WriteLine($"dist = {distance},target = {targets[targetIndex]},preNumber = {preNumber}, path = {Output(node)}, ans = {node.Value}");
                    }
                }
            }
        }

        public string Output()
        {
            return Output(head);
        }

        public string Output(Node node)
        {
            StringBuilder sb = new StringBuilder();
            Node cur = node;
            while (cur != null)
            {
                if (cur.Opt != Operation.None)
                {
                    sb.Append(" " + OptNames[(int)cur.Opt]);
                }
                else
                {
                    break;
                }

                cur = cur.Parent;
            }
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
                case Operation.Square1:
                    number = Utils.IntSqrt(number);
                    break;
                case Operation.Square2:
                    number = -Utils.IntSqrt(number);
                    break;
                case Operation.Cube:
                    number = Utils.IntCubeRoot(number);
                    break;
            }

            return number;
        }

        /// 是否能执行反射操作
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
                case Operation.Square1:
                case Operation.Square2:
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