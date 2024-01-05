using System;
using System.Collections.Generic;
using System.Threading;

/*In this example the idea is that in the previous exercise we did return "this" in order to create the fluent interface
and in that way, be able to chain different calls. However, in the case of inheritance this condition changes, the reason for this is:
we are no longer returning this, but an abstraction of the object, for that reason we create the generic <SELF> which 
indicates that we are actually returning itself provide a fluent type-safe builder*/

//TODO-Create another level of inheritance after born and use it and integrate for the fluent builder.

namespace Fluent_Builder_Inheritance

{
  public class Person
  {
    public string Name;

    public string Position;

    public DateTime DateOfBirth;
    
    public class Builder : PersonBirthDateBuilder<Builder>
    {
      internal Builder() {}
    }
    
    public static Builder New => new Builder();

    public override string ToString()
    {
      return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }
  }

  public abstract class PersonBuilder
  {
    //Reason this is protected is because we are using inheritance
    protected Person person = new Person();

    public Person Build()
    {
      return person;
    }
  }

  public class PersonInfoBuilder<SELF> : PersonBuilder
    where SELF : PersonInfoBuilder<SELF>
  {
    public SELF Called(string name)
    {
      person.Name = name;
      return (SELF) this;
    }
  }

  public class PersonJobBuilder<SELF> 
    : PersonInfoBuilder<PersonJobBuilder<SELF>>
    where SELF : PersonJobBuilder<SELF>
  {
    public SELF WorksAsA(string position)
    {
      person.Position = position;
      return (SELF) this;
    }
  }

  // here's another inheritance level
  // note there's no PersonInfoBuilder<PersonJobBuilder<PersonBirthDateBuilder<SELF>>>!

  public class PersonBirthDateBuilder<SELF> 
    : PersonJobBuilder<PersonBirthDateBuilder<SELF>>
    where SELF : PersonBirthDateBuilder<SELF>
  {
    public SELF Born(DateTime dateOfBirth)
    {
      person.DateOfBirth = dateOfBirth;
      return (SELF)this;
    }
  }

  internal class Program
  {
    class SomeBuilder : PersonBirthDateBuilder<SomeBuilder>
    {

    }

    public static void Main(string[] args)
    {
      var me = Person.New
        .Called("Dmitri")
        .WorksAsA("Quant")
        .Born(DateTime.UtcNow)
        .Build();
      Console.WriteLine(me);
    }
  }
}