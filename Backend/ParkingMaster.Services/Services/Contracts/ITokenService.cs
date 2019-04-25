﻿using System;
using System.Collections.Generic;
using ParkingMaster.Models.DTO;

namespace ParkingMaster.Services.Services
{
    public interface ITokenService
    {
        bool isValidSignature(Dictionary<string, string> preSig, string sig);
        string Sign(Dictionary<string, string> plaintext);
    }
}
