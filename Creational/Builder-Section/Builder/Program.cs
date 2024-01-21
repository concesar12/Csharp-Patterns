using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

/*The idea of this pattern is to provide a way to build and object, in this case is a HTML Builder,
with that, we will be able to add all the tags and append to the object with no trouble.

This pattern is useful when:

The construction of an object is a complex process with many steps.
The object can have multiple variations or configurations.
You want to ensure that the constructed object is consistent and follows a specific construction process.
By using the Builder pattern, you can create objects with different configurations without exposing the details of the construction process to the client code. 
It also allows you to add new types of builders without modifying the client code, making it more flexible and maintainable.*/

//TODO -> Recreate the Builder, and the Fluent build for it

namespace Builder
{

  class HtmlElement
  {
    public string Name, Text;
    public List<HtmlElement> Elements = new List<HtmlElement>();
    private const int indentSize = 2;

    public HtmlElement()
    {
      
    }

    public HtmlElement(string name, string text)
    {
      Name = name;
      Text = text;
    }

    private string ToStringImpl(int indent)
    {
      var sb = new StringBuilder();
      var i = new string(' ', indentSize * indent);
      sb.Append($"{i}<{Name}>\n");
      if (!string.IsNullOrWhiteSpace(Text))
      {
        sb.Append(new string(' ', indentSize * (indent + 1)));
        sb.Append(Text);
        sb.Append("\n");
      }

      foreach (var e in Elements)
        sb.Append(e.ToStringImpl(indent + 1));

      sb.Append($"{i}</{Name}>\n");
      return sb.ToString();
    }

    public override string ToString()
    {
      return ToStringImpl(0);
    }
  }

  class HtmlBuilder
  {
    private readonly string rootName;

    public HtmlBuilder(string rootName)
    {
      this.rootName = rootName;
      root.Name = rootName;
    }

    // not fluent
    public void AddChild(string childName, string childText)
    {
      var e = new HtmlElement(childName, childText);
      root.Elements.Add(e);
    }

    public HtmlBuilder AddChildFluent(string childName, string childText)
    {
      var e = new HtmlElement(childName, childText);
      root.Elements.Add(e);
      //This specific this is used to create the fluent interface in this way we are returning our object (HtmlBuilder)
      return this;
    }

    public override string ToString()
    {
      return root.ToString();
    }

    public void Clear()
    {
      root = new HtmlElement{Name = rootName};
    }

    HtmlElement root = new HtmlElement();
  }

  public class Demo
  {
    static void Main(string[] args)
    {
      // if you want to build a simple HTML paragraph using StringBuilder
      // var hello = "hello";
      var sb = new StringBuilder();
      // sb.Append("<p>");
      // sb.Append(hello);
      // sb.Append("</p>");
      // WriteLine(sb);

      // // now I want an HTML list with 2 words in it
      // var words = new[] {"hello", "world"};
      // sb.Clear();
      // sb.Append("<ul>");
      // foreach (var word in words)
      // {
      //   sb.AppendFormat("<li>{0}</li>", word);
      // }
      // sb.Append("</ul>");
      // WriteLine(sb);

//------------Here is where the actual pattern is applied--------

      // ordinary non-fluent builder
      // builder.AddChild("li", "hello");
      // builder.AddChild("li", "world");
      // WriteLine(builder.ToString());

      // // fluent builder -> with fluent we just return the object straight 
      // sb.Clear();
      // builder.Clear(); // disengage builder from the object it's building, then...
      var builder = new HtmlBuilder("ul").AddChildFluent("li", "hello").AddChildFluent("li", "world");
      WriteLine(builder);
    }
  }
}