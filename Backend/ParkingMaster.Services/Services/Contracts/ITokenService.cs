using System;

namespace ParkingMaster.Services.Services
{
    public interface ITokenService
    {
        string GenerateToken();
        bool isValidSignature(string preSig, string sig);
        string GenerateSignature(string plaintext);
    }
}
