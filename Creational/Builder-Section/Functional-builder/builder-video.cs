// using System.Security.Cryptography.X509Certificates;
// using System.Text;
// /*Important hint in this case, we are doing it like this because we are taking the scenario where we can't inherit*/
// public class Person
// {
//     public string Name, Position;
// }
// //What we will do here is to preserve a list of mutating functions which will affect our person
// public sealed class PersonBuilder
// {
//     private readonly List<Func<Person, Person/*to make it fluent*/>> actions
//         = new List<Func<Person, Person>>();
//     //Then now we are specifyung a persons name in here 
//     public PersonBuilder Called(string name) 
//         => Do(p => p.Name = name);
//     public PersonBuilder Position(string position)
//         => Do(p => p.Position = position); 

//     public PersonBuilder Do(Action<Person> action) //this method is created to no expose AddAction directly
//         => AddAction(action);
//     //Then we can add an action to a person    
//     private PersonBuilder AddAction(Action<Person> action)
//     {
//         actions.Add(p => {action(p);
//         return p;
//         });
//         return this;
//     }

//     public Person Build()
//     //Basicaly what we do here is to create a new person, then assign the function(action) to that person
//      => actions.Aggregate(new Person(), (p,f) => f(p)); //Aggregate will just compact the list into a single application
// }

// static class Program
// {
//     public static void Main(string[] args)
//     {
//         var person = new PersonBuilder()
//             .Called("Cesar")
//             .Position("stripper")
//             .Build();
//         Console.WriteLine(person.Name);
//         Console.WriteLine(person.Position);

//     }
// }