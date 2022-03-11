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

        public decimal Balance { get; set; } = 1000.0M;

        public Account()
        {

        }
        public Account(int accountId, int userId, decimal balance)
        {
            Account_Id = accountId;
            User_Id = userId;
            Balance = balance;
        }
    }

    
}
