using System;

namespace Context
{
	class ContextMain
	{
      static void Main(string[] args)
      {
         NumberDispenser nd = new NumberDispenser();
         int i = nd.TakeANumber();		
      }
	}
}
