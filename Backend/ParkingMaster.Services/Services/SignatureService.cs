﻿using System;
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
    public class SignatureService : ISignatureService
    {
        private readonly string APISecret = "07D3E72B11AD4F056AAAC480A8896DC8EEF9AD684AF2F69428FC9E35EBDE713B";
        //secret key made on local apps, for dev only

        public bool isValidSignature(Dictionary<string, string> presignatureString, string signature)
        {
            return Sign(presignatureString) == signature;
        }

        public bool isValidSignature(string preSig, string sig)
        {
            throw new NotImplementedException();
        }

        public string Sign(string plaintext)
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

        // Signs a dictionary with the provided key by constructing a key/value string
        public string Sign(Dictionary<string, string> payload)
        //public string Sign(string key, dynamic payload)
        {
            // Order the provided dictionary by key
            // This is necessary so that the recipient of the payload will be able to generate the
            // correct hash even if the order changes
            var orderedPayload = from payloadItem in payload
                                 orderby payloadItem.Value descending
                                 select payloadItem;

            var payloadString = "";
            // Build a payload string with the format:
            // key =value;key2=value2;
            // SECURITY: This must be passed in this format so that the resulting hash is the same
            foreach (KeyValuePair<string, string> pair in orderedPayload)
            {
                payloadString = payloadString + pair.Key + "=" + pair.Value + ";";
            }

            var signature = Sign(payloadString);
            return signature;
        }
    }
}
