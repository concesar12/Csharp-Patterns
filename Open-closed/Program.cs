using System;
using System.Collections.Generic;
using static System.Console;

//The idea of this program is to show how can we implement a store that sells pcs and games, 
//The idea behind is to make use of the principle of open-closed
public enum Consoles
{
    nintendo, ps, xbox
}

public enum Games
{
    gears, halo, forza, starfield
}

public class Computer
{
    public string Name;
    public Consoles _consoles;
    public Games _games;

    public Computer(string name, Consoles consoles, Games games)
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
public class ComputerFilter
{
    /*
    The purpose of this method is to filter the  computers  collection based on the  consoles  value. It iterates over each  Computer  object in the  computers  collection and checks
     if the  _consoles  property of the  Computer  object matches the specified  consoles  value. 
     If there is a match, it yields (returns) the  Computer  object. 
 
    The  yield return  statement is used to create an iterator block. It allows the method to return elements one at a time, as requested, instead of returning the entire collection at once. 
    This is useful when dealing with large collections or when you want to lazily evaluate the items. 
    */
    public static IEnumerable<Computer> FilterByConsoles(IEnumerable<Computer> computers, Consoles consoles)
    {
        foreach (var c in computers)
            if(c._consoles == consoles)
                yield return c;
    }
}

public class Demo
{
    public static void Main(string[] args)
    {
        // in here we will create some computers
    }
}