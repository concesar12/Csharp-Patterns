﻿// using System;
// using System.Collections.Generic;
// /**/
// namespace Functional_builder
// {
//   public class Person
//   {
//     public string Name, Position;
//   }
//   //Sealed class means it can't inherit
//   public sealed class PersonBuilder
//   {
//     public readonly List<Action<Person>> Actions 
//       = new List<Action<Person>>();

//     public PersonBuilder Called(string name)
//     {
//       Actions.Add(p => { p.Name = name; });
//       return this;
//     }

//     public Person Build()
//     {
//       var p = new Person();
//       Actions.ForEach(a => a(p));
//       return p;
//     }
//   }

//   public static class PersonBuilderExtensions
//   {
//     public static PersonBuilder WorksAsA
//       (this PersonBuilder builder, string position)
//     {
//       builder.Actions.Add(p =>
//       {
//         p.Position = position;
//       });
//       return builder;
//     }
//   }
  
//   public class FunctionalBuilder
//   {
//     public static void Main(string[] args)
//     {
//       var pb = new PersonBuilder();
//       var person = pb.Called("Dmitri").WorksAsA("Programmer").Build();
//     }
//   }
// }