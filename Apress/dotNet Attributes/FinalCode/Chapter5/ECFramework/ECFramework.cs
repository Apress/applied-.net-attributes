using System;
using System.Reflection;

namespace ECFramework
{
	[AttributeUsage(AttributeTargets.Method |
		 AttributeTargets.Property, AllowMultiple = true, 
		 Inherited = true)]
	public sealed class ThrowsAttribute : Attribute
	{
		private const string ERROR_NOT_SUBCLASS = "The given type is not an Exception type.";
		private const string PARAM_EXCEPTION_TYPE = "exceptionType";

		private Type mExceptionType = null;

		private ThrowsAttribute() : base() {}

		public ThrowsAttribute(Type exceptionType) : base()
		{
			//  Precondition: exceptionType must not be null.
			if(exceptionType == null)
			{
				throw new ArgumentNullException(PARAM_EXCEPTION_TYPE);
			}

			//  Precondition: exceptionType must be an Exception type.
			Type baseExType = typeof(Exception);
			if(exceptionType != baseExType &&
				exceptionType.IsSubclassOf(typeof(Exception)) == false)
			{
				throw new ArgumentException(ERROR_NOT_SUBCLASS, 
					PARAM_EXCEPTION_TYPE);
			}

			this.mExceptionType = exceptionType;
		}

		public Type ExceptionType
		{
			get
			{
				return this.mExceptionType;
			}
		}
	}
}
