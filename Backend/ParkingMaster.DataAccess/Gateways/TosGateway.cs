using ParkingMaster.Models.Models;
using ParkingMaster.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Drawing;
using ParkingMaster.Models.Constants;

namespace ParkingMaster.DataAccess
{
    public class TosGateway
    {
        UserContext context;

        public TosGateway()
        {
            context = new UserContext();
        }

        public TosGateway(UserContext c)
        {
            context = c;
        }

        public ResponseDTO<bool> AddAndSetNewTos(TermsOfService newTOS)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var currentTOS = (from tos in context.TOS
                                      where tos.IsActive
                                      select tos).FirstOrDefault();

                    if (currentTOS == null)
                    {
                        // If there is currently no active TOS, only add new TOS
                        newTOS.IsActive = true;
                        context.TOS.Add(newTOS);
                        context.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    else
                    {
                        // If there is a current TOS, disable it then add new active TOS
                        currentTOS.IsActive = false;
                        newTOS.IsActive = true;
                        context.TOS.Add(newTOS);
                        context.SaveChanges();

                        dbContextTransaction.Commit();
                    }

                    return new ResponseDTO<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception e)
                {
                    dbContextTransaction.Rollback();

                    return new ResponseDTO<bool>()
                    {
                        Data = false,
                        Error = ErrorStrings.DATA_ACCESS_ERROR
                    };
                }
            }
        }

        // The Guid passed in depicts the new active TOS, the old TOS will be found through a db query
        public ResponseDTO<bool> ChangeActiveTos(Guid newTosGuid)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var currentTOS = (from tos in context.TOS
                                      where tos.IsActive
                                      select tos).FirstOrDefault();

                    var newTOS = (from tos in context.TOS
                                  where tos.Id == newTosGuid
                                  select tos).FirstOrDefault();

                    if(newTOS == null)
                    {
                        return new ResponseDTO<bool>()
                        {
                            Data = false,
                            Error = ErrorStrings.RESOURCE_NOT_FOUND
                        };
                    }

                    if (currentTOS == null)
                    {
                        // If there is currently no active TOS, only activate new TOS
                        newTOS.IsActive = true;
                        context.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    else
                    {
                        // If there is a current TOS, disable it then activate new active TOS
                        currentTOS.IsActive = false;
                        newTOS.IsActive = true;
                        context.SaveChanges();

                        dbContextTransaction.Commit();
                    }

                    return new ResponseDTO<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception e)
                {
                    dbContextTransaction.Rollback();

                    return new ResponseDTO<bool>()
                    {
                        Data = false,
                        Error = ErrorStrings.DATA_ACCESS_ERROR
                    };
                }
            }
        }

        public ResponseDTO<TermsOfService> GetActiveTermsOfService()
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var currentTOS = (from tos in context.TOS
                                     where tos.IsActive
                                     select tos).FirstOrDefault();

                    if(currentTOS == null)
                    {
                        return new ResponseDTO<TermsOfService>()
                        {
                            Data = new TermsOfService()
                        };
                    }
                    else
                    {
                        return new ResponseDTO<TermsOfService>()
                        {
                            Data = currentTOS
                        };
                    }
                }
                catch (Exception e)
                {
                    dbContextTransaction.Rollback();

                    return new ResponseDTO<TermsOfService>()
                    {
                        Data = null,
                        Error = ErrorStrings.DATA_ACCESS_ERROR
                    };
                }
            }
        }

        public ResponseDTO<List<TermsOfService>> GetAll()
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var tosList = (from tos in context.TOS select tos).ToList();

                    if (tosList == null)
                    {
                        return new ResponseDTO<List<TermsOfService>>()
                        {
                            Data = new List<TermsOfService>()
                            {
                                new TermsOfService()
                            }
                        };
                    }
                    else
                    {
                        return new ResponseDTO<List<TermsOfService>>()
                        {
                            Data = tosList
                        };
                    }
                }
                catch (Exception e)
                {
                    dbContextTransaction.Rollback();

                    return new ResponseDTO<List<TermsOfService>>()
                    {
                        Data = null,
                        Error = ErrorStrings.DATA_ACCESS_ERROR
                    };
                }
            }
        }

    }
}
