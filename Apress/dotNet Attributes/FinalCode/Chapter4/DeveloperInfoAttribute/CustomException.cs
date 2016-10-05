using System;
using System.Reflection;
using System.Diagnostics;

namespace DeveloperAttribute
{
   public class CustomException : ApplicationException
   {
      private DeveloperInfoAttribute mDevInfo;

	   public CustomException(Exception e, object sourceObject)
         : base(e.Message, e)
	   {
         mDevInfo = (DeveloperInfoAttribute)Attribute.GetCustomAttribute(
            sourceObject.GetType(), typeof(DeveloperInfoAttribute), false);
	   }

      public DeveloperInfoAttribute DeveloperInfo
      {
         get {return mDevInfo;}
      }

      public override string ToString()
      {
         return string.Format("{0}\nDeveloper Information:{1}",
            base.ToString(), mDevInfo.ToString());
      }
   }
} 