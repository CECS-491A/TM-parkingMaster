using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess
{
   //Vehicle Queries
    public class VehicleGateway
    {
        // Open the User context
        UserContext context = new UserContext();

    
        // returns vehicle via responseDTO
        public ResponseDto<VehicleDTO> GetVehicleByGUID(guid id)
        {

            using (var vehicleContext = new vehicleContext())
            {
                // Find vehicle associated with account
                var userVehicle = (from vehicle in vehicleContext.Vehicle
                                   where vehicle.UserAccount.Id == id
                                   select vehicle).SingleOrDefault();

                ResponseDto<vehicleDTO> responseDto = new ResponseDto<vehicleDTO>
                {
                    Data = new vehicleDTO(userVehicle),
                    Error = null
                };

                return responseDto;
            }
        }

        //update, true if successful
        /*
        TODO: MANAGER
        public ResponseDto<bool> EditVehicleByGUID(guid userGUID, Vehicle vehicleDomain)
        {
            using (var vehicleContext = new vehicleContext())
            {
                var userVehicle = (from vehicle in vehicleContext.UserAccount
                                    where vehicle.Id == userAccountId
                                    select vehicle).SingleOrDefault();

                using (var dbContextTransaction = vehicleContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Apply and save changes
                        userVehcile.DisplayName = vehicleDomain.DisplayName;
                        vehicleContext.SaveChanges();
                        dbContextTransaction.Commit();

                        ResponseDto<bool> responseDto = new ResponseDto<bool>
                        {
                            Data = true,
                            Error = null
                        };
                        return responseDto;
                    }

                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();

                        ResponseDto<bool> responseDto = new ResponseDto<bool>
                        {
                            Data = false,
                        };
                        return responseDto;
                    }
                }
            }
        }
*/ 
    
        void IDisposable.Dispose()
        {
            context.Dispose();
        }

    }
}
