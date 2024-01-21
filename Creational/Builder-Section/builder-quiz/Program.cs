/*Considerations for coding exercise
1. The root of the code chain looks is "public class"
2. After the root is given with the name of the parameter then a curly bracket is given next
3. Looks like root is always public.
4. only if the in add field we add the name and the type
5. after each add field we place a semicolon ";"
6. we finish with a curly braces

Algorithm
1. string root
2. dictionary field
3. class CodeBuilder 

root = "public"


*/

using System.Text;

namespace builder_quiz

{
    public class Field
    {
        public string Type, Name;

        public override string ToString()
        {
            return $"public {Type} {Name}";
        }
    }

    public class Class
    {
        public string ClassName;
        public List<Field> fields = new List<Field>();

        public Class()
        {

        }

        public override string ToString()
        {
            var sb = new StringBuilder ();
            sb.AppendLine($"public class {ClassName}").AppendLine("{");
            foreach (var f in fields)
            {
                sb.AppendLine($"  {f};");
            }
            sb.AppendLine("}");
            return sb.ToString();   
        }
    }

    public class CodeBuilder
    {
        private readonly Class theClass = new Class();
        public CodeBuilder(string rootName)
        {
            theClass.ClassName=rootName;
        }
        public CodeBuilder AddField(string name, string type)
        {
            theClass.fields.Add(new Field {Name = name, Type = type});
            return this;
        }

        public override string ToString()
        {
            return theClass.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
        }
    }

}