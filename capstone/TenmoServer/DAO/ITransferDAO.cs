using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models; 

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        public Transfer CreateTransfer(Transfer transfer);

        //public decimal ReceiveTransfer(decimal amount);

        public List<Transfer> GetTransfersByUser(int id);

        public Transfer GetTransferById(int id); 

        
    }
}
