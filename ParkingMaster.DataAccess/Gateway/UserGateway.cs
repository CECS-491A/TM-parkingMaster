\\Gateway to communicate with UserContext



 public DTO<SomeAccount> GetUserByEmail(string email)
        {
            try
            {
                var SomeAccount = (from account in context.SomeAccount
                                   where account.email == email
                                   select account).FirstOrDefault();

                // Return a DTO with UserAccount model 
                return new DTO<SomeAccount>()
                {
                    Data = SomeAccount
                };
            }
            catch (Exception)
            {
                return new DTO<SomeAccount>()
                {
                    Data = new SomeAccount(email),
                    //Error = idk if we got any error message but we could put one here
                };
            }
}
