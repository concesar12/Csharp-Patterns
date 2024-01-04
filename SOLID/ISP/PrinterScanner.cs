using System;
using System.Collections.Generic;
using static System.Console;

namespace ISP
{
    /*In this excercise we will try to simulate an scenario where we are going to print or scan a document
     We will demostrate Interface segregation principle which will show how to */
    public class Document
    {

    }

    public interface IMachine
    {
        //Create functionalities for the printer
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Fax(Document d)
        {

        }

        public void Print(Document d)
        {

        }

        public void Scan(Document d)
        {

        }
    }

    //Create old fashion printer that does not require many functionalities
    public class OldFashionPrinter : IPrinter
    {
        public void Fax(Document d)
        {
            throw new NotImplementedException();
        }

        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }

    public class Photocopier :IScanner, IPrinter
    {
        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }

    //Separate concerns to avoid using extra stuff
    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public interface IFaxer
    {
        void Fax(Document d);
    }

    public class PrinterScanner
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }



}