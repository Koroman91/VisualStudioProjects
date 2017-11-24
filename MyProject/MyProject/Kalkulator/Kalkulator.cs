using System;

namespace MyProject.Kalkulator
{
    class Kalkulator
    {
        static void Main()
        {
            int num1, num2;
            int add, sub, mul, rem;
            float div, div1;

            Console.Write("Enter the first number\t\t");
            num1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("\n\nEnter the second number\t\t");
            num2 = Convert.ToInt32(Console.ReadLine());

            add = num1 + num2;

            sub = num1 - num2;

            mul = num1 * num2;

            div = num1 / num2;

            rem = num1 % num2;

            div1 = (float)num1 / num2;

            Console.WriteLine("Addition\t\t{0}", add);
            Console.WriteLine("Substraction\t\t{0}", sub);
            Console.WriteLine("Multiplication\t\t{0}", mul);
            Console.WriteLine("Division\t\t{0}", div);
            Console.WriteLine("Reminder\t\t{0}", rem);
            Console.WriteLine("Division1\t\t{0}", div1);

            Console.ReadLine();
        }
    }
}
