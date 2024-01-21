/*This example of observer we have defined an event handler called FallsIll, which contains an argument that is the street address
The main idea of this is we can use CallDoctor Method and bring the event to be filled up everytime CatchACold is called*/

//TODO- Modify Doctor attending to use a method instead of just reading the value in a println

namespace observer_event
{

public class FallsIllEventArgs
  {
    public string Address;
  }
public class DoctorAttending
{
    public string Name;
}

  public class Person
  {
    public void CatchACold()
    {
      FallsIll?.Invoke(this,
        new FallsIllEventArgs { Address = "123 London Road" }); // We use a safe call "?" in case there is no reference or subscribers to this event
    }

    public void AttendedBy()
    {
        Doctor?.Invoke(this,
         new DoctorAttending { Name = "Dr. Cesar"});
    }

    public event EventHandler<FallsIllEventArgs> FallsIll;
    public event EventHandler<DoctorAttending> Doctor;
  }

  public class Demo
  {
    static void Main()
    {
      var person = new Person();

      person.FallsIll += CallDoctor;
      person.Doctor += DoctorComing;
      //person.Doctor -= DoctorComing; //Doing this will unsubscribe me from that event

      person.CatchACold();
      person.AttendedBy();
      person.CatchACold();
    }

    private static void CallDoctor(object sender, FallsIllEventArgs eventArgs)
    {
      Console.WriteLine($"A doctor has been called to {eventArgs.Address}");
    }
    private static void DoctorComing(object sender, DoctorAttending eventArgs)
    {
      Console.WriteLine($"Patient attended by: {eventArgs.Name}");
    }
  }
}