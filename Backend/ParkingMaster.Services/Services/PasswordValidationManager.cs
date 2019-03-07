using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Services.Services
{
    public class PasswordValidationManager
    {
        PasswordService ps = new PasswordService();

        public string GetHashedPw(string pw)
        {
            return ps.Sha1Hash(pw);
        }
        public string CreatePrefix(string hashed)
        {
            return hashed.Substring(0, 5);
        }
        public string CreateSuffix(string hashed)
        {
            return hashed.Substring(5);
        }
        public int CheckPassword(string hashed)
        {
            if (string.IsNullOrEmpty(hashed))
            {
                return -1;
            }
            string prefix = CreatePrefix(hashed);
            string suffix = CreateSuffix(hashed);
            var results = ps.GetPwnedPasswords(prefix);
            if (results.Length == 0) // Handles the possibility of an empty array being returned into hashResults
            {
                return -1;
            }
            foreach (string s in results)
            {
                var pair = s.Split(':');
                if (pair[0].Equals(suffix, StringComparison.InvariantCultureIgnoreCase))
                {
                    return Int32.Parse(pair[1]);
                }

            }
            return 0;
        }
    }
}
