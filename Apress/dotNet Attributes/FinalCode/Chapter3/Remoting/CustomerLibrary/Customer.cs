using System;

namespace CustomerLibrary
{
   [Serializable()]
   public class Customer
   {
      private int mId;
      private string mName;
      private string mEmail;

      public Customer(int id, string name, string email)
      {
         mId = id;
         mName = name;
         mEmail = email;
      }

      public int Id
      {
         get { return mId; }
         set { mId = value; }
      }
      public string Name
      {
         get { return mName; }
         set { mName = value; }
      }
      public string Email
      {
         get { return mEmail; }
         set { mEmail = value; }
      }
   }
}