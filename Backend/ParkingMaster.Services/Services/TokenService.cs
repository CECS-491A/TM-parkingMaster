using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using ParkingMaster.DataAccess;

namespace ParkingMaster.Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly string APISecret = "07D3E72B11AD4F056AAAC480A8896DC8EEF9AD684AF2F69428FC9E35EBDE713B";
                                            //secret key made on local apps, for dev only

        public bool isValidSignature(string presignatureString, string signature)
        {
            return GenerateSignature(presignatureString) == signature;
        }

        public string GenerateSignature(string plaintext)
        {
            // Instantiate a new hashing algorithm with the provided key
            HMACSHA256 hashingAlg = new HMACSHA256(Encoding.ASCII.GetBytes(APISecret));

            // Get the raw bytes from our payload string
            byte[] payloadBuffer = Encoding.ASCII.GetBytes(plaintext);

            // Calculate our hash from the byte array
            byte[] signatureBytes = hashingAlg.ComputeHash(payloadBuffer);

            var signature = Convert.ToBase64String(signatureBytes);
            return signature;
        }
    }
}
