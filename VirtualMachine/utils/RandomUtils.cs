using System;

namespace VirtualMachine.utils
{
    public class RandomUtils
    {
        private static Random rand = new Random();

        public static uint NextUint(uint u)                                 //  0 <= x <= u
        {
            uint x;
            if (u < int.MaxValue) return (uint)rand.Next((int)u + 1);
            do                                         
            {
                do x = (uint)rand.Next(1 << 30) << 2;
                while (x > u);
                x |= (uint)rand.Next(1 << 2);
            }
            while (x > u);
            return x;
        }

        public static uint NextUint(uint u0, uint u1)                      // set the range
        {
            return u0 < u1 ? u0 + NextUint(u1 - u0) : u1 + NextUint(u0 - u1);
        }
    }
}