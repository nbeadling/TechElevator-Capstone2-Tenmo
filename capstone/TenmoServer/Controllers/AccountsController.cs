﻿using Microsoft.AspNetCore.Mvc;
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
    //[Authorize]
    public class AccountsController : ControllerBase
    {
        private IAccountDAO accountDao;

        public AccountsController(IAccountDAO accountDao)
        {
            this.accountDao = accountDao;
        }

        [HttpGet]
        public List<Account> ListAllAccounts()
        {
            return accountDao.GetAllAccounts();
        }
        
        [HttpGet("/accounts/myaccount")]
        public Account GetMyAccount()
        {
            string userId = User.FindFirst("sub")?.Value;

            int userIdNumber = Convert.ToInt32(userId);
            Account account = accountDao.GetAccountByUserId(userIdNumber);
            return account;
        }

        //[HttpGet("/accounts/user/{id}")]
        //public decimal GetAccountByUserId(int id)
        //{
        //    //string userId = User.FindFirst("sub")?.Value;

        //    //int userIdNumber = Convert.ToInt32(userId);
        //    //Account account = accountDao.GetAccountByAccountId(userIdNumber);
        //    //return account.Balance;
        //}

        [HttpGet("{id}")]
        public Account GetAccountByAccountID(int id)
        {
            return accountDao.GetAccountByAccountId(id);
        }

        [HttpPut("{id}")]
        public ActionResult<Account> UpdateBalance(Account account)
        {
            Account existingAccount = accountDao.GetAccountByAccountId(account.AccountId);
            if (existingAccount == null)
            {
                return NotFound();
            }

            accountDao.UpdateBalance(existingAccount);
            Account updatedAccount = accountDao.GetAccountByAccountId(account.AccountId); 
            return Ok(updatedAccount);
        }

        //[HttpPut("{id}")]
        //public ActionResult<Account> DecreaseBalance(decimal amount, int id)
        //{
        //    Account existingAccount = accountDao.GetAccountByAccountId(id);
        //    if (existingAccount == null)
        //    {
        //        return NotFound();
        //    }

        //    accountDao.DecreaseBalance(amount, id);
        //    Account updatedAccount = accountDao.GetAccountByAccountId(id);
        //    return Ok(updatedAccount);
        //}
        //[HttpPost]
        //public ActionResult<Account> CreateNewAccount(int userId)
        
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
