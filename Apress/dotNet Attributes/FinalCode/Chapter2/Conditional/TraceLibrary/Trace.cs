using System;
using System.IO;
using System.Diagnostics;

namespace TraceLibrary
{
   public class CustomTrace
   {
      [Conditional("CUSTOM_TRACING")]
      public static void Write(TextWriter tw, string msg)
      {  
         tw.WriteLine("{0}: {1}", DateTime.Now, msg);
      }
   }
}
