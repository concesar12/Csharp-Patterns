using System;
using System.Collections.Generic;
using static System.Console;

namespace Liskov
{
    public class Rectangle
    {
        //We will define the width and hight of it (Make properties virtual so they can be modified)
        public virtual int width { get; set; }
        public virtual int height { get; set; }

        //Empty constructor
        public Rectangle()
        {

        }
        //Constructor with params rectangle
        public Rectangle(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        //Override to string to print our values as we please
        public override string ToString()
        {
            return $"{nameof(width)}: {width} , {nameof(height)}: {height}";
        }

    }

    //Create the class for square
    public class Square : Rectangle
    {
        //The reason this is done like this is: we want to set width = height to make it a square
        public override int width
        { 
            set { base.width = base.height = value; } 
        }

        public override int height
        {
            set { base.width = base.height = value; }
        }
    }

    public class Program
    {
        //We made this static to avoid creating a new instance for it
        static public int Area(Rectangle r) => r.width * r.height ;

        static void Main(string[] args)
        {
            //Create a rectangle and calculate the are
            Rectangle r = new Rectangle(2,3);
            WriteLine($"The width is {r.width}, the height is: {r.height} and the Area is: {Area(r)}");

            //Now create square
            Rectangle s = new Square();
            s.width = 6;
            WriteLine($"{s} has area of {Area(s)}");
        }
    }
}