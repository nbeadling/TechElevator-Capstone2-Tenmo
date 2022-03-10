using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TenmoServer.DAO;
using TenmoServer.Models;


namespace TenmoServer.Controllers
{
    [Route("transfer")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private ITransferDAO transferDAO;

        public TransferController(ITransferDAO transferDAO)
        {
            this.transferDAO = transferDAO;
        }

        [HttpGet("{id}")]
        public Transfer GetTransfer(int id)
        {
            return transferDAO.GetTransferById(id);
        }

        [HttpGet("{id}")]
        public ActionResult<List<Transfer>> GetTransferByUserId(int id)
        {
            string userId = User.FindFirst("sub")?.Value;

            int userIdNumber = Convert.ToInt32(userId); 

            return transferDAO.GetTransfersByUser(userIdNumber); 
            
        }

        [HttpPost()]

        public ActionResult<Transfer> CreateTransfer(Transfer transfer)
        {
            Transfer added = transferDAO.CreateTransfer(transfer);
            return Created($"/transfers/{added.Transfer_Id}", added); 
        }
        
        

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
