namespace CipherLib
{
	public class DictionaryCipher : CipherBase
	{
		public override int KeyLength => 4;
		public int Key { get; private set; }

		private Dictionary<char, char> _encriptDictionary = default!;
		private Dictionary<char, char> _decriptDictionary = default!;

		private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

		public DictionaryCipher(byte[] key) : base(key)
		{
		}

		public DictionaryCipher(byte[] key, int index) : base(key, index)
		{
		}

		protected override void OnKeyAsigned()
		{
			Key = BitConverter.ToInt32(KeyArray, 0);
			Random random = new Random(Key);

			char[] symbols = ALPHABET.ToCharArray();
			char[] shuffled = symbols.OrderBy(x => random.Next()).ToArray();

			_encriptDictionary = new();
			_decriptDictionary = new();

			for (int i = 0; i < symbols.Length; i++)
			{
				_encriptDictionary[symbols[i]] = shuffled[i];
				_decriptDictionary[shuffled[i]] = symbols[i];
			}
		}

		public override IEnumerable<char> Decript(IEnumerable<char> message)
		{
			foreach(char c in message)
			{
				if (_decriptDictionary.TryGetValue(c, out char value))
					yield return value;
				else
					yield return c;
			}
		}

		public override IEnumerable<char> Encript(IEnumerable<char> message)
		{
			foreach (char c in message)
			{
				if (_encriptDictionary.TryGetValue(c, out char value))
					yield return value;
				else
					yield return c;
			}
		}
	}
}
