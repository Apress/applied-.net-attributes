using System;

namespace CustomerClient
{	
   using System.Runtime.Remoting;
   using CustomerLibrary;

   class ClientMain
   {
      static void Main(string[] args)
      {
         // Create a local proxy to the remote object.
         object remote = Activator.GetObject(typeof(CustomerService), 
            "http://localhost:13101/CustomerService.soap");

         // Cast the proxy to the CustomerService type
         CustomerService custSvc = (CustomerService)remote;

         // Fetch a customer
         Customer cust = custSvc.GetCustomer(1);
         Console.WriteLine(cust.Name);

         // Test the OneWay attribut
         Console.WriteLine("Begin Save method ...");
         custSvc.Save(1, "Homer", "hs@atomic.com");
         Console.WriteLine("End Save method");
      }
   }
}
