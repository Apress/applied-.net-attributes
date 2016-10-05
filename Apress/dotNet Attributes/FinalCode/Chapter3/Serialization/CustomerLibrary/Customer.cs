using System;

namespace CustomerLibrary
{
   public class OrderHistory
   {  // tracks order history details for a customer
   }

   public enum State
   {
      AL, AZ, IL, MN, MI, ME, OH, WI // etc.
   }

   [Serializable()]
   public struct Address
   {
      public string Street;
      public string City;
      public State State;
      public int Zip;
   }

   [Serializable()]
   public class Customer
   {
      private string mName;
      private Address mAddress;

      [NonSerialized()] private OrderHistory mHistory;

      public Customer(string name, Address address)
      {
         mName = name;
         mAddress = address;
         mHistory = new OrderHistory();
      }
   }

   [Serializable()]
   public class PreferredCustomer : Customer
   {
      private double mDiscount;

      public PreferredCustomer(string name, Address address, double discount)
         : base(name, address)
      {
         mDiscount = discount;
      }
   }
}
