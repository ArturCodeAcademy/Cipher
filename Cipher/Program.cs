using CipherLib;
using System.Text;
using System.Threading.Channels;

string[] cipherNames = CipherManager.Ciphers.Keys.ToArray();

Console.WriteLine("Available ciphers:");
for (int i = 0; i < cipherNames.Length; i++)
	Console.WriteLine($"{i + 1}. {cipherNames[i]}");

Console.WriteLine("Enter cipher indexes separated by space:");
string[] indexes = Console.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);

IEnumerable<string> GetSelectedNames(string[] names, string[] indexes)
{
	foreach (string index in indexes)
	{
		if (int.TryParse(index, out int i) && i > 0 && i <= names.Length)
		{
			yield return names[i - 1];
		}
	}
}

IEnumerable<string> names = GetSelectedNames(cipherNames, indexes);

Console.Write("Enter key: ");
string keyStr = Console.ReadLine()!;
byte[] key = Encoding.UTF8.GetBytes(keyStr);

CipherManager manager = CipherManager.CreateCipher(key, names);

Console.Write("Enter message: ");
string message = Console.ReadLine()!;
IEnumerable<char> encripted = manager.Encript(message);

Console.WriteLine("Encripted message:");
Console.WriteLine(string.Join("", encripted));

Console.WriteLine("Decripted message:");
IEnumerable<char> decripted = manager.Decript(encripted);
Console.WriteLine(string.Join("", decripted));