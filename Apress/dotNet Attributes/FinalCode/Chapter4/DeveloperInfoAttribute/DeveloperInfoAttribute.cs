using System;

namespace DeveloperAttribute
{
   [AttributeUsage(AttributeTargets.All, AllowMultiple=true, Inherited=true)]
   public class DeveloperInfoAttribute : Attribute
   { 
      private string mName;
      private string mEmail;
      private string mWorkPhone = string.Empty;
      private string mMobilePhone = string.Empty;

      public DeveloperInfoAttribute(string name, string email)
      {
         mName = name;
         mEmail = email;
      }

      public DeveloperInfoAttribute(string name)
      {
         mName = name;
      }

      public string Name
      {
         get { return mName; }
      }
      public string Email
      {
         get { return mEmail; }
      }
      public string WorkPhone
      {
         get { return mWorkPhone; }
         set { mWorkPhone = value; }
      }
      public string MobilePhone
      {
         get { return mMobilePhone; }
         set { mMobilePhone = value; }
      }

      public override string ToString()
      {
         return string.Format("Name={0};Email={1};Work Phone={2};Mobile Phone={3}", 
            mName, mEmail, mWorkPhone, mMobilePhone);
      }
   }
}
