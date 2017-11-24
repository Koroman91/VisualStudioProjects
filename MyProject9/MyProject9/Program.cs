using System;


namespace MyProject9
{
    class Program
    {
        public static void Main()
        {
            int num1 = 0, num2 = 0;

            Console.Write("Enter first number: \t\t");
            num1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter second number: \t\t");
            num2 = Convert.ToInt32(Console.ReadLine());

            Program pro9 = new Program();
            int add = pro9.sum(num1, num2);
            Console.WriteLine("Addition\t\t{0}", add);
            Console.ReadKey();
        }

        public int sum(int num1, int num2)
        {
            int add = num1 + num2;
            return add;
        }
    }
}
