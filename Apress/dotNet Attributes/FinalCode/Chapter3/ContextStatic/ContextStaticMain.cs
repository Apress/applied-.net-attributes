using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace ContextStatic
{
   class ContextStaticMain
   {
      static void Main(string[] args)
      {
         CountMe c1 = new CountMe();
         CountMe c2 = new CountMe();

         Console.WriteLine("Context ID for c1 = {0}", c1.GetContextId());
         Console.WriteLine("Context ID for c2 = {0}", c2.GetContextId());
         Console.WriteLine("Static count = {0}", c1.StaticCount);
         Console.WriteLine("Context count = {0}", c1.ContextCount);

         Console.ReadLine();
      }
   }

   [Synchronization()]
   class CountMe : ContextBoundObject
   {
      private static int mStaticCount = 0;

      [ContextStatic()]
      private static int mContextCount = 0;

      public CountMe()
      {
         mStaticCount++;
         mContextCount++;
      }

      public int StaticCount
      {
         get { return mStaticCount; }
      }

      public int ContextCount
      {
         get { return mContextCount; }
      }

      public int GetContextId()
      {
         return Thread.CurrentContext.ContextID;
      }
   }
}
