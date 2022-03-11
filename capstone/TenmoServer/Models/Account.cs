﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Account
    {
        public int AccountId { get; set; }

        public int UserId { get; set; }

        public decimal Balance { get; set; } = 1000.0M;

        public Account()
        {

        }
        public Account(int accountId, int userId, decimal balance)
        {
            AccountId = accountId;
            UserId = userId;
            Balance = balance;
        }
    }

    
}
