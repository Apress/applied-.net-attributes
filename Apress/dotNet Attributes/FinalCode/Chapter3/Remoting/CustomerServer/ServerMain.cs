using System;

namespace CustomerServer
{
   using System.Runtime.Remoting;
   using System.Runtime.Remoting.Channels;
   using System.Runtime.Remoting.Channels.Http;

   class ServerMain
   {
	   static void Main(string[] args)
	   {
         // Establish channel and open port 13101
		   IChannel channel = new HttpChannel(13101);
         ChannelServices.RegisterChannel(channel);

         // Configure CustomerServices as a well-known singleton object
         RemotingConfiguration.RegisterWellKnownServiceType(
            typeof(CustomerLibrary.CustomerService),
            "CustomerService.soap",
            WellKnownObjectMode.Singleton);

         // Keep running until user presses enter
         Console.WriteLine("Server started. Press enter to end.");
         Console.ReadLine();
	   }
   }
}
