using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyProject5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please Enter a Number ");
            int no = int.Parse(Console.ReadLine());
            int count = 0;
            int count1 = 0;

            while (count < no)
            {
                Console.Write("Number : {0} \n", count);
                count++;
            }

            do
            {
                Console.Write("Number : {0} \n", count1);
                count++;
            }
            while (count1 < no);

            Console.ReadKey();
        }
    }
}
