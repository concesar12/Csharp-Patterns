using System;
using System.Collections.Generic;
using static System.Console;

//The idea of this program is to show how can we implement a store that sells consoles and games and bundles of it, 
//The idea behind is to make use of the principle of open-closed
public enum Consoles
{
    nintendo, ps, xbox
}

public enum Games
{
    gears, mario, forza, starfield
}

public class Bundle
{
    public string Name;
    public Consoles _consoles;
    public Games _games;

    public Bundle(string name, Consoles consoles, Games games)
    {
        if (name == null)
        {
            throw new ArgumentNullException(paramName: nameof(name));
        }
        Name = name;
        _consoles = consoles;
        _games = games;
    }
}

//We want to filter our product so we will create a class that will allow us to do so
public class BundleFilter
{
    /*
    The purpose of this method is to filter the  Bunsles  collection based on the  consoles  value. It iterates over each  Bundle  object in the  Bunsles  collection and checks
     if the  _consoles  property of the  Bundle  object matches the specified  consoles  value. 
     If there is a match, it yields (returns) the  Bundle  object. 
 
    The  yield return  statement is used to create an iterator block. It allows the method to return elements one at a time, as requested, instead of returning the entire collection at once. 
    This is useful when dealing with large collections or when you want to lazily evaluate the items. 
    */
    public IEnumerable<Bundle> FilterByConsoles(IEnumerable<Bundle> bundles, Consoles consoles)
    {
        foreach (var c in bundles)
            if (c._consoles == consoles)
                yield return c;
    }

    public IEnumerable<Bundle> FilterByGames(IEnumerable<Bundle> bundles, Games games)
    {
        foreach (var c in bundles)
            if (c._games == games)
                yield return c;
    }
}

//Create an interface that will take a specification of a typt T that will be filtered
public interface ISpecification<T>
{
    bool IsSatisfied(T t);    
}

//Create Ifilter that will filter any object based on the specification
public interface IFilter<T>
{
    IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
}

//Create a class for Consoles specification
public class ConsoleSpecification : ISpecification<Bundle> 
{
    private Consoles _consoles;

    public ConsoleSpecification(Consoles consoles)
    {
        this._consoles = consoles;
    }
    //Still need more undersdtanding
    public bool IsSatisfied(Bundle t)
    {
        return t._consoles == _consoles;
    }
}

//Create a class for Game specification
public class GameSpecification :ISpecification<Bundle>
{
    private Games _games;

    public GameSpecification(Games games)
    {
        this._games = games;
    }
    //Still need more undersdtanding
    public bool IsSatisfied(Bundle t)
    {
        return t._games == _games;
    }
}

//Now we will create a better filter
public class BetterFilter :IFilter<Bundle>
{
    //The reason for this is because filter returns a list of all filtered elements
    public IEnumerable<Bundle> Filter(IEnumerable<Bundle> items, ISpecification<Bundle> spec)
    {
        //Have the specification of the bundle
        foreach (var i in items)
            if(spec.IsSatisfied(i))
                yield return i;
    }
}

//In case we want to specify by two things, then, create ANDspecification class and leave it generic:
public class AndSpecification<T> :ISpecification<T>
{
    private ISpecification<T> first, second;

    public AndSpecification(ISpecification<T> first, ISpecification<T> second)
    {
        this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
        this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
    }

    public bool IsSatisfied(T t)
    {
        //First thing to do is to declare the two specifications to evaluate
        return first.IsSatisfied(t) && second.IsSatisfied(t);
    }
}

public class Demo
{
    public static void Main(string[] args)
    {
        // in here we will create some bundles
        var bundle1 = new Bundle("massimo", Consoles.xbox, Games.starfield);
        var bundle2 = new Bundle("Mega", Consoles.ps, Games.forza);
        var bundle3 = new Bundle("family", Consoles.nintendo, Games.mario);

        //Create an array, list or enumerable to store them
        Bundle[] bundles = { bundle1, bundle2, bundle3 };

        //Start using the filter now filter by console and by games
        var bf = new BundleFilter();
        WriteLine("Bundles with xbox");
        foreach (var b in bf.FilterByConsoles(bundles, Consoles.xbox))
        {
            WriteLine($" - {b.Name} has xbox");
        }
        foreach (var b in bf.FilterByGames(bundles, Games.mario))
        {
            WriteLine($" - {b.Name} has starfield");
        }
        //Use the better filter in here to filter create a new
        var btf = new BetterFilter();
        WriteLine("Bundles with ps (new filter)");
        //NOw iterate for every betterfilter and start filtering
        foreach (var b in btf.Filter(bundles, new ConsoleSpecification(Consoles.ps)))
        {
            WriteLine($" - {b.Name} has ps");
        }

        foreach (var b in btf.Filter(bundles, new GameSpecification(Games.forza)))
        {
            WriteLine($" - {b.Name} has {b._games}");
        }

        //Use better filter to get specification by console and games.
        foreach (var b in btf.Filter(bundles, new AndSpecification<Bundle>(new ConsoleSpecification(Consoles.xbox), new GameSpecification(Games.starfield))))
        {
            WriteLine($" - {b.Name} has {b._games} and {b._consoles}");
        }
    }
}