using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;

namespace ParkingMaster.Manager.Managers.Contracts
{
    public interface ILoginManager
    {
        ResponseDTO<Session> SsoLogin(SsoUserRequestDTO request);
    }
}