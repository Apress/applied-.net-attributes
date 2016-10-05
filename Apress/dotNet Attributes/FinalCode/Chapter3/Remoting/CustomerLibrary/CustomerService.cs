using System;

namespace CustomerLibrary
{
   using System.Runtime.Remoting.Messaging; // Contains OneWay attr
   using System.Threading;

   public class CustomerService : MarshalByRefObject
   {
      public Customer GetCustomer(int customerId)
      {
         // Look up customer in database and return.
         return new Customer(1, "Homer Simpson", "hs@atomic.com");
      }
      public double GetTotalExpenditures(int customerId)
      {
         // Query database and calculate the total sum the
         // customer has spent on our products.
         return 1400; // A simulated value
      }

      [OneWay()]
      public void Save(int id, string name, string email)
      {
         // Save customer data to database

         // Sleep for 5 seconds to simulate long running task
         Thread.Sleep(5000);
      }
   }
}
