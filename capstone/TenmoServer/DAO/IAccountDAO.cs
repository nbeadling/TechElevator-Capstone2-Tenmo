using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        public decimal GetBalance(int accountId);

        public Account GetAccountById(int id);

        public Account IncreaseBalance(decimal amount, int id);

        public Account DecreaseBalance(decimal amount, int id); 
    }
}
