namespace CipherLib
{
	public interface ICipher
	{
		int KeyLength { get; }
		byte[] KeyArray { get; }
	}
}
