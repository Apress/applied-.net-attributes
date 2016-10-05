using Microsoft.CSharp;
using Microsoft.Tools.FxCop.Sdk.IL;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;

namespace ECFramework.Compiler
{
	public class CommandLineResults
	{
		[DllImport("mscoree.dll")]
		private static extern int GetCORSystemDirectory([MarshalAs(UnmanagedType.LPWStr)] StringBuilder buffer, int bufferLength, ref int length);
		private const int MAX_PATH = 1024;

		private const string CSC_RSP = "csc.rsp";
		private const string DEBUG_OPTION = "debug";
		private const string DEBUG_MINUS_OPTION = "debug-";
		private const string MAIN_OPTION = "main:";
		private const string NO_CONFIG_OPTION = "noconfig";
		private const string OUT_OPTION = "out:";
		private const string REFERENCE_OPTION = "reference:";
		private const string REFERENCE_SHORT_OPTION = "r:";
		private const string RESPONSE_OPTION = "@";
		private const string SWITCH_SLASH = "/";
		private const string SWITCH_DASH = "-";
		private const string TARGET_OPTION = "target:";
		private const string TARGET_EXE_OPTION = "exe";
		private const string WARN_AS_ERROR_OPTION = "warnaserror";
		private const string WARN_AS_ERROR_MINUS_OPTION = "warnaserror-";
		private const string WARNING_LEVEL_OPTION = "warn:";
		private const string WARNING_LEVEL_SHORT_OPTION = "w:";
		private const string WIN32_RESOURCE_OPTION = "win32res:";

		private string[] mFiles = null;
		private bool mFixup = false;
		private CompilerParameters mParameters = null;

		public CommandLineResults(string[] args)
		{
			Initialize(args);
		}

		private void Initialize(string[] args)
		{
			bool useConfig = true;
			this.mParameters = new CompilerParameters();
			StringCollection filesCollection = new StringCollection();
			StringBuilder options = new StringBuilder();

			this.mParameters.GenerateExecutable = false;
			this.mParameters.GenerateInMemory = false;
			this.mParameters.IncludeDebugInformation = false;
			this.mParameters.TreatWarningsAsErrors = false;

			foreach(string arg in args)
			{
				int val = arg.IndexOf(SWITCH_SLASH);
				if(arg.IndexOf(SWITCH_DASH) == 0 ||
					arg.IndexOf(SWITCH_SLASH) == 0)
				{
					//  It's a C# switch - figure out if it maps to
					//  a property. If it doesn't, add it to options.
					if(arg.IndexOf(DEBUG_OPTION) == 1)
					{
						if(arg.IndexOf(DEBUG_MINUS_OPTION) == -1)
						{
							this.mParameters.IncludeDebugInformation = true;
						}
					}
					else if(arg.IndexOf(MAIN_OPTION) == 1)
					{
						this.mParameters.MainClass =
							arg.Substring(MAIN_OPTION.Length + 1);
					}
					else if(arg.IndexOf(OUT_OPTION) == 1)
					{
						this.mParameters.OutputAssembly =
							arg.Substring(OUT_OPTION.Length + 1);
					}
					else if(arg.IndexOf(REFERENCE_OPTION) == 1)
					{
						this.mParameters.ReferencedAssemblies.Add(
							arg.Substring(REFERENCE_OPTION.Length + 1));
					}
					else if(arg.IndexOf(REFERENCE_SHORT_OPTION) == 1)
					{
						this.mParameters.ReferencedAssemblies.Add(
							arg.Substring(REFERENCE_SHORT_OPTION.Length + 1));
					}
					else if(arg.IndexOf(TARGET_OPTION) == 1 && 
						arg.IndexOf(TARGET_EXE_OPTION) > 0)
					{
						this.mParameters.GenerateExecutable = true;
					}
					else if(arg.IndexOf(WARN_AS_ERROR_OPTION) == 1)
					{
						if(arg.IndexOf(WARN_AS_ERROR_MINUS_OPTION) == -1)
						{
							this.mParameters.TreatWarningsAsErrors = true;
						}
					}
					else if(arg.IndexOf(WARNING_LEVEL_OPTION) == 1)
					{
						this.mParameters.WarningLevel = Int32.Parse(
							arg.Substring(WARNING_LEVEL_OPTION.Length + 1));
					}
					else if(arg.IndexOf(WARNING_LEVEL_SHORT_OPTION) == 1)
					{
						this.mParameters.WarningLevel = Int32.Parse(
							arg.Substring(WARNING_LEVEL_SHORT_OPTION.Length + 1));
					}
					else if(arg.IndexOf(WIN32_RESOURCE_OPTION) == 1)
					{
						this.mParameters.Win32Resource = 
							arg.Substring(WIN32_RESOURCE_OPTION.Length + 1);
					}
					else
					{
						if(arg.IndexOf(NO_CONFIG_OPTION) == 1)
						{
							useConfig = false;
						}
						options.Append(string.Format("{0} ", arg));
					}
				}
				else if(arg.IndexOf(RESPONSE_OPTION) == 0)
				{
					//  Check for the response file - if it's there
					//  add it.
					options.Append(string.Format("{0} ", arg));
				}
				else
				{
					//  It's a file.
					filesCollection.Add(arg);
				}
			}

			//  Use the default .rsp file if /noconfig wasn't specified.
			if(useConfig == true)
			{
				string cscRspPath = string.Empty;

				StringBuilder corPath = new StringBuilder(MAX_PATH);
				int pathSize = 0;
				GetCORSystemDirectory(corPath, corPath.Capacity, ref pathSize);
				
				cscRspPath = Path.Combine(corPath.ToString(), CSC_RSP);

				options.Append(string.Format("@{0} ", cscRspPath));
			}

			this.mParameters.CompilerOptions = options.ToString().Trim();

			this.mFiles = new string[filesCollection.Count];
			filesCollection.CopyTo(this.mFiles, 0);

			//  Set the output value if it hasn't been.
			if(this.mParameters.OutputAssembly == null ||
				this.mParameters.OutputAssembly.Length == 0)
			{
				//  The rule is to use the first file found if it's a DLL.
				if(this.mParameters.GenerateExecutable == false)
				{
					this.mParameters.OutputAssembly = 
						string.Format("{0}.dll", 
						Path.GetFileNameWithoutExtension(this.mFiles[0]));
				}
				else
				{
					//  We'll need to fix it up later.
					this.mFixup = true;
				}
			}
		}

		public string[] Files
		{
			get
			{
				return this.mFiles;
			}
		}

		public bool Fixup
		{
			get
			{
				return this.mFixup;
			}
		}

		public CompilerParameters Parameters
		{
			get
			{
				return this.mParameters;
			}
		}
	}

	public class ECProvider : CSharpCodeProvider
	{
		private const string PARAM_CMD_LINE_RESULTS = "cmdLineResults";
		protected CommandLineResults mCmdLineResults = null;

		private ECProvider() : base() {}

		public ECProvider(CommandLineResults cmdLineResults) : base() 
		{
			//  PRECONDITION: cmdLineResults != null.
			if(cmdLineResults == null)
			{
				throw new ArgumentNullException(PARAM_CMD_LINE_RESULTS);
			}

			this.mCmdLineResults = cmdLineResults;
		}

		override public ICodeCompiler CreateCompiler()
		{
			return new ECCompiler(base.CreateCompiler(), this.mCmdLineResults);
		}
	}

	public class UnhandledExceptions
	{
		private const string PARAM_TARGET_METHOD = "targetMethod";
		private const int MAX_OPCODE_SIZE = 4;
		protected ArrayList mExceptionList = new ArrayList();
		protected MethodInfo mTargetMethodInfo = null;
		protected Method mTargetMethod = null;
		protected byte[] mCIL = null;

		private UnhandledExceptions() : base() {}

		public UnhandledExceptions(MethodInfo targetMethod) : base()
		{
			//  PRECONDITION: targetMethod != null.
			if(targetMethod == null)
			{
				throw new ArgumentNullException(PARAM_TARGET_METHOD);
			}
				
			this.mTargetMethodInfo = targetMethod;
			this.mTargetMethod = Method.GetMethod(targetMethod.ReflectedType.Module,
				this.mTargetMethodInfo);
			this.mCIL = this.mTargetMethod.GetILByteStream();
			this.FindUnhandledExceptions();
		}

		protected void FindUnhandledExceptions()
		{
			Hashtable methodExMarks = this.GetExceptionMarks();
			bool hasHandlers = true;

			if(methodExMarks.Count > 0)
			{
				//  This method can throw exceptions.
				ArrayList exHandlers = this.mTargetMethod.RetrieveEHClauses();

				if(exHandlers != null && exHandlers.Count > 0)
				{
					//  Remove the unneeded handlers.
					for(int i = exHandlers.Count - 1; i >= 0; i--)
					{
						EHClause cTest = (EHClause)exHandlers[i];

						if(cTest.Type != EHClauseTypes.EHNone)
						{
							exHandlers.Remove(cTest);
						}
					}

					if(exHandlers.Count > 0)
					{
						foreach(object key in methodExMarks.Keys)
						{
							uint keyValue = (uint)((int)key);
							ArrayList foundExceptions = (ArrayList)methodExMarks[key];
							foreach(Type foundExType in foundExceptions)
							{
								bool areAllTryBlocksProcessed = false;
								bool isHandled = false;
								int maxDistance = this.mCIL.Length;
								int minDistance = -1;

								do
								{
									bool foundOneHandler = false;

									foreach(EHClause clause in exHandlers)
									{
										uint tryEnd = clause.TryOffset + clause.TryLength;
										if(keyValue >= clause.TryOffset &&
											keyValue <= tryEnd)
										{
											int distance = Math.Abs((int)clause.TryOffset - (int)keyValue) + 
												Math.Abs((int)tryEnd - (int)keyValue);
											if(distance <= maxDistance && distance > minDistance)
											{
												foundOneHandler = true;
												maxDistance = distance;
												Type handlerEx = Type.GetType(
													ILServices.GetNameForToken(this.mTargetMethod.ModuleName, 
													clause.ClassTokenOrFilterOffset));

												if(foundExType == handlerEx ||
													foundExType.IsSubclassOf(handlerEx))
												{
													isHandled = true;
													areAllTryBlocksProcessed = true;
													break;
												}
											}
										}
									} 

									if(foundOneHandler == true)
									{
										minDistance = maxDistance;
									}
									else
									{
										areAllTryBlocksProcessed = true;
									}
								} while(areAllTryBlocksProcessed == false);

								if(isHandled == false)
								{
									this.CheckExceptionTypeInThrowsAttribute(foundExType);
								}
							}
						}
					}
					else
					{
						hasHandlers = false;
					}
				}
				else
				{
					hasHandlers = false;
				}

				if(hasHandlers == false)
				{
					//  ALL of the found exceptions from GetExceptionMarks()
					//  must be marked by this method, or it's a compilation error
					foreach(object key in methodExMarks.Keys)
					{
						uint keyValue = (uint)(int)key;
						ArrayList foundExceptions = (ArrayList)methodExMarks[key];
						foreach(Type foundEx in foundExceptions)
						{
							this.CheckExceptionTypeInThrowsAttribute(foundEx);
						}
					}
				}
			}
		}

		protected Hashtable GetExceptionMarks()
		{
			Hashtable retVal = new Hashtable();

			ProgramStepCollection ps = this.mTargetMethod.DataFlow;
			int cilStart = 0;
			ArrayList methodExceptions = null;

			if(ps != null && ps.Count > 0)
			{
				for(int i = 0; i < ps.Count; i++)
				{
					ProgramStep p = ps[i];

					if(p.Operation.Flow == ControlFlow.Throw)
					{
						//  Mark it.
						if(retVal.ContainsKey(cilStart) == true)
						{
							methodExceptions = (ArrayList)retVal[cilStart];
						}
						else
						{
							methodExceptions = new ArrayList();
							retVal.Add(cilStart, methodExceptions);
						}

						//  Find out what the exception is.
						if(p.Stacks != null && p.Stacks.Count > 0)
						{
							StackPossible currentStack = (StackPossible)p.Stacks[0];
										
							if(currentStack.Elements != null && currentStack.Elements.Count > 0)
							{
								StackElement exOnStack = (StackElement)currentStack.Elements.Peek();
								Type exType = Type.GetType(exOnStack.TypeName, false, false);

								if(exType != null)
								{
									methodExceptions.Add(exType);
								}
							}
						}
					}
					else if(p.Operation.Flow == ControlFlow.Call)
					{
						if(p.RawOperand is uint && ((uint)p.RawOperand > 0)) //  It should be!
						{
							MethodBase rGetMethod = typeof(Method).GetMethod(
								"GetMethod", BindingFlags.NonPublic | BindingFlags.Static);

							Method mCalled = (Method)rGetMethod.Invoke(null, new object[] {this.mTargetMethod.AssemblyName, 
								this.mTargetMethod.ModuleName, (uint)p.RawOperand});

							if(mCalled != null)
							{
								MethodBase mCalledBase = mCalled.MethodBase;
											
								if(mCalledBase != null)
								{
									//  Check to see if it's been flagged with
									//  [Throws]
									object[] throwsAttribs = mCalledBase.GetCustomAttributes(
										typeof(ThrowsAttribute), true);

									if(throwsAttribs != null &&
										throwsAttribs.Length > 0)
									{
										if(retVal.ContainsKey(cilStart) == true)
										{
											methodExceptions = (ArrayList)retVal[cilStart];
										}
										else
										{
											methodExceptions = new ArrayList();
											retVal.Add(cilStart, methodExceptions);
										}

										foreach(ThrowsAttribute throws in throwsAttribs)
										{
											methodExceptions.Add(throws.ExceptionType);
										}
									}
								}
							}
						}
					}

					cilStart += p.Length;
				}
			}

			return retVal;
		}

		protected void CheckExceptionTypeInThrowsAttribute(Type exceptionType)
		{
			//  If the given exception isn't in the ThrowsAttribute list,
			//  add it to the mExceptionList list as it is an error.
			bool isMarked = false;

			object[] currentThrowsAttribs = this.mTargetMethodInfo.GetCustomAttributes(
				typeof(ThrowsAttribute), true);

			if(currentThrowsAttribs != null &&
				currentThrowsAttribs.Length > 0)
			{
				foreach(ThrowsAttribute currentThrows in currentThrowsAttribs)
				{
					if(exceptionType == currentThrows.ExceptionType ||
						exceptionType.IsSubclassOf(currentThrows.ExceptionType))
					{
						isMarked = true;
						break;
					}
				}
			}

			if(isMarked == false)
			{
				if(this.mExceptionList.Contains(exceptionType) == false)
				{
					this.mExceptionList.Add(exceptionType);
				}
			}					
		}

		public ArrayList ExceptionList
		{
			get
			{
				return this.mExceptionList;
			}
		}
	}

	internal class ECCompiler : ICodeCompiler
	{
		private const string PARAM_COMPILER = "compiler";
		public const string ERROR_UNHANDLED_EXCEPTION_CODE = "EC0001";
		private const string ERROR_UNHANDLED_EXCEPTION_DESCRIPTION = 
			"Method {0} in class {1} must state that it may throw an exception of type {2}.";
		protected ICodeCompiler mCompiler = null;
		protected CommandLineResults mCmdLineResults = null;

		private ECCompiler() : base() {}

		public ECCompiler(ICodeCompiler compiler, CommandLineResults cmdLineResults)
		{
			//  PRECONDITION: compiler != null.
			if(compiler == null)
			{
				throw new ArgumentNullException(PARAM_COMPILER);
			}

			this.mCompiler = compiler;
			this.mCmdLineResults = cmdLineResults;
		}

		#region Implementation of ICodeCompiler
		public System.CodeDom.Compiler.CompilerResults CompileAssemblyFromDomBatch(System.CodeDom.Compiler.CompilerParameters options, System.CodeDom.CodeCompileUnit[] compilationUnits)
		{
			return this.mCompiler.CompileAssemblyFromDomBatch(options, compilationUnits);
		}

		public System.CodeDom.Compiler.CompilerResults CompileAssemblyFromFile(System.CodeDom.Compiler.CompilerParameters options, string fileName)
		{
			return this.mCompiler.CompileAssemblyFromFile(options, fileName);
		}

		public System.CodeDom.Compiler.CompilerResults CompileAssemblyFromFileBatch(System.CodeDom.Compiler.CompilerParameters options, string[] fileNames)
		{
			//  Redirect this call into the CompileAssemblyFromDomBatch().
			CompilerResults results = this.mCompiler.CompileAssemblyFromFileBatch(options, fileNames);
			CheckForFixup(this.mCmdLineResults);
			IsExceptionCatchingOK(this.mCmdLineResults, results);
			return results;
		}

		public System.CodeDom.Compiler.CompilerResults CompileAssemblyFromSource(System.CodeDom.Compiler.CompilerParameters options, string source)
		{
			return this.mCompiler.CompileAssemblyFromSource(options, source);
		}

		public System.CodeDom.Compiler.CompilerResults CompileAssemblyFromDom(System.CodeDom.Compiler.CompilerParameters options, System.CodeDom.CodeCompileUnit compilationUnit)
		{
			return this.mCompiler.CompileAssemblyFromDom(options, compilationUnit);
		}

		public System.CodeDom.Compiler.CompilerResults CompileAssemblyFromSourceBatch(System.CodeDom.Compiler.CompilerParameters options, string[] sources)
		{
			return this.mCompiler.CompileAssemblyFromSourceBatch(options, sources);
		}
		#endregion

		private bool IsExceptionCatchingOK(CommandLineResults cmdResults,
			CompilerResults results)
		{
			bool retVal = true;
			//  Look at the assembly and go through each of the types and
			//  their corresponding methods.
			Assembly newAssembly = results.CompiledAssembly;

			//  For each method, parse the CIL, and look for call, calli, and
			//  callvirt calls. If that method is tagged with [Throws],
			//  it must be caught or the method itself must be tagged.
			Type[] assemblyTypes = newAssembly.GetTypes();
			
			foreach(Type t in assemblyTypes)
			{
				BindingFlags methodBinding = BindingFlags.Public |
					BindingFlags.NonPublic | BindingFlags.Instance |
					BindingFlags.Static;

				MethodInfo[] typeMethods = t.GetMethods(methodBinding);

				if(typeMethods != null && typeMethods.Length > 0)
				{
					foreach(MethodInfo mi in typeMethods)
					{
						UnhandledExceptions uEx = new UnhandledExceptions(mi);

						if(uEx.ExceptionList.Count > 0)
						{
							//  We need to report this to the user.
							retVal = false;

							foreach(Type ex in uEx.ExceptionList)
							{
								results.Errors.Add(new CompilerError(newAssembly.Location, 
									0, 0, ERROR_UNHANDLED_EXCEPTION_CODE, 
									string.Format(ERROR_UNHANDLED_EXCEPTION_DESCRIPTION, 
									mi.Name, t.Name, ex.FullName)));
							}
						}
					}
				}
			}

			return retVal;
		}

		private void CheckForFixup(CommandLineResults results)
		{
			if(results.Fixup == true)
			{
				//  Find the output location
				string currentLocation = results.Parameters.OutputAssembly;

				//  Find the main method and its' corresponding class.
				Assembly currentAssembly = Assembly.LoadFrom(
					currentLocation);

				string mainClass = currentAssembly.EntryPoint.DeclaringType.Name;

				//  Move the file to its' new location w/ its' new name.
				string newFile = Path.Combine(Directory.GetCurrentDirectory(), 
					string.Format("{0}.exe", mainClass));

				if(File.Exists(newFile) == true)
				{
					File.Delete(newFile);
				}

				File.Move(currentLocation, newFile);

				results.Parameters.OutputAssembly = newFile;
			}
		}
	}
}

