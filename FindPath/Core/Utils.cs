using System;

namespace FindPath.Core
{
    public static class Utils
    {
        /// 快速求整数平方根
        public static int IntSqrt(int x)
        {
            if (x <= 1) return x;
            int lo = 1, hi = x;
            while (lo < hi)
            {
                int mid = lo + (hi - lo + 1) / 2;
                if (mid <= x / mid)
                {
                    lo = mid;
                }
                else
                {
                    hi = mid - 1;
                }
            }
            return lo;
        }

        /// 快速求整数立方根
        public static int IntCubeRoot(int x)
        {
            int sign = Math.Sign(x);
            x = Math.Abs(x);
            if (x <= 1) return x * sign;
            int lo = 1, hi = x;
            while (lo < hi)
            {
                int mid = lo + (hi - lo + 1) / 2;
                if (mid <= x / mid / mid)
                {
                    lo = mid;
                }
                else
                {
                    hi = mid - 1;
                }
            }
            return lo * sign;
        }
    }
}