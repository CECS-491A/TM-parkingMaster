using System;
using System.Net.Http;

namespace ParkingMaster.Services
{
    public class PwnedPasswords
    {

        string baseUri = "https://api.pwnedpasswords.com/";

        public string[] GetHashListByPrefix(string prefix)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUri);
            var response = client.GetStringAsync("range/" + prefix).Result;
            client.Dispose();
            return response.Split(new string[] { Environment.NewLine }, StringSplitOptions.None); // splits string response on current environment's newline format
        }

    }
}
