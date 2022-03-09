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

        public void IncreaseBalance(decimal amount, int id);

        public void DecreaseBalance(decimal amount, int id); 
    }
}
