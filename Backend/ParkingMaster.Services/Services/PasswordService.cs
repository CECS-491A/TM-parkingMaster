using System;
using System.Security.Cryptography;
using System.Text;

namespace ParkingMaster.Services
{
    public class PasswordService : IPasswordService
    {

        /** PWNED PASSWORDS API USES SHA1 HASH **/
        public string Sha1Hash(string pw)
        {
            if(pw == null)
            {
                return Sha1Hash("");
            }
            byte[] temp;
            SHA1Cng sha = new SHA1Cng();
            temp = sha.ComputeHash(Encoding.UTF8.GetBytes(pw));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < temp.Length; i++)
            {
                sb.Append(temp[i].ToString("x2")); // X2 formats each char into two hex characters
            }

            return sb.ToString();
        }

        /** Rfc2898 key derive for securing stored passwords **/
        public string RfcHashPassword(string pw, byte[] salt)
        {
            using (Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(pw, salt))
            {
                key.IterationCount = 10000;
                byte[] hashedPw = key.GetBytes(16);
                return Convert.ToBase64String(hashedPw);
            }

        }

        /** Salt generating method to be saved and stored **/
        public byte[] GetSalt()
        {
            byte[] salt = new byte[16];
            using (RNGCryptoServiceProvider random = new RNGCryptoServiceProvider())
            {
                random.GetBytes(salt);
            }
            return salt;
        }

        public string[] GetPwnedPasswords(string prefix)
        {
            return new PwnedPasswords().GetHashListByPrefix(prefix);
        }
    }
}
