using System;

namespace MyProject7
{
    class Program
    {
        static void Main()
        {
            string[] arr = new string[5];

            arr[0] = "Steven";
            arr[1] = "Clark";
            arr[2] = "Mark";
            arr[3] = "Thompson";
            arr[4] = "John";

            foreach(string name in arr)
            {
                Console.Write("Hello " +name+ "!\n");
            }
      

            for (int a = 0; a < 5; a++)
            {
                Console.Write("Number of a: {0}", arr[a] + "!\n");
            }

            Console.ReadKey();
        }
    }
}
