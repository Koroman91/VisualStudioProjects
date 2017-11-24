using System;

namespace MyProject10
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Sum of two ints is {0}", sum(23, 34));
            Console.WriteLine("Sum of two doubles is {0}", sum(23.23, 34.34));
            Console.WriteLine("Sum of two strings is {0}", sum("Hello ", "Youtube"));

            Console.ReadKey();
        }

        public static int sum(int x, int y)
        {
            int add = x + y;
            return add;
        }
        public static double sum(double x, double y)
        {
            double add = x + y;
            return add;
        }
        public static string sum(string x, string y)
        {
            string add = x + y;
            return add;
        }

    }
}
