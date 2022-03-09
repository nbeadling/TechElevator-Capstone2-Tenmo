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

        
       
        public Account GetAccountById(int id)
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

        public decimal GetBalance(int id)
        {   
            foreach (Account account in Accounts)
            {
                if (account.Account_Id == id)
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

        private Account CreateAccountFromReader(SqlDataReader reader)
        {
            Account account = new Account();
            account.Account_Id = Convert.ToInt32(reader["account_id"]);
            account.User_Id = Convert.ToInt32(reader["user_id"]);
            account.Balance = Convert.ToDecimal(reader["balance"]);

            return account; 
        }
    }
}
