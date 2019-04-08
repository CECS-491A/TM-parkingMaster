//using ParkingMaster.DataAccess.Context; // giving error for some reason
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
        UserContext context; // used to be LotContext

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
                        Error = "[DATA ACCESS] Failed to add lot to data store."
                    };
                }
            }
        }

        public ResponseDTO<Boolean> DeleteLot(Guid ownerid, string lotname)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var deletelot = (from lot in context.Lots
                               where lot.OwnerId == ownerid &&
                               lot.LotName == lotname
                               select lot).FirstOrDefault();
                    context.Lots.Remove(deletelot);
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
                        Error = "[DATA ACCESS] Failed to delete lot from data store."
                    };
                }
            }
        }

        public ResponseDTO<Boolean> EditLotSpots() // todo
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();

            return response;
        }

        public ResponseDTO<Boolean> EditLotName(Guid ownerid, string oldlotname, string newlotname)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var editlot = (from lot in context.Lots
                                     where lot.OwnerId == ownerid &&
                                     lot.LotName == oldlotname
                                     select lot).FirstOrDefault();

                    editlot.LotName = newlotname;

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
                        Error = "[DATA ACCESS] Failed to edit lot name."
                    };
                }
            }
        }

        public ResponseDTO<Lot> GetLotByName(Guid ownerid, string lotname)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var returnlot = (from lot in context.Lots
                                   where lot.OwnerId == ownerid &&
                                   lot.LotName == lotname
                                   select lot).FirstOrDefault();

                    //context.SaveChanges();

                    //dbContextTransaction.Commit();

                    return new ResponseDTO<Lot>()
                    {
                        Data = returnlot
                    };
                }
                catch (Exception)
                {
                    //dbContextTransaction.Rollback();

                    return new ResponseDTO<Lot>()
                    {
                        Data = null,
                        Error = "[DATA ACCESS] Could not fetch lot."
                    };
                }
            }
        }

        public ResponseDTO<List<Lot>> GetAllLots()
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var lots = (from lot in context.Lots select lot).ToList();

                    //context.SaveChanges();

                    //dbContextTransaction.Commit();

                    return new ResponseDTO<List<Lot>>()
                    {
                        Data = lots
                    };
                }
                catch (Exception)
                {
                    //dbContextTransaction.Rollback();

                    return new ResponseDTO<List<Lot>>()
                    {
                        Data = null,
                        Error = "[DATA ACCESS] Could not fetch lots."
                    };
                }
            }
        }

        public ResponseDTO<List<Lot>> GetAllLotsByOwner(Guid ownerid)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var lots = (from lot in context.Lots where lot.OwnerId == ownerid select lot).ToList();

                    //context.SaveChanges();

                    //dbContextTransaction.Commit();

                    return new ResponseDTO<List<Lot>>()
                    {
                        Data = lots
                    };
                }
                catch (Exception)
                {
                    //dbContextTransaction.Rollback();

                    return new ResponseDTO<List<Lot>>()
                    {
                        Data = null,
                        Error = "[DATA ACCESS] Could not fetch lots."
                    };
                }
            }
        }

        public ResponseDTO<List<Spot>> GetAllSpots()
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var spots = (from spot in context.Spots select spot).ToList();

                    //context.SaveChanges();

                    //dbContextTransaction.Commit();

                    return new ResponseDTO<List<Spot>>()
                    {
                        Data = spots
                    };
                }
                catch (Exception)
                {
                    //dbContextTransaction.Rollback();

                    return new ResponseDTO<List<Spot>>()
                    {
                        Data = null,
                        Error = "[DATA ACCESS] Could not fetch spots."
                    };
                }
            }
        }

        public ResponseDTO<List<Spot>> GetAllSpotsByOwner(Guid ownerid)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<Spot> spots = new List<Spot>();

                    var lots = (from lot in context.Lots where lot.OwnerId == ownerid select lot).ToList();
                    List<Spot> lotspots = new List<Spot>();
                    foreach (Lot l in lots)
                    {
                        lotspots = (from spot in context.Spots where spot.LotId == l.LotId select spot).ToList();
                        foreach (Spot sp in lotspots)
                        {
                            spots.Add(sp);
                        }
                    }

                    return new ResponseDTO<List<Spot>>()
                    {
                        Data = spots
                    };
                }
                catch (Exception)
                {
                    //dbContextTransaction.Rollback();

                    return new ResponseDTO<List<Spot>>()
                    {
                        Data = null,
                        Error = "[DATA ACCESS] Could not fetch spots."
                    };
                }
            }
        }

        public ResponseDTO<List<Spot>> GetAllSpotsByLot(Guid ownerid, string lotname)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var _lot = (from lot in context.Lots where lot.OwnerId == ownerid && lot.LotName == lotname select lot).FirstOrDefault();
                    List<Spot> lotspots = (from spot in context.Spots where spot.LotId == _lot.LotId select spot).ToList();

                    //context.SaveChanges();

                    //dbContextTransaction.Commit();

                    return new ResponseDTO<List<Spot>>()
                    {
                        Data = lotspots
                    };
                }
                catch (Exception)
                {
                    //dbContextTransaction.Rollback();

                    return new ResponseDTO<List<Spot>>()
                    {
                        Data = null,
                        Error = "[DATA ACCESS] Could not fetch spots."
                    };
                }
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
