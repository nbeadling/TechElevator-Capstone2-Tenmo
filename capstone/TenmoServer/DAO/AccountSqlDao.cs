using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountSQLDao: IAccountDAO
    {
        private readonly string connectionString;
        private List<Account> Accounts { get; set; }
        
        public AccountSQLDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Account GetAccountByUserId(int id)
        {
            Account account = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select account_id, user_id, balance FROM account WHERE user_id = @user_id;", conn);
                cmd.Parameters.AddWithValue("@user_id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    account = CreateAccountFromReader(reader);
                }

            }
            return account;
        }
        public Account GetAccountByAccountId(int id)
        {
            Account account = null; 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); 
                SqlCommand cmd = new SqlCommand("Select account_id, user_id, balance FROM account WHERE account_id = @account_id;", conn);
                cmd.Parameters.AddWithValue("@account_id", id);

                SqlDataReader reader = cmd.ExecuteReader(); 

                if (reader.Read())
                {
                    account = CreateAccountFromReader(reader); 
                }

            }
            return account; 
            //Actually return error code
        }

        public List<Account> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM account;", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Account account = CreateAccountFromReader(reader);
                    accounts.Add(account);
                }

            }
            return accounts;
        }

        public decimal GetBalance(int id)
        {   
            foreach (Account account in Accounts)
            {
                if (account.AccountId == id)
                {
                    return account.Balance;
                }
            }
            return 0;
            //actually return error value
        }

        public void IncreaseBalance(decimal amount, int id)
        {
          // Account account = null; 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); 
                SqlCommand cmd = new SqlCommand("UPDATE account SET balance = balance + @amount where account_id = @account_id", conn);
                cmd.Parameters.AddWithValue("@account_id", id);
                cmd.Parameters.AddWithValue("@amount", amount); 

                cmd.ExecuteNonQuery(); 
            }
            //return GetAccountById(id); 
            //return account; 
            //this will be an error handling
        }

        public void DecreaseBalance(decimal amount, int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); 
                SqlCommand cmd = new SqlCommand("UPDATE account SET balance = balance - @amount where account_id = @account_id", conn);
                cmd.Parameters.AddWithValue("@account_id", id);
                cmd.Parameters.AddWithValue("@amount", amount); 

                cmd.ExecuteNonQuery(); 
            }
            //this will be an error handling
        }

        //public Account CreateNewAccount(int userId)
        //{
        //    int newAccountId = 0;
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("INSERT INTO account (user_id, balance)" +
        //                                        "OUPUT INSERTED.account_id" +
        //                                        "VALUES (@user_id, balance;", conn);
        //        cmd.Parameters.AddWithValue("@user_id", userId);

        //        newAccountId = Convert.ToInt32(cmd.ExecuteScalar());

        //    }
        //    return GetAccountById(newAccountId);
        //}

        private Account CreateAccountFromReader(SqlDataReader reader)
        {
            Account account = new Account();
            account.AccountId = Convert.ToInt32(reader["account_id"]);
            account.UserId = Convert.ToInt32(reader["user_id"]);
            account.Balance = Convert.ToDecimal(reader["balance"]);

            return account; 
        }
    }
}
