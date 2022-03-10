using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace TenmoServer.Models
{
    public class Transfer
    {
        public int Transfer_Id { get; set; }

        public int Transfer_Type_Id { get; set; }

        public int Transfer_Status_Id {get; set; }

        public int Account_From { get; set; }

        public int Account_To { get; set; }
        [Range(0.01, double.PositiveInfinity, ErrorMessage = "Transfer Amount Cannot be negative")] 
        public decimal Amount { get; set; }

        public Transfer(int transferId, int transferTypeId, int transferStatusId, int accountFrom, int accountTo, decimal amount)
        {
            Transfer_Id = transferId;
            Transfer_Type_Id = transferTypeId;
            Transfer_Status_Id = transferStatusId;
            Account_From = accountFrom;
            Account_To = accountTo;
            Amount = amount;
        }
        
        public Transfer() 
        { 
        }

    }

}
