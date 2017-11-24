using System;


namespace MyProject4
{
    class Program
    {
        static void Main()
        {
            Console.Write("Please Enter Students marks ");
            int marks = int.Parse(Console.ReadLine());

            switch(marks)
            {
                case 90:
                    Console.Write("Excelent");
                    break;
                case 60:
                    Console.Write("Very good");
                    break;
                case 30:
                    Console.Write("need improvement");
                    break;
                default:
                    Console.Write("Please Enter Valid Value");
                    break;
            }

            Console.ReadKey();
        }
    }
}
