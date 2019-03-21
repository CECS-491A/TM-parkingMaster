using System;


namespace ParkingMaster.Services.Services
{
    public interface IPasswordService
    {
        string Sha1Hash(string pw);
        string RfcHashPassword(string pw, byte[] salt);
        byte[] GetSalt();
        string[] GetPwnedPasswords(string prefix);
        int CheckPassword(string pw);
        
    }
}
