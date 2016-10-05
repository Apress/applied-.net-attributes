using System;
using System.Reflection;
using System.Runtime.Remoting.Contexts;

namespace DeveloperAttribute
{
   class TheApp
   {
      static void Main(string[] args)
      {
         Demo demo = new Demo();
         demo.DemoMethod();   
      }
   }
}
