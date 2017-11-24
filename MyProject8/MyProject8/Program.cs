using System;


namespace MyProject8
{
    class Program
    {
        public static void Main()
        {
            Program pro8 = new Program();
            pro8.sum();
            Console.ReadKey();
        }

        public void sum()
        {
            int num1 = 345, num2 = 345;
            int add = num1 + num2;
            Console.WriteLine("Addition\t\t{0}", add);
        }
    }
}
