using System;
using System.Xml.Serialization; // Must also reference System.Xml.dll

namespace CustomerLibrary
{
   public enum State
   {
      AL, AZ, IL, MN, MI, ME, OH, WI // etc.
   }

   public struct Address
   {
      public string Street;
      public string City;
      public State State;
      public int Zip;
   }
 
   public class Customer
   {
      private string mName; 
 
      [XmlAttribute("Name")]  // Serialize this as an attribute
      public string Name
      {
         get { return mName; }
         set { mName = value; }
      }
 
      // Make Home address a public field
      public Address HomeAddress;

      [XmlIgnore()]  // Tell serializer to skip the order history
      public OrderHistory mHistory;

      public Customer(string name, Address address)
      {
         mName = name;
         HomeAddress = address;
         mHistory = new OrderHistory();
      }

      // Default ctor required For XML Serialization
      public Customer(){}
   }

   [XmlRoot("VIP")]
   public class PreferredCustomer : Customer
   {
      private double mDiscount;

      [XmlElement("CustomerDiscount")]
      public double Discount
      {
         get { return mDiscount; }
         set { mDiscount = value; }
      }

      public PreferredCustomer(string name, Address address, double discount)
         : base(name, address)
      {
         mDiscount = discount;
      }

      // Default ctor required For XML Serialization
      public PreferredCustomer() {}
   }
}
