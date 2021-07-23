namespace ConsoleAppTask1.Services
{
    public class EncryptionService
    {
        private const int lettersCount = 26;
        //abcdefghijklmnopqrstuvwxyz
        //12345678901234567890123456

        public string Decrypt(string message, int key)
        {
            return Encrypt(message, lettersCount - key);
        }

        public string Encrypt(string message, int key)
        {
            var result = string.Empty;

            foreach (var symbol in message)
            {
                result += GetCipher(symbol, key);
            }

            return result;

            char GetCipher(char value, int key)
            {
                if (!char.IsLetter(value))
                {
                    return value;
                }

                char offset = char.IsUpper(value) ? 'A' : 'a'; //КАПЕЦ
                return (char)((((value + key) - offset) % lettersCount) + offset);
            }
        }
    }
}
