using SOStringCipher;

namespace StringEncryptorConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("String encrypt / decrypt console");

            int menuItem;
            do
            {
                menuItem = ChooseMenu();

                if (menuItem == 1)
                {
                    Encrypt(OutputFormat.Base64);
                }
                else if (menuItem == 2)
                {
                    Decrypt(OutputFormat.Base64);
                }
                else if (menuItem == 3)
                {
                    Encrypt(OutputFormat.Base62);
                }
                else if (menuItem == 4)
                {
                    Decrypt(OutputFormat.Base62);
                }
                else if (menuItem == 5)
                {
                    Encrypt(OutputFormat.Base36);
                }
                else if (menuItem == 6)
                {
                    Decrypt(OutputFormat.Base36);
                }
                else if (menuItem == 7)
                {
                    Encrypt(OutputFormat.Hex);
                }
                else if (menuItem == 8)
                {
                    Decrypt(OutputFormat.Hex);
                }
            }
            while (menuItem > 0);
        }

        private static int ChooseMenu()
        {
            Console.WriteLine("1 -> Encrypt with output Base64");
            Console.WriteLine("2 -> Decrypt from output Base64");
            Console.WriteLine("3 -> Encrypt with output Base62");
            Console.WriteLine("4 -> Decrypt from output Base62");
            Console.WriteLine("5 -> Encrypt with output Base36");
            Console.WriteLine("6 -> Decrypt from output Base36");
            Console.WriteLine("7 -> Encrypt with output HEX");
            Console.WriteLine("8 -> Decrypt from output HEX");
            Console.WriteLine("0 -> Exit");

            return int.Parse(Console.ReadLine() ?? "0");
        }

        private static void Encrypt(OutputFormat outputFormat)
        {
            Console.WriteLine("Plain text to encrypt:");
            var plainText = Console.ReadLine();

            Console.WriteLine("Password for the encryption:");
            var password = Console.ReadLine();

            Console.WriteLine("The encrypted string:");
            var encrypted = StringCipher.Encrypt(plainText, password, outputFormat);

            Console.WriteLine(encrypted);
        }

        private static void Decrypt(OutputFormat outputFormat)
        {
            Console.WriteLine("Encrypted text to decrypt:");
            var encrypted = Console.ReadLine();

            Console.WriteLine("Password for the encryption:");
            var password = Console.ReadLine();

            Console.WriteLine("The plain text is:");
            var plainText = StringCipher.Decrypt(encrypted, password, outputFormat);

            Console.WriteLine(plainText);
        }
    }
}