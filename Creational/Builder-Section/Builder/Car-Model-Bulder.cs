//TODO- THIS WILL BE AN EXTRA CLASS
/*The idea is to build and make work this example of fluent interface and Builder method

public class CarBuilder
{
    private string make;
    private string model;
    private int year;

    public CarBuilder SetMake(string make)
    {
        this.make = make;
        return this;
    }

    public CarBuilder SetModel(string model)
    {
        this.model = model;
        return this;
    }

    public CarBuilder SetYear(int year)
    {
        this.year = year;
        return this;
    }

    public Car Build()
    {
        return new Car { Make = make, Model = model, Year = year };
    }
}

public class Car
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
}

Car myCar = new CarBuilder()
    .SetMake("Toyota")
    .SetModel("Camry")
    .SetYear(2022)
    .Build();

*/