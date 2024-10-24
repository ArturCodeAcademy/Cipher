namespace CipherLib
{
	public class RandomInsertionCipher : CipherBase
	{
		public override int KeyLength => 1;
		public byte Key { get; private set; }

		private static string RANDOM_SYMBOLS =
			"ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
			"abcdefghijklmnopqrstuvwxyz" +
			"0123456789" +
			"!@#$%^&*()_+-=[]{};':,.<>?";

		public RandomInsertionCipher(byte[] bytes) : base(bytes)
        {
            
        }

		public RandomInsertionCipher(byte[] bytes, int index) : base(bytes, index)
		{
			
		}

		protected override void OnKeyAsigned()
		{
			Key = KeyArray[0];
		}

        public override IEnumerable<char> Decript(IEnumerable<char> message)
		{
			IEnumerator<char> enumerator = message.GetEnumerator();
			while (enumerator.MoveNext())
			{
				yield return enumerator.Current;

				for (int i = 0; i < Key && enumerator.MoveNext(); i++);
			}
		}

		public override IEnumerable<char> Encript(IEnumerable<char> message)
		{
			foreach (char c in message)
			{
				yield return c;

				for (int i = 0; i < Key; i++)
				{
					int index = Random.Shared.Next(RANDOM_SYMBOLS.Length);
					yield return RANDOM_SYMBOLS[index];
				}
			}
		}
	}
}
