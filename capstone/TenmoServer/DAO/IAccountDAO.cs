using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        public decimal GetBalance(int accountId);

        public void IncreaseBalance(decimal amount);

        public void DecreaseBalance(decimal amount); 
    }
}
