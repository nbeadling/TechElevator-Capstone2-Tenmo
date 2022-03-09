using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Account
    {
        public int Account_Id { get; set; }

        public int User_Id { get; set; }

        public decimal Balance { get; set; }

        public Account(int accountId, int userId, decimal balance)
        {
            Account_Id = accountId;
            User_Id = userId;
            Balance = balance;
        }
    }

    
}
