using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;
using Microsoft.AspNetCore.Authorization;


namespace TenmoServer.Controllers
{
    [Route("accounts")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private IAccountDAO accountDao;

        public AccountController(IAccountDAO accountDao)
        {
            this.accountDao = accountDao;
        }

        [HttpGet("{id}")]
        public decimal GetBalance(int id)
        {
            return accountDao.GetBalance(id);
        }

        [HttpPut("{id}")]
        public ActionResult<Account> IncreaseBalance(decimal amount, int id)
        {
            Account existingAccount = accountDao.GetAccountById(id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            Account updatedAccount = accountDao.IncreaseBalance(amount, id);
            return Ok(updatedAccount);
        }

        public ActionResult<Account> DecreaseBalance(decimal amount, int id)
        {
            Account existingAccount = accountDao.GetAccountById(id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            Account updatedAccount = accountDao.DecreaseBalance(amount, id);
            return Ok(updatedAccount);
        }
        
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
