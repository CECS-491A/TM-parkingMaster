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
    public class VehicleGateway : IDisposable
    {
        // Open the User context
        UserContext context;

        public VehicleGateway()
        {
            context = new UserContext();
        }

        public VehicleGateway(UserContext c)
        {
            context = c;
        }

        //Store a user
        public ResponseDTO<bool> StoreVehicle(Vehicle vehicle)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {

                    // Add Vehicle
                    context.Vehicles.Add(vehicle);

                    context.SaveChanges();

                    // Commit transaction to database
                    dbContextTransaction.Commit();

                    // Return a true ResponseDto
                    return new ResponseDTO<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
                    // Rolls back the changes saved in the transaction
                    dbContextTransaction.Rollback();
                    // Returns a false ResponseDto
                    return new ResponseDTO<bool>()
                    {
                        Data = false,
                        Error = "[DATA ACCESS] Could not add Lot."
                    };
                }
            }
        }

        // returns vehicle via responseDTO
        public ResponseDTO<VehicleDTO> GetVehicleByGUID(Guid id)
        {

            using(var dbContextTransaction = context.Database.BeginTransaction())
            {
                // Find vehicle associated with account
                var userVehicle = (from vehicle in context.Vehicles
                                   where vehicle.UserAccount.Id == id
                                   select vehicle).SingleOrDefault();

                ResponseDTO<VehicleDTO> responseDto = new ResponseDTO<VehicleDTO>
                {
                    Data = new VehicleDTO(userVehicle),
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
