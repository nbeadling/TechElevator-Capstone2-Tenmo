using System;
using System.Collections.Generic;
using TenmoClient.Models;
using TenmoClient.Services;

namespace TenmoClient
{
    public class TenmoApp
    {
        private readonly TenmoConsoleService console = new TenmoConsoleService();
        private readonly TenmoApiService tenmoApiService;
        //private readonly AuthenticatedApiService authApiService;

        public TenmoApp(string apiUrl)
        {
            tenmoApiService = new TenmoApiService(apiUrl);
            //authApiService = new AuthenticatedApiService(apiUrl);
        }

        public void Run()
        {
            bool keepGoing = true;
            while (keepGoing)
            {
                // The menu changes depending on whether the user is logged in or not
                if (tenmoApiService.IsLoggedIn)
                {
                    keepGoing = RunAuthenticated();
                }
                else // User is not yet logged in
                {
                    keepGoing = RunUnauthenticated();
                }
            }
        }

        private bool RunUnauthenticated()
        {
            console.PrintLoginMenu();
            int menuSelection = console.PromptForInteger("Please choose an option", 0, 2, 1);
            while (true)
            {
                if (menuSelection == 0)
                {
                    return false;   // Exit the main menu loop
                }

                if (menuSelection == 1)
                {
                    // Log in
                    Login();
                    return true;    // Keep the main menu loop going
                }

                if (menuSelection == 2)
                {
                    // Register a new user
                    Register();
                    return true;    // Keep the main menu loop going
                }
                console.PrintError("Invalid selection. Please choose an option.");
                console.Pause();
            }
        }

        private bool RunAuthenticated()
        {
            console.PrintMainMenu(tenmoApiService.Username);
            int menuSelection = console.PromptForInteger("Please choose an option", 0, 6);
            if (menuSelection == 0)
            {
                // Exit the loop
                return false;
            }

            if (menuSelection == 1)
            {
                ViewCurrentBalance(); 
            }

            if (menuSelection == 2)
            {
                ViewPastTransfers();
            }

            if (menuSelection == 3)
            {
                // View your pending requests
            }

            if (menuSelection == 4)
            {
                InitiateSendMoney();
            }

            if (menuSelection == 5)
            {
                // Request TE bucks
            }

            if (menuSelection == 6)
            {
                // Log out
                tenmoApiService.Logout();
                console.PrintSuccess("You are now logged out");
            }

            return true;    // Keep the main menu loop going
        }

        private void Login()
        {
            LoginUser loginUser = console.PromptForLogin();
            if (loginUser == null)
            {
                return;
            }

            try
            {
                ApiUser user = tenmoApiService.Login(loginUser);
                if (user == null)
                {
                    console.PrintError("Login failed.");
                }
                else
                {
                    console.PrintSuccess("You are now logged in");
                }
            }
            catch (Exception)
            {
                console.PrintError("Login failed.");
            }
            console.Pause();
        }

        private void Register()
        {
            LoginUser registerUser = console.PromptForLogin();
            if (registerUser == null)
            {
                return;
            }
            try
            {
                bool isRegistered = tenmoApiService.Register(registerUser);
                if (isRegistered)
                {
                    console.PrintSuccess("Registration was successful. Please log in.");
                }
                else
                {
                    console.PrintError("Registration was unsuccessful.");
                }
            }
            catch (Exception)
            {
                console.PrintError("Registration was unsuccessful.");
            }
            console.Pause();
        }

        private void ViewCurrentBalance()
        {
            decimal currentBalance = 0.0M;
            Account currentAccount = tenmoApiService.GetMyAccount();
            currentBalance = currentAccount.Balance;
            console.DisplayBalance(currentBalance);
            console.Pause();

        }

        private void ViewPastTransfers()
        {
            
            List<Transfer> transfers = tenmoApiService.GetTransfersByUserId();
            console.DisplayAllTransfers(transfers);
            console.Pause();
        }

        private void InitiateSendMoney()
        {
            List <User> users = tenmoApiService.GetAllUsers();
            foreach (User user in users)
            {
                if (user.Username == tenmoApiService.Username)
                {
                    users.Remove(user);
                    break;
                }
            }
            console.DisplayListOfUsers(users);
            int transferTargetAccount = console.PromptForInteger("Chose a user number from the list above to send money to: ");
            decimal transferAmount = console.PromptForDecimal("How much money would you like to transfer?");

            Account sourceAccount = tenmoApiService.GetAccountByUserId(tenmoApiService.UserId);
            Account destinationAccount = tenmoApiService.GetAccountByUserId(transferTargetAccount);

            sourceAccount.Balance -= transferAmount;
            destinationAccount.Balance += transferAmount;

            tenmoApiService.UpdateAccount(sourceAccount.AccountId, sourceAccount);
            tenmoApiService.UpdateAccount(destinationAccount.AccountId, destinationAccount);

            Transfer completedTransfer = new Transfer(2, 2, sourceAccount.AccountId, destinationAccount.AccountId, transferAmount);
            tenmoApiService.CreateTransfer(completedTransfer);

            console.Pause();
        }
    }
}
