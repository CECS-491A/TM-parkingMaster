using System;
using ParkingMaster.Models.DTO;

namespace ParkingMaster.Services.Services
{
    public interface ITokenService
    {
        bool isValidSignature(string preSig, string sig);
        string GenerateSignature(string plaintext);
    }
}
