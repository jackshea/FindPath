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
                int start = 0;
                int target = 0;
                Console.Write("Input start:");
                start = Convert.ToInt32(Console.ReadLine());
                Console.Write("Input target:");
                target = Convert.ToInt32(Console.ReadLine());
                findPathSolver.Solve(start, target);
                Console.WriteLine(findPathSolver.Output());
            }
        }
    }
}
