﻿using static System.Console;

namespace Liskov_sol;

class Program
{
  // using a classic example: 
  /*The idea of Liskov subsitution is that we should be able to substitute a base type for a sub type, and in this case we have as a base type a rectangle
  This rectangle we will make it a square by creating inheritance 
  to understand: 
  Rectangle has width and height 
  square has all sides same size
  so if I pass a square into the rectangle we should only use one of the parameters and have the same value for all of them */
    //Hint : Main thing is to override the attributes and use virtual attributes to to be overriden
  //Todo -> 
  //1. Create a triangle following the same principle
  //2. Modify exercise to be shape instead of rectangle
  public class Rectangle
  {
    //public int Width { get; set; }
    //public int Height { get; set; }

    public virtual int Width { get; set; }
    public virtual int Height { get; set; }

    public Rectangle()
    {
      
    }

    public Rectangle(int width, int height)
    {
      Width = width;
      Height = height;
    }
    //This to string is nice to use when wanting to generally print something out in our own way
    public override string ToString()
    {
      return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
    }
  }

  public class Square : Rectangle
  {
    //public new int Width
    //{
    //  set { base.Width = base.Height = value; }
    //}

    //public new int Height
    //{ 
    //  set { base.Width = base.Height = value; }
    //}

    public override int Width // nasty side effects
    {
      set { base.Width = base.Height = value; }
    }

    public override int Height
    { 
      set { base.Width = base.Height = value; }
    }
  }

  public class Demo
  {
    static public int Area(Rectangle r) => r.Width * r.Height;

    static void Main(string[] args)
    {
      Rectangle rc = new Rectangle(2,3);
      WriteLine($"{rc} has area {Area(rc)}");

      // should be able to substitute a base type for a subtype
      /*Square*/ Rectangle sq = new Square();
      sq.Width = 4;
      WriteLine($"{sq} has area {Area(sq)}");
    }
  }
}