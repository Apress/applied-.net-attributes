using System;
using System.EnterpriseServices;
using CustomerLibrary;

namespace CustomerClient
{
   class ClientMain
   {		
      static void Main(string[] args)
      {
         CustomerService cs = new CustomerService();
         Console.WriteLine(cs.GetContextInfo());
      }
   }
}
