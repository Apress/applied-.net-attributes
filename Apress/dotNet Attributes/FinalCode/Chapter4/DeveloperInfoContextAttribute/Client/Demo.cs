using System;
using DeveloperInfo;

namespace Client
{
   [DeveloperInfo("Homer", "hs@atomic.com", WorkPhone="555-5555")]
   public class Demo : ContextBoundObject
   {
      public void DemoMethod()
      {
         ForceException();
      }
     
      private int ForceException()
      {
         int zero = 0;
         return 5 / zero;
      }
   }
}
