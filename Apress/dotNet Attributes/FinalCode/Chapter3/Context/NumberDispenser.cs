using System;

namespace Context
{
   // Required for Synchronization attribute
   using System.Runtime.Remoting.Contexts;

   [Synchronization()]
   public class NumberDispenser : ContextBoundObject
   {
      private int mCurrentNumber;
   	
      public int TakeANumber()
      {
         mCurrentNumber++;
         return mCurrentNumber;
      }
   }
}
