using System;
using System.Collections.Generic;
using ParkingMaster.DataAccess;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;

namespace ParkingMaster.Services.Services
{
    public class TermsOfServiceService
    {
        TosGateway _tosGateway;

        public TermsOfServiceService()
        {
            _tosGateway = new TosGateway();
        }

        public ResponseDTO<bool> AddAndSetNewTos(TermsOfService tos)
        {
            return _tosGateway.AddAndSetNewTos(tos);
        }

        public ResponseDTO<bool> ChangeActiveTos(Guid tosId)
        {
            return _tosGateway.ChangeActiveTos(tosId);
        }

        public ResponseDTO<TermsOfService> GetActiveTermsOfService()
        {
            return _tosGateway.GetActiveTermsOfService();
        }

        public ResponseDTO<List<TermsOfService>> GetAll()
        {
            return _tosGateway.GetAll();
        }
    }
}
