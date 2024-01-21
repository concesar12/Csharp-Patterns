﻿using System;
using System.Collections.Immutable;

/*In this case and giving the meaning of why it is called stepwise is, we want to create a builder that know what is necessary to create first
In this case the example is a car, whe depending on the car type then we can tell what wheel size it will need
so: 
We will use our interface segregation principle which in this case we will use as such: 
one interface for configuring a car type
another interface to build the car wheel size
and last interface to actually building the car
*/

//TODO- delete the impl and move the builder to class car
//Create another condition such as headlightSize and use it

namespace stepwise_builder

{
  public enum CarType
  {
    Sedan,
    Crossover
  };
  public class Car
  {
    public CarType Type;
    public int WheelSize;
  }

  public interface ISpecifyCarType
  {
    public ISpecifyWheelSize OfType(CarType type);
  }

  public interface ISpecifyWheelSize
  {
    public IBuildCar WithWheels(int size);
  }

  public interface IBuildCar
  {
    public Car Build();
  }

  public class CarBuilder
  {
    public static ISpecifyCarType Create()
    {
      return new Impl();
    }
  // The only reason we have this Impl in here is because we don't want to expose the build as public so we keep it as private
  // We could actually put the builder inside of the class car 
    private class Impl : 
      ISpecifyCarType,
      ISpecifyWheelSize,
      IBuildCar
    {
      private Car car = new Car();

      public ISpecifyWheelSize OfType(CarType type)
      {
        car.Type = type;
        return this;
      }

      public IBuildCar WithWheels(int size)
      {
        switch (car.Type)
        {
          case CarType.Crossover when size < 17 || size > 20:
          case CarType.Sedan when size < 15 || size > 17:
            throw new ArgumentException($"Wrong size of wheel for {car.Type}.");
        }
        car.WheelSize = size;
        return this;
      }

      public Car Build()
      {
        return car;
      }
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      var car = CarBuilder.Create() // ISpecifyCarType
        .OfType(CarType.Crossover) // ISpecifyWheelSize
        .WithWheels(18)            // IBuildCar
        .Build();
      Console.WriteLine(car);
    }
  }
}
