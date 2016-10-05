using CustomerLibrary;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

// Must also set reference to System.Runtime.Serialization.Formatters.dll
using System.Runtime.Serialization.Formatters.Soap;

namespace SerializationExample
{
   class SerializationMain
   {
      static void Main(string[] args)
      {
         // First, establish the customer data
         Address addr;
         addr.Street = "121 Maple St.";
         addr.City = "Springfield";
         addr.State = State.IL;
         addr.Zip = 33333;

         PreferredCustomer pc;
         pc = new PreferredCustomer("Homer", addr, 0.20);

         // Open a file to hold the data
         Stream s = File.OpenWrite("customer.soap");

         // Create the Soap formatter and serialize to file
         IFormatter formatter = new SoapFormatter();
         formatter.Serialize(s, pc);

         s.Close();

         DeserializeCustomer();
      }

      static void DeserializeCustomer()
      {
         // Open the data file for reading
         Stream s = File.OpenRead("customer.soap");

         // Create the SoapFormatter, this time for deserialization purposes
         IFormatter formatter = new SoapFormatter();

         // Deserialize, cast returned object to PreferredCustomer
         PreferredCustomer pc = (PreferredCustomer)formatter.Deserialize(s);
         s.Close();

         // Use the PreferredCustomer object ...
      }
   }
}
