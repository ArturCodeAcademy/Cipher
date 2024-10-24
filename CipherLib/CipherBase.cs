namespace CipherLib
{
	public abstract class CipherBase : ICipher
	{
		public virtual int KeyLength => 0;
		public byte[] KeyArray { get; }

		public CipherBase(byte[] key)
		{
			KeyArray = key;
			OnKeyAsigned();
		}

		public CipherBase(byte[] key, int index)
		{
			KeyArray = new byte[KeyLength];
			Array.Copy(key, index, KeyArray, 0, KeyLength);
			OnKeyAsigned();
		}

		protected virtual void OnKeyAsigned()
		{

		}

		public abstract IEnumerable<char> Encript(IEnumerable<char> message);

		public abstract IEnumerable<char> Decript(IEnumerable<char> message);
	}
}
