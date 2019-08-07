using System;
namespace Library.Math
{
    public static partial class Functions
    {
        public static long Gcd(long a, long b)
        {
            if (a < b)
            {
                var tmp = a;
                a = b;
                b = tmp;
            }

            long ret = 1;
            while (b > 0)
            {
                ret = b;
                b = a % b;
                a = ret;
            }

            return ret;
        }

        public static long Lcm(long a, long b)
            => a / Gcd(a, b) * b;
    }
}
