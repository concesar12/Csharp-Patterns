using System.Threading;
using System.Windows;
using static System.Console;

namespace weak_event
{

// an event subscription can lead to a memory
  // leak if you hold on to it past the object's
  // lifetime

  // weak events help with this
  //TODO- print when the event is actually erased when assigned to null and print Is window alive after GC? False

  public class Button
  {
    public event EventHandler Clicked;

    public void Fire()
    {
      Clicked?.Invoke(this, EventArgs.Empty);
    }
  }

  public class Window
  {
    public Window(Button button)
    {
      button.Clicked += ButtonOnClicked;
    }

    private void ButtonOnClicked(object sender, EventArgs eventArgs)
    {
      WriteLine("Button clicked (Window handler)");
    }

    ~Window()
    {
      WriteLine("Window finalized");
    }
  }

  public class Window2
  {
    public Window2(Button button)
    {
        /*Weak Event MAnager is coming from dotnet framework and used in WPF
        It is basically a way to handle the Garbage collector*/
        //  WeakEventManager<Button, EventArgs>
        // .AddHandler(button, "Clicked", ButtonOnClicked);
     button.Clicked += ButtonOnClicked;
    }

    private void ButtonOnClicked(object sender, EventArgs eventArgs)
    {
      WriteLine("Button clicked (Window2 handler)");
    }

    ~Window2()
    {
      WriteLine("Window2 finalized");
    }
  }

  public class Demo
  {
    static void Main(string[] args)
    {
      var btn = new Button();
      var window = new Window2(btn);
      //var window = new Window(btn);
      var windowRef = new WeakReference(window);
      btn.Fire();

      WriteLine("Setting window to null");
      window = null;

      FireGC();
      WriteLine($"Is window alive after GC? {windowRef.IsAlive}");

      btn.Fire();

      WriteLine("Setting button to null");
      btn = null;

      FireGC();
    }

    private static void FireGC()
    {
      WriteLine("Starting GC");
      GC.Collect();
      GC.WaitForPendingFinalizers();
      GC.Collect();
      WriteLine("GC is done!");
    }
  }
}