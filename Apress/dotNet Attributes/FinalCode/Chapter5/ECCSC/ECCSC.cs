using ECFramework;
using ECFramework.Compiler;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace ECFramework
{
	class ECCSC
	{
		internal const string MESSAGE_OK = "EC compilation was successful.";
		internal const string MESSAGE_ERRORS = "EC compilation was unsuccessful.";
		internal const string MESSAGE_UNEXPECTED = "An unexpected exception occurred during EC compilation.";
		internal const string MESSAGE_UNKNOWN = "No results were returned from EC compilation.";
		internal const string MESSAGE_ERROR = "Error {0}\n\tCode: {1}\n\tDescription - {2}";

		internal const int OK_STATUS = 0;
		internal const int UNKNOWN_STATUS = 1;
		internal const int UNHANDLED_ERROR_STATUS = 2;

		static int Main(string[] args)
		{
			int retVal = OK_STATUS;
			
			try
			{
				CodeDomProvider provider = new ECProvider(new CommandLineResults(args));
				ICodeCompiler compiler = provider.CreateCompiler();
				CommandLineResults cmdResults = new CommandLineResults(args);

				CompilerResults results = compiler.CompileAssemblyFromFileBatch(
					cmdResults.Parameters, cmdResults.Files);

				if(results != null)
				{
					if(results.Errors != null && results.Errors.Count > 0)
					{
						Console.WriteLine(MESSAGE_ERRORS);
						int errorCount = 1;

						foreach(CompilerError error in results.Errors)
						{
							Console.WriteLine(string.Format(
								MESSAGE_ERROR, errorCount,
								error.ErrorNumber, error.ErrorText));
							errorCount++;
						}
					}
					else
					{
						Console.WriteLine(MESSAGE_OK);
					}
				}
				else
				{
					retVal = UNKNOWN_STATUS;
					Console.WriteLine(MESSAGE_UNKNOWN);
				}
			}
			catch(Exception ex)
			{
				retVal = UNHANDLED_ERROR_STATUS;
				Console.WriteLine(MESSAGE_UNEXPECTED);
				Console.WriteLine(ex);
				Console.WriteLine(ex.StackTrace);
			}

			return retVal;
		}
	}
}
