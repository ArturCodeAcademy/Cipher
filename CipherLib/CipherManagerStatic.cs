using System.Collections.Immutable;
using System.Reflection;

namespace CipherLib
{
	public partial class CipherManager
	{
		public static ImmutableDictionary<string, Type> Ciphers { get; private set; }

		static CipherManager()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			Dictionary<string, Type> ciphers = new();

			foreach (Assembly assembly in assemblies)
			{
				foreach (Type type in assembly.GetTypes())
				{
					if (type.IsSubclassOf(typeof(CipherBase)) && !type.IsAbstract)
					{
						ciphers.Add(type.Name, type);
					}
				}
			}

			Ciphers = ciphers.ToImmutableDictionary();
		}

		public static CipherManager CreateCipher(byte[] key, IEnumerable<string> names)
		{
			CipherManager manager = new();
			int index = 0;
			foreach (string name in names)
			{
				CipherBase? cipher = CreateCipher(name, key, index);
				if (cipher != null)
				{
					manager._ciphers.Add(cipher);
					index += cipher.KeyLength;
				}
			}

			return manager;
		}

		private static CipherBase? CreateCipher(string name, byte[] key, int index)
		{
			if (Ciphers.TryGetValue(name, out Type? type))
			{
				return (CipherBase)Activator.CreateInstance(type, key, index)!;
			}

			return null;
		}
	}
}
