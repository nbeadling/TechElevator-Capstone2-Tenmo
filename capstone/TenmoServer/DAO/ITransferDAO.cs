using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models; 

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        public decimal SendTransfer(decimal amount);

        //public decimal ReceiveTransfer(decimal amount);

        List<Transfer> GetTransfersByUser();

        public Transfer GetTranferById(int id); 

        
    }
}
