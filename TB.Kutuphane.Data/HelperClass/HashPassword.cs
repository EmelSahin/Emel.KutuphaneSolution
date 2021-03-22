using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TB.Kutuphane.Data.HelperClass
{
    public static class HashPassword
    {
        private static string MD5(this string parola)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] dizi = md5.ComputeHash(Encoding.UTF8.GetBytes(parola));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < dizi.Length; i++)
            {
                stringBuilder.Append(dizi[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        private static string SHA_1(this string parola)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] dizi = sha1.ComputeHash(Encoding.UTF8.GetBytes(parola));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < dizi.Length; i++)
            {
                stringBuilder.Append(dizi[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public static string Passwording(this string parola)
        {
            parola = parola.SHA_1();
            parola = parola.MD5();
            parola = parola.SHA_1();
            parola = parola.MD5();
            return parola;
        }

        public static string newCreatePassword(int numberOfCharacters)
        {
            var chars = "QWERTYUIOPLKJHGFDSAZXCVBNM0123654789";
            var random = new Random();
            var result = new string(Enumerable
                .Repeat(chars, numberOfCharacters)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
            return result;
        }
    }
}
