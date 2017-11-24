using System;


namespace MyProject11
{
    class Box
    {
        public double l, b, h;

        public double volume()
        {
            return (this.l * this.b * this.h);
        }
    }
    
    
    
    
    class Program
    {
        public static void Main()
        {
            Box box1 = new Box();
            box1.l = 45;
            box1.b = 10;
            box1.h = 20;

            Console.WriteLine("Volume of box1 is {0}", box1.volume());

            Box box2 = new Box();
            box2.l = 34;
            box2.b = 50;
            box2.h = 50;

            Console.WriteLine("Volume of box1 is {0}", box2.volume());


            Console.ReadKey();
        }
    }
}
