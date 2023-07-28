using SOStringCipher;

namespace StringEncryptorConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Encrypt and decrypt string");

            var menuItem = 0;
            do
            {
                menuItem = ChooseMenu();

                if (menuItem == 1)
                {
                    Encrypt();
                }
                else if (menuItem == 2)
                {
                    Decrypt();
                }
            }
            while (menuItem > 0);
        }

        private static int ChooseMenu()
        {
            Console.WriteLine("1 -> Encrypt");
            Console.WriteLine("2 -> Decrypt");
            Console.WriteLine("0 -> Exit");

            return int.Parse(Console.ReadLine());
        }

        private static void Encrypt()
        {
            Console.WriteLine("Plain text to encrypt:");
            var plainText = Console.ReadLine();

            Console.WriteLine("Password for the encryption:");
            var password = Console.ReadLine();

            Console.WriteLine("The encrypted string:");
            var encrypted = StringCipher.Encrypt(plainText, password);

            Console.WriteLine(encrypted);
        }

        private static void Decrypt()
        {
            Console.WriteLine("Encrypted text to decrypt:");
            var encrypted = Console.ReadLine();

            Console.WriteLine("Password for the encryption:");
            var password = Console.ReadLine();

            Console.WriteLine("The plain text is:");
            var plainText = StringCipher.Decrypt(encrypted, password);

            Console.WriteLine(plainText);
        }
    }
}