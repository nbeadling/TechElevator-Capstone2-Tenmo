﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;
using System.Data.SqlClient;

namespace TenmoServer.DAO
{
    public class TransferSqlDao: ITransferDAO
    {
        private readonly string connectionString;

        public TransferSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Transfer> GetTransfersByUser(int id)
        {
            List<Transfer> transfersByUser = new List<Transfer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT t.transfer_id, t.transfer_type_desc, t.transfer_status_desc, t.account_from, t.account_to, t.ammount" +
                   "FROM transfer t JOIN account a ON a.account_id = t.account_from OR a.account_id = t.account_to " +
                   "WHERE a.user_id = @user_id;", conn);
                cmd.Parameters.AddWithValue("@user_id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Transfer transfer = CreateTransferFromReader(reader);
                    transfersByUser.Add(transfer);
                }

            }
            return transfersByUser;
        }

        public Transfer GetTransferById(int id)
        {
            Transfer transfer = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM transfer WHERE transfer_id = @transfer_id", conn);
                cmd.Parameters.AddWithValue("@transfer_id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    transfer = CreateTransferFromReader(reader);
                }
            }
            return transfer;
        } 

        public Transfer CreateTransfer(Transfer transfer)
        {
            int newTransferId = 0;  
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO transfer (transfer_type_id, transfer_status_id, account_from, account_to, amount) " +
                                                "OUTPUT INSERTED.transfer_id " +
                                                "Values(@transfer_type_id, @transfer_status_id, @account_from, @account_to, @amount);", conn);
                cmd.Parameters.AddWithValue("@transfer_type_id", transfer.Transfer_Type_Id);
                cmd.Parameters.AddWithValue("@transfer_status_id", transfer.Transfer_Status_Id);
                cmd.Parameters.AddWithValue("@account_from", transfer.Account_From);
                cmd.Parameters.AddWithValue("@account_to", transfer.Account_To);
                cmd.Parameters.AddWithValue("@amount", transfer.Amount);

                newTransferId = Convert.ToInt32(cmd.ExecuteScalar()); 
            }
            return GetTransferById(newTransferId); 

        }
        

        private Transfer CreateTransferFromReader(SqlDataReader reader)
        {
            Transfer transfer = new Transfer();
            transfer.Transfer_Id = Convert.ToInt32(reader["transfer_id"]);
            transfer.Transfer_Type_Id = Convert.ToInt32(reader["transfer_type_id"]);
            transfer.Transfer_Status_Id = Convert.ToInt32(reader["transfer_status_id"]);
            transfer.Account_From = Convert.ToInt32(reader["account_from"]);
            transfer.Account_To = Convert.ToInt32(reader["account_to"]);
            transfer.Amount = Convert.ToDecimal(reader["amount"]);

            return transfer;
        }
    }
}
