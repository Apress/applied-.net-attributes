using System;
using System.Text;
using System.EnterpriseServices;

namespace CustomerLibrary
{
   [JustInTimeActivation()]
   public class CustomerService : ServicedComponent
   {
      public CustomerService() {}

      public string GetContextInfo()
      {
         StringBuilder ctxInfo = new StringBuilder();
            
         // Use ContextUtil to fetch context information
         ctxInfo.AppendFormat("Context ID:  {0}\n", ContextUtil.ContextId);
         ctxInfo.AppendFormat("Activity ID: {0}\n", ContextUtil.ActivityId);
           
         // If in transaction, get transaction ID
         string txId = "No Tx";
         if (ContextUtil.IsInTransaction)
            txId = ContextUtil.TransactionId.ToString();

         ctxInfo.AppendFormat("Transaction ID:    {0}\n", txId);
         ctxInfo.AppendFormat("Security Enabled?: {0}\n", 
            ContextUtil.IsSecurityEnabled);
           
         return ctxInfo.ToString();
      }
   }
}
