using System;

namespace MyProject2
{
    class Program
    {
        static void Main(string[] args)
        {
            int num1, num2;
            Console.Write("Enter first number\t\t");
            num1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("\n\nEnter second number\t\t");
            num2 = Convert.ToInt32(Console.ReadLine());


            if(num1 < num2 || num1==20)
            {
                Console.Write("num1 is less then num2 or num1 is 20\t\t");
            }
            else if (num1 == num2)
            {
                Console.Write("num1 is equal num2\t\t");
            }
            else
            {
                Console.Write("num1 is greater then num2\t\t");
            }


            Console.ReadLine();
        }
    }
}
