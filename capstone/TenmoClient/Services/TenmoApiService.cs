using RestSharp;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;

        public TenmoApiService(string apiUrl) : base(apiUrl) { }

        // Add methods to call api here...
        public List<Account> GetAccounts()
        {
            RestRequest request = new RestRequest("accounts");
            IRestResponse<List<Account>> response = client.Get<List<Account>>(request);

            CheckForError(response);
            return response.Data;

        }

        public Account GetAccountById(int accountId)
        {
            RestRequest request = new RestRequest($"accounts/{accountId}");
            IRestResponse<Account> response = client.Get<Account>(request);

            CheckForError(response);
            return response.Data;
        }

        public Transfer IntitiateTransfer(Transfer newTransfer)
        {
            RestRequest request = new RestRequest("transfers");
            request.AddJsonBody(newTransfer);
            IRestResponse<Transfer> response = client.Post<Transfer>(request);

            CheckForError(response);
            return response.Data;
        }

        public Account UpdateAccount(Account accountToUpdate)
        {
            RestRequest request = new RestRequest($"accounts/{accountToUpdate.Account_Id}");
            request.AddJsonBody(accountToUpdate);
            IRestResponse<Account> response = client.Put<Account>(request);

            CheckForError(response);
            return response.Data;
        }

        public List<Transfer> GetTransfers()
        {
            RestRequest request = new RestRequest("transfers");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);

            CheckForError(response);
            return response.Data;
        }

        public Transfer GetTransferById(int transferId)
        {
            RestRequest request = new RestRequest($"transfers/{transferId}");
            IRestResponse<Transfer> response = client.Get<Transfer>(request);

            CheckForError(response);
            return response.Data;
        }
    }
}
