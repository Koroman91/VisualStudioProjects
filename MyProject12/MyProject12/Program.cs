using System;

namespace MyProject12
{
    class Polygon
    {
        public int width, height;
        public void setValues(int a, int b)
        {
            this.width = a;
            this.height = b;
        }
    }
    class Rectangle : Polygon
    {
        public int area()
        {
            return width * height;
        }
    }
    class Triangle : Polygon
    {
        public int area()
        {
            return width * height/2;
        }
    }
    class Program
    {
        public static void Main()
        {
            Rectangle rect = new Rectangle();
            Triangle trig = new Triangle();
            rect.setValues(10,20);
            trig.setValues(30,20);

            Console.WriteLine("Rectangle area = {0}", rect.area());
            Console.WriteLine("Triangle area = {0}", trig.area());
            Console.ReadKey();
        }
    }
}
