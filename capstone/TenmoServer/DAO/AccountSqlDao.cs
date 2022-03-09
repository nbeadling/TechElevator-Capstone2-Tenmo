using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountSQLDao: IAccountDAO
    {
        private readonly string connectionString;
        private List<Account> Accounts { get; set; }
        
        public AccountSQLDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        
       
        public Account GetAccountById(int id)
        {
           foreach (Account account in Accounts)
           {
                if (account.Account_Id == id)
                {
                    return account;
                }
           }
            return null;
            //Actually return error code
        }

        public decimal GetBalance(int id)
        {   
            foreach (Account account in Accounts)
            {
                if (account.Account_Id == id)
                {
                    return account.Balance;
                }
            }
            return 0;
            //actually return error value
        }

        public Account IncreaseBalance(decimal amount, int id)
        {
            Account original = GetAccountById(id);
            if (original != null)
            {
                original.Balance += amount;
                return original;
            }
            return null;
            //this will be an error handling
        }

        public Account DecreaseBalance(decimal amount, int id)
        {
            Account original = GetAccountById(id);
            if (original != null)
            {
                original.Balance -= amount;
                return original;
            }
            return null;
            //this will be an error handling
        }
    }
}
