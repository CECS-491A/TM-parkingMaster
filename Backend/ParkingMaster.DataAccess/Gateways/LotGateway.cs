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
        LotContext context;

        public LotGateway()
        {
            context = new LotContext();
        }

        public LotGateway(LotContext c)
        {
            context = c;
        }

        public ResponseDTO<Boolean> AddLot(Lot lot)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Lots.Add(lot);
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

        public ResponseDTO<Boolean> AddSpots(List<Spot> spots)
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

        // Delete lot - needs cascading delete to delete spots as well
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
