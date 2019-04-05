using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingMaster.DataAccess
{
    public class FunctionGateway : IDisposable
    {
        // Open the function context
        UserContext context;

        public FunctionGateway()
        {
            context = new UserContext();
        }

        public FunctionGateway(UserContext c)
        {
            context = c;
        }

        public ResponseDTO<Boolean> StoreFunction(Function function)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Add Function
                    context.Functions.Add(function);
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
                        Error = "Failed to add Function to data store."
                    };
                }
            }
        }

        public ResponseDTO<Function> GetFunction(string name)
        {
            try
            {
                var funct = (from function in context.Functions
                                   where function.Name == name
                                   select function).FirstOrDefault();

                // Return a ResponseDto with a UserAccount model
                return new ResponseDTO<Function>()
                {
                    Data = funct
                };
            }
            catch (Exception)
            {
                return new ResponseDTO<Function>()
                {
                    Data = null,
                    Error = "The Function: " + name + " does not exist in the data store."
                };
            }
        }

        public ResponseDTO<Boolean> IsFunctionActive(string name)
        {
            try
            {
                var funct = (from function in context.Functions
                             where function.Name == name
                             select function).FirstOrDefault();

                // Return a ResponseDto with a UserAccount model
                return new ResponseDTO<Boolean>()
                {
                    Data = funct.Active
                };
            }
            catch (Exception)
            {
                return new ResponseDTO<Boolean>()
                {
                    Data = false,
                    Error = "The Function: " + name + " does not exist in the data store."
                };
            }
        }

        // Deletes Function from data store by function name
        public ResponseDTO<Boolean> DeleteFunction(string name)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {

                    // Queries for the user account based on username.
                    var funct = (from function in context.Functions
                                       where function.Name == name
                                       select function).FirstOrDefault();

                    // Checking if user account is null.
                    if (funct == null)
                    {
                        return new ResponseDTO<bool>()
                        {
                            Data = false,
                            Error = "Did not find function: " + name + " in the data store."
                        };
                    }

                    // Delete function
                    context.Functions.Remove(funct);
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
                        Error = "Error occured during deletion of function: " + name
                    };
                };
            }
        }

        // Returns all functions in the data store
        public ResponseDTO<List<Function>> GetAllFunctions()
        {
            ResponseDTO<List<Function>> response = new ResponseDTO<List<Function>>();
            try
            {
                response.Data = context.Functions.ToList<Function>();
                return response;
            }
            catch
            {
                response.Data = null;
                response.Error = "Failed to read Function table";
                return response;
            }
        }

        // Deletes all functions to reinitialize the database
        public void ResetDatabase()
        {

            List<Function> functions = GetAllFunctions().Data;

            if(functions == null)
            {
                return;
            }
            foreach (Function function in functions)
            {
                DeleteFunction(function.Name);
            }

        }

        /// <summary>
        /// Dispose of the context
        /// </summary>
        void IDisposable.Dispose()
        {
            context.Dispose();
        }
    }
}
