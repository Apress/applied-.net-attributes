using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;

namespace Apress.NetAttributes
{
	public interface IObjectStorage
	{
		void Load(IPersist targetObject, FileStream stream);
		void Save(IPersist targetObject, FileStream stream);
	}

	public class ObjectPersist
	{
		public void Load(IPersist targetObject, FileStream stream)
		{
			FieldInfo[] fields = targetObject.GetType().GetFields(
				BindingFlags.Public | BindingFlags.NonPublic | 
				BindingFlags.Instance);

			foreach(FieldInfo field in fields)
			{
				string fieldName = field.Name;
				object fieldValue = field.GetValue(targetObject);
			}
		}

		public void Save(IPersist targetObject, FileStream stream)
		{

		}
	}

	public interface IPersist
	{
		void Load(Stream persistenceTarget);
		void Load(Hashtable nameValuePairs);
		void Save(Stream persistenceTarget);
		void Save(Hashtable nameValuePairs);
	}

	[type: Obsolete("This class should no longer be used - switch to ImprovedCountry.", 
		false)]
	public class BadCountry
	{
		public string mName;
		public long mPopulation;

		public BadCountry() : base() {}

		[property: Obsolete("This is a bad property.", 
			true)]
		public long Population
		{
			get
			{
				return this.mPopulation;
			}
			set
			{
				if(value < 0 || 
					value > 5000000000000)
				{
					throw new ArgumentOutOfRangeException(
						"value", value, 
						"The given value is out of range.");
				}

				this.mPopulation = value;
			}
		}

		public string Name
		{
			get
			{
				return this.mName;
			}
			set
			{
				this.mName = value;
			}
		}
	}

	public class BadInheritedCountry : BadCountry {}

	public class ImprovedCountry
	{
		private const string ERROR_ARGUMENT_NAME = "name";
		private const string ERROR_ARGUMENT_POPULATION = "population";
		private const string ERROR_MESSAGE_NULL_VALUE = "The given value should not be null.";
		private const string ERROR_MESSAGE_OUT_OF_RANGE = "The given value is out of range.";
		public const long MINIMUM_POPULATION = 0;
		public const long MAXIMUM_POPULATION = 5000000000000;

		protected string mName = string.Empty;
		protected long mPopulation;

		private ImprovedCountry() : base() {}

		public ImprovedCountry(string name, long population)
		{
			this.CheckInvariantName(name);
			this.CheckInvariantPopulation(population);

			this.mName = name;
			this.mPopulation = population;
		}

		protected void CheckInvariantName(string name)
		{
			if(name == null)
			{
				throw new ArgumentNullException(
					ImprovedCountry.ERROR_ARGUMENT_NAME, 
					ImprovedCountry.ERROR_MESSAGE_NULL_VALUE);
			}
		}

		protected void CheckInvariantPopulation(long population)
		{
			if(population < ImprovedCountry.MINIMUM_POPULATION || 
				population > ImprovedCountry.MAXIMUM_POPULATION)
			{
				throw new ArgumentOutOfRangeException(
					ImprovedCountry.ERROR_ARGUMENT_POPULATION, population, 
					ImprovedCountry.ERROR_MESSAGE_OUT_OF_RANGE);
			}
		}

		public long Population
		{
			get
			{
				return this.mPopulation;
			}
			set
			{
				this.CheckInvariantPopulation(value);
				this.mPopulation = value;
			}
		}

		public string Name
		{
			get
			{
				return this.mName;
			}
			set
			{
				this.CheckInvariantName(value);
				this.mName = value;
			}
		}
	}

	public class PersistedCountry : ImprovedCountry, IPersist
	{
		private const string NAME_KEY = "name";
		private const string POPULATION_KEY = "population";

		public PersistedCountry(string name, long population) : 
			base(name, population) {}

		public void Load(Hashtable nameValuePairs)
		{
			if(nameValuePairs != null)
			{
				if(nameValuePairs.Contains(NAME_KEY))
				{
					this.mName = (string)nameValuePairs[NAME_KEY];
				}
				if(nameValuePairs.Contains(POPULATION_KEY))
				{
					this.mPopulation = (long)nameValuePairs[POPULATION_KEY];
				}
			}
		}

		public void Load(Stream persistenceTarget)
		{
			int totalBytes = (int)persistenceTarget.Length;
			Decoder dec = (new UnicodeEncoding()).GetDecoder();
			byte[] storedInfo = new byte[totalBytes];
			persistenceTarget.Read(storedInfo, 0, totalBytes);
			char[] storedChars = new char[dec.GetCharCount(storedInfo, 0, totalBytes)];
			int totalDecodedChars = dec.GetChars(storedInfo, 0, totalBytes,
				storedChars, 0);
			string info = new string(storedChars);
			
			int nameStart = info.IndexOf(NAME_KEY);
			int populationStart = info.IndexOf(POPULATION_KEY);

			this.mName = info.Substring(nameStart + NAME_KEY.Length + 1, 
				populationStart - (nameStart + NAME_KEY.Length));
			this.mPopulation = Int32.Parse(
				info.Substring(populationStart + POPULATION_KEY.Length + 1));
		}

		public void Save(Hashtable nameValuePairs)
		{
			if(nameValuePairs != null)
			{
				nameValuePairs.Add(NAME_KEY, this.mName);
				nameValuePairs.Add(POPULATION_KEY, this.mPopulation);
			}
		}

		public void Save(Stream persistenceTarget)
		{
			Encoding enc = new UnicodeEncoding();
			string nameInfo = string.Format("{0}:{1}", NAME_KEY, this.mName);
			byte[] nameInfoBytes = enc.GetBytes(nameInfo);
			persistenceTarget.Write(nameInfoBytes, 0, 
				nameInfoBytes.Length);
			string populationInfo = string.Format("{0}:{1}", POPULATION_KEY, this.Population);
			byte[] populationInfoBytes = enc.GetBytes(populationInfo);
			persistenceTarget.Write(populationInfoBytes, 0, 
				populationInfoBytes.Length);
		}
	}

	[type: Serializable]
	public class SerializableCountry
	{

	}
}


