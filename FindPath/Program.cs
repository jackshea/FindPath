using System;
using FindPath.Core;

namespace FindPath
{
    class Program
    {
        static void Main(string[] args)
        {
            var findPathSolver = new FindPathSolver();

            Console.WriteLine("模式： 正常=1，不重复 = 4, 干扰 = 8。不重复且干扰输入12或者4,8");
            Console.WriteLine("多目标用逗号隔开，如 50,-25");

            while (true)
            {
                int inputModel = 0;
                Console.Write("Input models:");
                var line = Console.ReadLine();
                var split = line.Split(',');
                for (int i = 0; i < split.Length; i++)
                {
                    if (string.IsNullOrEmpty(split[i]))
                    {
                        continue;
                    }

                    inputModel |= Convert.ToInt32(split[i]);
                }

                Console.Write("Input starts:");
                line = Console.ReadLine();
                split = line.Split(',');
                int[] starts = new int[split.Length];
                for (int i = 0; i < split.Length; i++)
                {
                    if (string.IsNullOrEmpty(split[i]))
                    {
                        continue;
                    }

                    starts[i] = Convert.ToInt32(split[i]);
                }

                Console.Write("Input targets:");
                line = Console.ReadLine();
                split = line.Split(',');
                int[] targets = new int[split.Length];
                for (int i = 0; i < split.Length; i++)
                {
                    if (string.IsNullOrEmpty(split[i]))
                    {
                        continue;
                    }

                    targets[i] = Convert.ToInt32(split[i]);
                }

                findPathSolver.Solve(starts, targets, (Model)inputModel);
                Console.WriteLine(findPathSolver.Output());
            }

            //findPathSolver.Solve(new[] { 1,1 }, new[] { 512,255 }, Model.Disturb);
            //Console.WriteLine(findPathSolver.Output());
            //Console.ReadLine();
        }
    }
}
