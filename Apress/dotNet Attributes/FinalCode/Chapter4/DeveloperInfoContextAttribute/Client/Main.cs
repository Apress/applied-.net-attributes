using System;
using DeveloperInfo;

namespace Client
{
   public class ClientMain
   {

      static void Main(string[] args)
      {     
         // Demo is created in seperate context
         Demo d = new Demo();

         // Catch the exception
         try
         {
            d.DemoMethod();
         }
         catch(CustomException ce)
         {
            Console.WriteLine("\n** Begin Developer Info **");
            Console.WriteLine("Name: {0}", ce.DeveloperInfo.Name);
            Console.WriteLine("Email: {0}", ce.DeveloperInfo.Email);
            Console.WriteLine("Work Phone: {0}", ce.DeveloperInfo.WorkPhone);
            Console.WriteLine("Mobile Phone: {0}", ce.DeveloperInfo.MobilePhone);
            Console.WriteLine("** End Developer Info **\n");
         }

         // What happens with no try/catch?
         d.DemoMethod();
         Console.ReadLine();
      }
   }    
}
