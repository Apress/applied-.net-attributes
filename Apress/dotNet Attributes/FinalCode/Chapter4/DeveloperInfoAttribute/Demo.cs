using System;
using System.IO;

namespace DeveloperAttribute
{
   [DeveloperInfo("Homer", "hs@atomic.com", WorkPhone="555-5555")]
   public class Demo
   {
      public void DemoMethod()
      {
         try 
         {
            ForceException();
         }
         catch(Exception e)
         {
            CustomException custom = new CustomException(e, this);
            throw custom;
         }
      }

      private void ForceException()
      {
         int zero = 0;
         int i = 5 / zero;
      }
   }
}
