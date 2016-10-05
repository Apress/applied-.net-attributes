using System;
using System.Runtime.Remoting.Messaging;

namespace DeveloperInfo
{
   public class DeveloperInfoSink : IMessageSink
   {
      private IMessageSink mNextSink;
      private DeveloperInfoAttribute mDevInfo;

	   public DeveloperInfoSink(IMessageSink nextSink, 
         DeveloperInfoAttribute devInfo)
	   { 
         mNextSink = nextSink; 
         mDevInfo = devInfo;
      }

      public IMessageSink NextSink
      {
         get {return mNextSink; }
      }

      public IMessage SyncProcessMessage(IMessage msg)
      {
         msg = PreProcess(msg);
         IMessage returnMsg = mNextSink.SyncProcessMessage(msg);
         return PostProcess(msg, returnMsg);
      }

      private IMessage PreProcess(IMessage msg)
      {
         // Perform any required pre processing here
         return msg;
      }

      private IMessage PostProcess(IMessage msg, IMessage returnMsg)
      {
         IMethodReturnMessage rm = (IMethodReturnMessage)returnMsg;   
         if (rm.Exception != null)
         {
            rm = new ReturnMessage(
               new CustomException(rm.Exception, mDevInfo), 
               (IMethodCallMessage)msg);
         }
         return rm;
      }

      public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
      {
         return null;
      }	
   }
}
