namespace CipherLib
{
	public class CaesarCipher : CipherBase
	{
		public override int KeyLength => 4;

		public int Key { get; private set; }

		private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

		public CaesarCipher(byte[] key) : base(key)
		{

		}

		public CaesarCipher(byte[] key, int index) : base(key, index)
		{

		}

		protected override void OnKeyAsigned()
		{
			Key = BitConverter.ToInt32(KeyArray, 0) % ALPHABET.Length;
		}

		public override IEnumerable<char> Decript(IEnumerable<char> message)
		{
			foreach (char c in message)
			{
				yield return Shift(c, -Key);
			}
		}

		public override IEnumerable<char> Encript(IEnumerable<char> message)
		{
			foreach (char c in message)
			{
				yield return Shift(c, Key);
			}
		}

		private char Shift(char c, int shift)
		{
			int index = ALPHABET.IndexOf(c);
			if (index == -1)
			{
				return c;
			}
			return ALPHABET[(index + shift + ALPHABET.Length) % ALPHABET.Length];
		}
	}
}
