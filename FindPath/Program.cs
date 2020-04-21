using System;
using FindPath.Core;

namespace FindPath
{
    class Program
    {
        static void Main(string[] args)
        {
            var findPathSolver = new FindPathSolver();
            while (true)
            {
                int target = 0;
                Console.Write("Input starts:");
                var line = Console.ReadLine();
                var split = line.Split(',');
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

                findPathSolver.Solve(starts, targets, Model.NoRepeat);
                Console.WriteLine(findPathSolver.Output());
            }

            //findPathSolver.Solve(new[] { 1, 1 }, new[] { 60, -55 }, Model.NoRepeat);
            //Console.WriteLine(findPathSolver.Output());
            //Console.ReadLine();
        }
    }
}
