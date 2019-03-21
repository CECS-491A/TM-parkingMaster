using ParkingMaster.DataAccess.Context;
using ParkingMaster.Models.Models;
using ParkingMaster.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess.Gateways
{
    public class LotGateway : IDisposable
    {
        UserContext context;

        public LotGateway()
        {
            context = new UserContext();
        }

        public LotGateway(UserContext c)
        {
            context = c;
        }

        public ResponseDTO<Boolean> AddLot(Lot lot, List<Spot> spotList)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Lots.Add(lot);

                    foreach (Spot spot in spotList)
                    {
                        context.Spots.Add(spot);
                    }

                    context.SaveChanges();

                    dbContextTransaction.Commit();

                    return new ResponseDTO<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();

                    return new ResponseDTO<bool>()
                    {
                        Data = false,
                        Error = "Failed to add lot to data store."
                    };
                }
            }
        }

        public ResponseDTO<Boolean> DeleteLot(Lot lot)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Lots.Remove(lot);
                    context.SaveChanges();

                    dbContextTransaction.Commit();

                    return new ResponseDTO<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();

                    return new ResponseDTO<bool>()
                    {
                        Data = false,
                        Error = "Failed to delete lot from data store."
                    };
                }
            }
        }

        public ResponseDTO<Boolean> AddSpots(List<Spot> spots) // will delete this, ignore
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (Spot spot in spots)
                    {
                        context.Spots.Add(spot);
                    }
                    context.SaveChanges();

                    dbContextTransaction.Commit();

                    return new ResponseDTO<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();

                    return new ResponseDTO<bool>()
                    {
                        Data = false,
                        Error = "Failed to add spots to data store."
                    };
                }
            }
        }



        // TO DO

        // Edit lot
        // Delete spot(s) - though I think an EditSpots would suffice
        // GetLotByName (?)
        // GetAllLots
        // GetAllSpotsInLot
        // GetAllSpots - get all lots then get all spots? need to figure this out

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
