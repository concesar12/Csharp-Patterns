﻿using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static System.Console;

/// <summary>
/// This code is to demonstrate how the Single principle works : 
/// We have three classes 
/// 1. Journal
/// 2. Persistence
/// 3. Demo(Main)
/// The aim of this is to Have a Journal in a list in which we will put information and then we save it in a file with persistance
/// So what we do in this case is to separate the class Journal from the Persistence in that way we can actually use the Journal only for their functions and the Persistence for its functions too
/// </summary>
/// 
public class Journal
{
    //First thing is to create a list of strings called entries
    private readonly List<string> entries = new List<string>();
    //then add the count of that journal and initialize to 0
    private static int count = 0;

    // Create a logger instance
    private static readonly ILog logger = LogManager.GetLogger(typeof(Journal));

    //then create the method to add that entry into the list journal
    public int AddEntry(string text)
    {
        //add the entry to the list and add it to our count
        entries.Add($"{++count}: {text}");
        logger.Info($"{text} Added successfully");
        return count; //memento
    }
    //Then the remove entry method as expected as well
    public void RemoveEntry(int index)
    {
        try
        {
            entries.RemoveAt(index);
            logger.Info("Removed successfully");
        }
        catch (System.Exception ex)
        {
            logger.Error("An error occurred while removing entry.", ex);
            throw;
        }

    }
    //Then I will have to override toString method to print what I like
    public override string ToString()
    {
        return string.Join(Environment.NewLine, entries);
    }

}
//The reason for this class is to create persistance and separate concerns in different classes.
public class Persistence
{
    //Create the save file to file where we will have three parameters Journal, the file name, and overwrite option
    public void SaveToFile(Journal j, string filename, bool overwrite = false)
    {
        //Then the process to serialize either overwrite is true or the file does not exist
        if (overwrite || !File.Exists(filename))
            File.WriteAllText(filename, j.ToString());
    }
/// <summary>
/// TO-DO This method reads the file and print it in memory Journal 
/// </summary>
/// <param name="filename"></param>
/// <returns></returns>
/// <exception cref="NotImplementedException"></exception>
    public Journal LoadFile(string filename)
    {
        throw new NotImplementedException();
    }

}

public class Demo
{
    static void Main(string[] args)
    {
        // Initialize log4net
        BasicConfigurator.Configure();

        //Then I have instance the class Journal and add a new entry
        var jour = new Journal();
        jour.AddEntry("Primer programa");
        jour.AddEntry("Segundo programa");
        jour.AddEntry("Tercer programa");
        jour.RemoveEntry(2);
        jour.AddEntry("Cuarto programa");

        //Write in the console, this method invokes toString implicitly
        WriteLine(jour);

        //Call persistance class
        var per = new Persistence();
        //specify the path
        var filename = "Template.txt";
        //Save the file
        per.SaveToFile(jour, filename, true);
        //Call the process to do it, UseShellExecute to tell the operating system the default program to use
        Process.Start(new ProcessStartInfo(filename) { UseShellExecute = true });
    }

}