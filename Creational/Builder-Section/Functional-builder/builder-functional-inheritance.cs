using System.Security.Cryptography.X509Certificates;
using System.Text;
/*This is the same exercise of builder-video however we are doing now inheriting to create the builder with our functional way*/
public class Person
{
    public string Name, Position;
    public int Income;
}

public abstract class FunctionalBuilder<TSubject, TSelf> // TSelf is here because we need a return type
    where TSelf :  FunctionalBuilder<TSubject, TSelf>
    where TSubject : new()
{
    private readonly List<Func<Person, Person/*to make it fluent*/>> actions
        = new List<Func<Person, Person>>();
    //Then now we are specifyung a persons name in here 
    // public TSelf Called(string name) 
    //     => Do(p => p.Name = name);
    public TSelf Position(string position)
        => Do(p => p.Position = position); 

    public TSelf Do(Action<Person> action) //this method is created to no expose AddAction directly
        => AddAction(action);
    //Then we can add an action to a person    
    private TSelf AddAction(Action<Person> action)
    {
        actions.Add(p => {action(p);
        return p;
        });
        return (TSelf) this;
    }
    public Person Build()
    //Basicaly what we do here is to create a new person, then assign the function(action) to that person
     => actions.Aggregate(new Person(), (p,f) => f(p)); //Aggregate will just compact the list into a single application
}

public sealed class PersonBuilder 
    : FunctionalBuilder<Person, PersonBuilder>
{
    public PersonBuilder Called(string name)
        => Do(p => p.Name = name);    
}

//New class to resect OCP
public static class PersonBuilderExtensions
{
    public static PersonBuilder Salary
        (this PersonBuilder builder, int income)
        => builder.Do(p => p.Income = income);
}

static class Program
{
    public static void Main(string[] args)
    {
        var person = new PersonBuilder()
            .Called("Cesar")
            .Position("stripper")
            .Salary(90000)
            .Build();
        Console.WriteLine(person.Name);
        Console.WriteLine(person.Position);
        Console.WriteLine(person.Income);

    }
}