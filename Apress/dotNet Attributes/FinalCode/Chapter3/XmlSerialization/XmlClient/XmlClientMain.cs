using System;
using System.IO;
using CustomerLibrary;
using System.Xml.Serialization;


namespace XmlClient
{
	class XmlClientMain
	{
      static void Main(string[] args)
      {  
         File.Delete("customer.xml");
         // First, establish the customer data
         Address addr = new Address();
         addr.Street = "121 Maple St.";
         addr.City = "Springfield";
         addr.State = State.IL;
         addr.Zip = 33333;

         PreferredCustomer pc;
         pc = new PreferredCustomer("Homer", addr, 0.20);

         // Open a file to hold the data
         Stream s = File.OpenWrite("customer.xml");

         // Create the XmlSerializer and serialize customer to file
         XmlSerializer xs = new XmlSerializer(typeof(PreferredCustomer));
         xs.Serialize(s,pc);
         s.Close();

         DeserializeCustomer();

         File.Delete("order.xml");
         // Serialize the order history information
         OrderHistory history = new OrderHistory(1);
         s = File.OpenWrite("order.xml");

         xs = new XmlSerializer(typeof(OrderHistory));
         xs.Serialize(s, history);

         s.Close();
      }

      static void DeserializeCustomer()
      {
         PreferredCustomer pc;
         XmlSerializer xs = new XmlSerializer(typeof(PreferredCustomer));
         
         // Open the XML document
         Stream s = File.OpenRead("customer.xml");

         // Deserialize the XML
         pc = (PreferredCustomer)xs.Deserialize(s);

         Console.WriteLine(pc.Discount);
      }
	}

  
}
