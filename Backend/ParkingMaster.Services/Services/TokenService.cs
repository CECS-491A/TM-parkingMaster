using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly string APISecret = "F74D9544CB5CA6F8F3A9F07DDE1EC75C102E013967364D52D6CE1CD3182029BA";
                                            //secret key made on local apps, for dev only
        public string GenerateToken()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            Byte[] b = new byte[64 / 2];
            provider.GetBytes(b);
            string hex = BitConverter.ToString(b).Replace("-", "");
            return hex;
        }

        public bool isValidSignature(string presignatureString, string signature)
        {
            return GenerateSignature(presignatureString) == signature;
        }

        public string GenerateSignature(string plaintext)
        {
            HMACSHA256 hmacsha1 = new HMACSHA256(Encoding.ASCII.GetBytes(APISecret));
            byte[] signatureBuffer = Encoding.ASCII.GetBytes(plaintext);
            byte[] signatureBytes = hmacsha1.ComputeHash(signatureBuffer);
            return Convert.ToBase64String(signatureBytes);
        }
    }
}
