#define CUSTOM_TRACING // Turn on custom tracing

using System;
using TraceLibrary;

namespace ConditionalTest
{
   class MainApp
   {
      static void Main(string[] args)
      {
         CustomTrace.Write(Console.Out, "Starting Main");
         Console.WriteLine("Doing work in Main");
         CustomTrace.Write(Console.Out, "Leaving Main");

         Console.ReadLine();
      }
   }
}