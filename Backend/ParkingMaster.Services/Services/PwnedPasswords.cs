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
            var response = client.GetAsync("range/" + prefix).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return result.Split(new string[] { Environment.NewLine }, StringSplitOptions.None); // splits string response on current environment's newline format
            }

            client.Dispose();
            return new string[0];
        }

    }
}
