using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hash
{
    class Program
    {
        static void Main(string[] args)
        {
            string salt = GenerateSalt(64);
            string password = GenerateSHA256("helloworld" + salt);


            Console.WriteLine(salt);
            Console.WriteLine("\n");
            Console.WriteLine(password);
        }
        public static string GenerateSalt(int len)
        {

            const string valid = "AbCdEfGhIjKlMnOpQrStUvWxYzaBcDeFgHiJkLmNoPqRsTuVwXyZ09876543211234567890";
            StringBuilder res = new StringBuilder();

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (len-- > 0)
                {

                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);

                }
            }
            return res.ToString();

        }
        public static string GenerateSHA256(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            SHA256Managed hashedString = new SHA256Managed();
            byte[] hash = hashedString.ComputeHash(bytes);
            string hashedStrings = String.Empty;

            foreach (byte b in hash)
                hashedStrings += String.Format("{0:X2}" , b);
            return hashedStrings;
        }
            

    }
}

