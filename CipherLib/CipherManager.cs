namespace CipherLib
{
	public partial class CipherManager
	{
		private List<CipherBase> _ciphers;

		private CipherManager()
		{
			_ciphers = new();
		}

		public IEnumerable<char> Encript(IEnumerable<char> message)
		{
			IEnumerable<char> result = message;
			foreach (CipherBase cipher in _ciphers)
			{
				result = cipher.Encript(result);
			}

			return result;
		}

		public IEnumerable<char> Decript(IEnumerable<char> message)
		{
			IEnumerable<char> result = message;
			for (int i = _ciphers.Count - 1; i >= 0; i--)
			{
				result = _ciphers[i].Decript(result);
			}

			return result;
		}
	}
}
