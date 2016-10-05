using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;

namespace DeveloperInfo
{
   [AttributeUsage(AttributeTargets.Class, Inherited=true)]
   public class DeveloperInfoAttribute : ContextAttribute, IContributeObjectSink
   { 
      private string mName;
      private string mEmail;
      private string mWorkPhone = string.Empty;
      private string mMobilePhone = string.Empty;

      public DeveloperInfoAttribute(string name, string email)
         : base("DeveloperInfo")
      {
         mName = name;
         mEmail = email;
      }

      public DeveloperInfoAttribute(string name)
         :base("DeveloperInfo")
      {
         mName = name;
      }

      public override string ToString()
      {
         return string.Format("Name={0};Email={1};Work Phone={2};Mobile Phone={3}", 
            mName, mEmail, mWorkPhone, mMobilePhone);
      }

      public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
      {
         return new DeveloperInfoSink(nextSink, this);
      }

      public new string Name
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
   }
}
