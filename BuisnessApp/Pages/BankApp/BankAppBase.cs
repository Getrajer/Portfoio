using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BuisnessApp.Pages.BankApp
{
    public class BankAppBase : ComponentBase
    {
        #region Classes

        public class BankAccount {
            /// <summary>
            /// This id will point to position in the dataset
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// This will connenct account with user
            /// </summary>
            public string UserUniqueId { get; set; }

            public string Name { get; set; }

            public double Balance { get; set; }

            public string AccountNumber { get; set; }

            /// <summary>
            /// This list will store data about money wchich went in and out the account
            /// </summary>
            public List<MoneyInOut> MoneyInOut = new List<MoneyInOut>();
        }

        /// <summary>
        /// This class will determine user
        /// </summary>
        public class BankUser
        {
            //Id of possition in database
            public int Id { get; set; }
            public string UniqueId { get; set; }
            public string Name { get; set; }
            public string Lastname { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public DateTime BirthDate { get; set; }
            public string Postcode { get; set; }
            public string StreetAddress { get; set; }
            //Street number in this program is simplyfied to an int, but it should come from the list connected to the Postcode database
            //And probably it should have form of string or enum
            public string StreetNumber { get; set; }
            //This password should be at least 8 characters long, it should have at least 2 unique characters and 2 numbers
            public string Password { get; set; }
            //Automaticly generated pin of lenght 3 ex (2:3:2)
            public string PIN { get; set; }
            //User can set if pin should be generated every time when trying to access to account
            //New pin can be connected for example with phone app where it will be displayed
            //For this program I'll create component showing new Pin on the login page
            public bool IfConstPINGenerating { get; set; }
            public string PasswordReminderQuestion { get; set; }
            public string PasswordRminderAnswer { get; set; }
            public bool IfHasBankAccount { get; set; }

            /// <summary>
            /// This parameter will determine how much money user can borrow
            /// </summary>
            public double MaximumCredit { get; set; }

            public List<Loan> loans = new List<Loan>();

            public List<BankAccount> bankAccounts = new List<BankAccount>();

            public BankUser() { }
        }

        /// <summary>
        /// This class will determine Saving bank account
        /// </summary>
        public class SavingBankAccount : BankAccount
        {
            public double Interest { get; set; }

            /// <summary>
            /// This iteration every few months. So if InterestIteration = 4 then every 4 months
            /// </summary>
            public int InterestIteration { get; set; }

            /// <summary>
            /// Balance for saving account must be at least £10000
            /// </summary>


            public SavingBankAccount() { 
                this.Name = "Saving Account";
                MoneyInOut = new List<MoneyInOut>() { new MoneyInOut(0 , DateTime.Now, "", "")};
            }
        }

        /// <summary>
        /// This class will determine Investment bank account
        /// </summary>
        public class InvestmentBankAccount : BankAccount
        {
            public InvestmentBankAccount() { 
                this.Name = "Investment Account";
                MoneyInOut = new List<MoneyInOut>() { new MoneyInOut(0, DateTime.Now, "", "") };
            }
        }

        /// <summary>
        /// This class will determine Checking bank account
        /// </summary>
        public class CheckingAccount : BankAccount
        {
            public string SortCode { get; set; }

            public DebitCard DebitCard { get; set; }



            public CheckingAccount() { 
                Name = "Cheking Account";
                MoneyInOut = new List<MoneyInOut>() { new MoneyInOut(0, DateTime.Now, "", "") };
            }
        }


        /// <summary>
        /// This class will determine Money Market bank account
        /// </summary>
        public class MoneyMarketAccount : BankAccount
        {

            /// <summary>
            /// In money market account number of outputing money is limited / in this example is set fixed to 2 per month
            /// </summary>
            /// In this program counter will be reset every month / but in real world example there would be function working on the server changing its value every 30 days
            public int MaxOutput { get; set; }

            /// <summary>
            /// Money market account requires min ammount of money on its balance / in this example it will be set fixed to £20000
            /// </summary>
            public double MinBalance { get; set; }

            /// <summary>
            /// Interest in this account will be set fixed to up to 8%
            /// </summary>
            public double MaxInterest { get; set; }

            public MoneyMarketAccount() { 
                this.Name = "Money Market Account";
                this.MaxOutput = 2;
                this.MinBalance = 20000;
                this.MaxInterest = 9;
                MoneyInOut = new List<MoneyInOut>() { new MoneyInOut(0, DateTime.Now, "", "") };
            }
        }

        /// <summary>
        /// This class will determine CD bank account
        /// </summary>
        public class CDAccount : BankAccount
        {
            public CDAccount() { 
                this.Name = "CD Account";
                MoneyInOut = new List<MoneyInOut>() { new MoneyInOut(0, DateTime.Now, "", "") };
            }
        }

        /// <summary>
        /// This class will determine Retairment bank account
        /// </summary>
        public class RetirmentAccount : BankAccount
        {
            public RetirmentAccount() 
            {
                this.Name = "Retirment Account";
                MoneyInOut = new List<MoneyInOut>() { new MoneyInOut(0, DateTime.Now, "", "") };
            }
        }


        /// <summary>
        /// This class will determine look and codes for credit card
        /// </summary>
        public class DebitCard
        {
            /// <summary>
            /// This id will point to position in the dataset
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// This will connenct account with user
            /// </summary>
            public string UserUniqueId { get; set; }

            public string Name { get; set; }

            public string CardNumber { get; set; }

            public DateTime ValidFrom { get; set; }

            public DateTime Expires { get; set; }

            public string SecurityCode1 { get; set; }

            public string SecurityCode2 { get; set; }

            public CheckingAccount CheckingAccount { get; set; }

            public DebitCard() {
                this.ValidFrom = DateTime.Now;
                this.Expires = new DateTime(DateTime.Now.Year + 4, DateTime.Now.Month, DateTime.Now.Day);
            }
        }

        public class MoneyInOut
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            /// <summary>
            /// This can be in or out Ex( +40£, -30£)
            /// </summary>
            public string Money { get; set; }
            public string Reference { get; set; }

            public string DateString { get; set; }

            public MoneyInOut() { }

            public MoneyInOut(int id, DateTime dateTime, string money, string refence)
            {
                this.Id = id;
                this.Date = dateTime;
                this.Money = money;
                this.Reference = refence;
                DateString = DateToString(dateTime);
            }

            private string DateToString(DateTime dt)
            {
                return dt.ToString("dd/MM/yyyy");
            }
        }

        /// <summary>
        /// This object will be used as option when user will choose loan
        /// </summary>
        public class LoanOption
        {
            public int Id { get; set; }
            public int Duration { get; set; }
            public double Ammount { get; set; }
            public double Interest { get; set; }

            public LoanOption(int id, int duration, double ammount, double interest)
            {
                this.Id = id;
                this.Duration = duration;
                this.Ammount = ammount;
                this.Interest = interest;
            }
        }

        /// <summary>
        /// This object determine loan for user account
        /// </summary>
        public class Loan
        {
            public int Id { get; set; }
            public int Duration { get; set; }
            public double AmmountBorrowed { get; set; }
            public double AmmountToPay { get; set; }
            public double Paid { get; set; }
            public double PartialPay { get; set; }
            public double Interest { get; set; }

            public Loan() { }
        }

        /// <summary>
        /// This class will determine Admin
        /// </summary>
        public class BankAdmin
        {
            public BankAdmin() { }
        }

        /// <summary>
        /// This class will simiulate bank by itself
        /// </summary>
        public class Bank
        {
            public Bank() { }
        }

        #endregion

        #region Mock Data
        /// <summary>
        /// This list will store user data
        /// </summary>
        protected List<BankUser> bank_users = new List<BankUser>();
        protected List<DebitCard> debit_cards_list = new List<DebitCard>();
        protected List<CheckingAccount> checking_accounts = new List<CheckingAccount>();
        protected List<InvestmentBankAccount> investment_accounts = new List<InvestmentBankAccount>();
        protected List<SavingBankAccount> saving_accounts = new List<SavingBankAccount>();
        protected List<CDAccount> CD_accounts = new List<CDAccount>();
        protected List<MoneyMarketAccount> money_market_accounts = new List<MoneyMarketAccount>();
        protected List<RetirmentAccount> retirement_accounts = new List<RetirmentAccount>();

        List<string> services_names = new List<string>();

        protected BankUser dave_info = new BankUser();

        #region Generate Data and other parameters
        protected override async Task OnInitializedAsync()
        {
            //Create example user
            BankUser user = new BankUser();
            user.Name = "Dave";
            user.Lastname = "Doe";
            user.Email = "dave@gmail.com";
            user.PhoneNumber = "759384950";
            user.Postcode = "N27Jb";
            user.StreetAddress = "Low Street";
            user.StreetNumber = "77";
            user.Password = "123";
            user.PIN = "123";
            user.PasswordReminderQuestion = "Your favorite color";
            user.PasswordRminderAnswer = "Blue";
            user.UniqueId = "111111111";
            user.IfHasBankAccount = true;
            user.BirthDate = new DateTime(1995, 5, 4);
            bank_users.Add(user);

            dave_info = user;

            #region Create mock user accounts

            //Create example checking account for example user
            CheckingAccount checking_account_ex = new CheckingAccount();
            checking_account_ex.AccountNumber = "54864751";
            checking_account_ex.Name = "Checking Account";
            checking_account_ex.Id = 0;
            checking_account_ex.SortCode = "902342";
            checking_account_ex.UserUniqueId = user.UniqueId;
            checking_account_ex.Balance = 8645.5;
            checking_account_ex.MoneyInOut = new List<MoneyInOut>()
            {
                new MoneyInOut(12, new DateTime(2020, 5, 4), "+4000", "Salary"),
                new MoneyInOut(11, new DateTime(2020, 5, 3), "-400", "Spending Example"),
                new MoneyInOut(10, new DateTime(2020, 5, 2), "-300", "Spending Example"),
                new MoneyInOut(9, new DateTime(2020, 5, 1), "+100", "Income Example"),
                new MoneyInOut(8, new DateTime(2020, 4, 30), "-400", "Spending Example"),
                new MoneyInOut(7, new DateTime(2020, 4, 22), "+400", "Income Example"),
                new MoneyInOut(6, new DateTime(2020, 4, 24), "-10", "Spending Example"),
                new MoneyInOut(5, new DateTime(2020, 4, 22), "+300", "Income Example"),
                new MoneyInOut(4, new DateTime(2020, 4, 20), "-1000", "Spending Example"),
                new MoneyInOut(3, new DateTime(2020, 4, 14), "+100", "Income Example"),
                new MoneyInOut(2, new DateTime(2020, 4, 4), "+4000", "Salary"),
                new MoneyInOut(1, new DateTime(2020, 4 ,6), "-300", "Spending Example"),
                new MoneyInOut(0, new DateTime(2020, 4, 1), "-2000", "Spending Example"),
            };



            for (int i = 0; i < checking_account_ex.MoneyInOut.Count; i++)
            {
                checking_account_ex.MoneyInOut[i].DateString = DateToString(checking_account_ex.MoneyInOut[i].Date);
            }

            checking_accounts.Add(checking_account_ex);
            user.bankAccounts.Add(checking_account_ex);

            //Create example savig account
            SavingBankAccount saving_account_ex = new SavingBankAccount();

            saving_account_ex.AccountNumber = "879641587";
            saving_account_ex.Balance = 10000;
            saving_account_ex.Id = 0;
            saving_account_ex.Interest = 1.5;
            saving_account_ex.InterestIteration = 1;
            saving_account_ex.Name = "Saving Account";
            saving_account_ex.UserUniqueId = "111111111";

            saving_account_ex.MoneyInOut = new List<MoneyInOut>()
            {
                new MoneyInOut(0, new DateTime(2020, 4, 1), "+10000", "Deposit"),
            };

            user.bankAccounts.Add(saving_account_ex);
            saving_accounts.Add(saving_account_ex);

            //Create example money market account
            MoneyMarketAccount moneyMarket_ex = new MoneyMarketAccount();
            moneyMarket_ex.AccountNumber = "5698547826";
            moneyMarket_ex.Balance = 30000;
            moneyMarket_ex.Id = 0;
            moneyMarket_ex.Name = "Money Market Account";
            moneyMarket_ex.UserUniqueId = "111111111";
            moneyMarket_ex.MoneyInOut = new List<MoneyInOut>()
            {
                new MoneyInOut(0, new DateTime(2020, 4, 1), "+30000", "Deposit"),
            };

            user.bankAccounts.Add(moneyMarket_ex);
            money_market_accounts.Add(moneyMarket_ex);

            //Create example of retirement account
            RetirmentAccount retirment_ex = new RetirmentAccount();
            retirment_ex.AccountNumber = "45896578256";
            retirment_ex.Balance = 71000;
            retirment_ex.Id = 0;
            retirment_ex.UserUniqueId = "111111111";
            retirment_ex.Name = "Retirement Account";
            retirment_ex.MoneyInOut = new List<MoneyInOut>()
            {
                new MoneyInOut(11, new DateTime(2020, 12, 1), "+1000", "Increase Funds"),
                new MoneyInOut(10, new DateTime(2020, 11, 1), "+1000", "Increase Funds"),
                new MoneyInOut(9, new DateTime(2020, 10, 1), "+1000", "Increase Funds"),
                new MoneyInOut(8, new DateTime(2020, 09, 1), "+1000", "Increase Funds"),
                new MoneyInOut(7, new DateTime(2020, 08, 1), "+1000", "Increase Funds"),
                new MoneyInOut(6, new DateTime(2020, 07, 1), "+1000", "Increase Funds"),
                new MoneyInOut(5, new DateTime(2020, 06, 1), "+1000", "Increase Funds"),
                new MoneyInOut(4, new DateTime(2020, 05, 1), "+1000", "Increase Funds"),
                new MoneyInOut(3, new DateTime(2020, 04, 1), "+1000", "Increase Funds"),
                new MoneyInOut(2, new DateTime(2020, 03, 1), "+1000", "Increase Funds"),
                new MoneyInOut(1, new DateTime(2020, 02, 1), "+1000", "Increase Funds"),
                new MoneyInOut(0, new DateTime(2020, 01, 1), "+60000", "Moving retirment funds"),
            };
            user.bankAccounts.Add(retirment_ex);
            retirement_accounts.Add(retirment_ex);


            //Create exaple of investment account
            InvestmentBankAccount investment_ex = new InvestmentBankAccount();
            investment_ex.Id = 0;
            investment_ex.Name = "Investment Account";
            investment_ex.UserUniqueId = "111111111";
            investment_ex.Balance = 10000;
            investment_ex.MoneyInOut = new List<MoneyInOut>()
            {
                new MoneyInOut(0, new DateTime(2020, 4, 1), "+10000", "Opening Account"),
            };



            //Example cd account
            CDAccount cD_ex = new CDAccount();
            cD_ex.AccountNumber = "876940857";
            cD_ex.Balance = 20000;
            cD_ex.Id = 0;
            cD_ex.Name = "CD Account";
            cD_ex.UserUniqueId = "111111111";
            cD_ex.MoneyInOut = new List<MoneyInOut>()
            {
                new MoneyInOut(0, new DateTime(2020, 4, 1), "+20000", "Deposit"),
            };
            user.bankAccounts.Add(cD_ex);
            CD_accounts.Add(cD_ex);

            #endregion

            //Create example debit card
            DebitCard credit = new DebitCard();
            credit.CardNumber = "4763456895642547";
            credit.CheckingAccount = checking_account_ex;
            credit.ValidFrom = new DateTime(2020, 4, 01);
            credit.Expires = new DateTime(2024, 4, 01);
            credit.Name = "Dave D Doe";
            credit.SecurityCode1 = "5876";
            credit.SecurityCode2 = "748";
            credit.UserUniqueId = user.UniqueId;
            debit_cards_list.Add(credit);

            checking_account_ex.DebitCard = credit;

            //To delete
            //loggedin_user = user;
            //is_logged_in = true;
            checking_account = checking_account_ex;
            saving_account = saving_account_ex;
            investment_account = investment_ex;
            retirment_account = retirment_ex;
            cd_account = cD_ex;
            mmn_account = moneyMarket_ex;

            //Pin numbers
            pin_list = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };


            //Create services list of names for unsigned services
            services_names = new List<string>()
            {
                "Checking Account",
                "Saving Account",
                "CD Account",
                "Money Market Account",
                "Retirement Account",
            };

            ReloadServices();

        }
        #endregion

        #endregion

        #region USER (Create, Edit Account, Edit Password, Delete Account)

        #region User variables
        protected string name = "";
        protected string lastname = "";
        protected string email = "";
        protected string phone_number = "";
        protected string postcode = "";
        protected string street_address = "";
        protected string street_number = "";
        protected string password = "";
        protected string password_reminder_question = "";
        protected string password_reminder_answer = "";
        protected string password_confirmation = "";
        protected int day = 1;
        protected int month = 1;
        protected int year = 1900;
        #endregion

        #region Error Variables
        protected string name_error = "";
        protected string lastname_error = "";
        protected string email_error = "";
        protected string phone_number_error = "";
        protected string postcode_error = "";
        protected string street_address_error = "";
        protected string street_number_error = "";
        protected string password_error = "";
        protected string password_reminder_question_error = "";
        protected string password_reminder_answer_error = "";
        protected string password_confirmation_error = "";

        protected string error_delete_account = "";

        protected string error_login = "";
        #endregion

        #region Login variables
        /// <summary>
        /// When user will log in this variable will store it
        /// </summary>
        protected BankUser loggedin_user = new BankUser();

        /// <summary>
        /// When variable will store user for editing / It needs to be declared in advance to give user posibility to cancel editing 
        /// </summary>
        protected BankUser edit_user = new BankUser();

        //Accounts for logged in user
        protected CheckingAccount checking_account = new CheckingAccount();
        protected SavingBankAccount saving_account = new SavingBankAccount();
        protected InvestmentBankAccount investment_account = new InvestmentBankAccount();
        protected RetirmentAccount retirment_account = new RetirmentAccount();
        protected CDAccount cd_account = new CDAccount();
        protected MoneyMarketAccount mmn_account = new MoneyMarketAccount();


        protected bool is_logged_in = false;

        //T
        protected List<string> pin_list;

        protected string pin_1 = "0";
        protected string pin_2 = "0";
        protected string pin_3 = "0";

        protected string class_pin_1 = "display_none";
        protected string class_pin_2 = "display_none";
        protected string class_pin_3 = "display_none";


        #endregion

        #region Create User
        /// <summary>
        /// This function will create user
        /// </summary>
        public void CreateUserAccount()
        {
            ResetVariablesErrors();

            bool formula_error = false;

            #region Name and Lastname validation
            if(name == "")
            {
                formula_error = true;
                name_error = "Please enter your name";
            }

            if(lastname == "")
            {
                formula_error = true;
                lastname_error = "Please enter your lastname";
            }
            #endregion


            #region Email validation 
            if (email == "")
            {
                formula_error = true;
                email_error = "Please enter your email";
            }
            else
            {
                bool if_email_valid = IsValidEmail(email);

                if(if_email_valid == false)
                {
                    formula_error = true;
                    email_error = "Email is not valid!";
                }
            }
            #endregion

            #region Phone number validation
            if (phone_number == "")
            {
                formula_error = true;
                phone_number_error = "Please enter your phone number";
            }
            else
            {
                bool if_phone_number_vaild = IsPhoneNumber(phone_number);

                if(if_phone_number_vaild == false)
                {
                    formula_error = true;
                    phone_number_error = "Phone number is not valid!";
                }
            }
            #endregion

            #region Postcode validation
            if(postcode == "")
            {
                formula_error = true;
                postcode_error = "Please enteryour postcode";
            }
            else{
                bool if_postcode_valid = IsValidPostcode(postcode);

                if(if_postcode_valid == false)
                {
                    formula_error = true;
                    postcode_error = "Postcode is not valid";
                }
                //In real world app it is good to use database of postcodes to validate postcode
                //It can be something like Postcode Address File (PAF), but since subscibtion is paid in this app ill juse just regex
                //More info https://ideal-postcodes.co.uk/guides/postcode-validation
            }
            #endregion

            #region Street address validation
            if(street_address == "")
            {
                formula_error = true;
                street_address_error = "Please enter your street address";
            }
            else
            {
                //Herewith use of database and postcode determine if address is real
                //For this app I need to asume that user will input right address
            }
            #endregion

            #region Street number
            if(street_number == "")
            {
                formula_error = true;
                street_number_error = "Please enter your street number";
            }
            else
            {
                //Here as well number should come from database based on street and postcode
            }
            #endregion

            #region Password validation
            if(password == "")
            {
                formula_error = true;
                password_error = "Please enter password number";
            }
            else
            {
                bool is_password_valid = IsValidPassword(password);

                if (is_password_valid == false)
                {
                    formula_error = true;
                    password_error = "Password is not valid!";
                }
            }

            if(formula_error == false)
            {
                if(password_confirmation != password)
                {
                    formula_error = true;
                    password_confirmation_error = "Passwords are not equal";
                }
            }
            #endregion

            #region Password reminder question and answer
            if(password_reminder_question == "")
            {
                formula_error = true;
                password_reminder_question_error = "Please write a question for your reminder";
            }

            if(password_reminder_answer == "")
            {
                formula_error = true;
                password_reminder_answer_error = "Please write answerto your question";
            }
            #endregion

            #region Create account
            if(formula_error == false)
            {
                BankUser user = new BankUser();

                user.Name = name;
                user.Lastname = lastname;
                user.Email = email;
                user.PhoneNumber = phone_number;
                user.Postcode = postcode;
                user.StreetAddress = street_address;
                user.StreetNumber = street_number;
                user.Password = password;
                user.BirthDate = new DateTime(year, month, day);

                user.IfHasBankAccount = false;
                user.IfConstPINGenerating = false;

                string pin = "";

                for(int i = 0; i < 3; i++)
                {
                    int r = RandomNumber(0, 9);

                    pin += r.ToString();
                }

                user.PIN = pin;

                user.Id = bank_users.Count;

                string uniqueId = "";
                bool is_unique = false;

                while(is_unique == false)
                {
                    uniqueId = "";

                    for (int i = 0; i < 9; i++)
                    {
                        int r = RandomNumber(0, 9);

                        uniqueId += r.ToString();
                    }

                    int c = 0;
                    for(int i = 0; i < bank_users.Count; i++)
                    {
                        if(uniqueId == bank_users[i].UniqueId)
                        {
                            c++;
                        }
                    }

                    if(c == 0)
                    {
                        is_unique = true;
                    }
                }

                user.UniqueId = uniqueId;

                bank_users.Add(user);

                OpenLoginPage();

            }
            #endregion
        }
        #endregion

        #region Delete User
        /// <summary>
        /// This function will delete user account from database
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteUserAccount(int Id)
        {
            error_delete_account = "";

            if (bank_users[Id].IfHasBankAccount == false)
            {
                bank_users.RemoveAt(Id);
            }
            else
            {
                error_delete_account = "You can't delete your user account. You must close all of your bank accounts first!";
            }
        }
        #endregion

        #region Edit User
        /// <summary>
        /// This function will edit user informtaions
        /// </summary>
        public void EditUserInformation()
        {
            if(is_logged_in == true)
            {
                #region Reset error variables
                name_error = "";
                lastname_error = "";
                email_error = "";
                phone_number_error = "";
                postcode_error = "";
                street_address_error = "";
                street_number_error = "";
                #endregion
                
                bool formula_error = false;

                #region Name and Lastname validation
                if (edit_user.Name == "")
                {
                    formula_error = true;
                    name_error = "Please enter your name";
                }

                if (edit_user.Lastname == "")
                {
                    formula_error = true;
                    lastname_error = "Please enter your lastname";
                }
                #endregion

                #region Email validation 
                if (edit_user.Email == "")
                {
                    formula_error = true;
                    email_error = "Please enter your email";
                }
                else
                {
                    bool if_email_valid = IsValidEmail(edit_user.Email);

                    if (if_email_valid == false)
                    {
                        formula_error = true;
                        email_error = "Email is not valid!";
                    }
                }
                #endregion

                #region Phone number validation
                if (edit_user.PhoneNumber == "")
                {
                    formula_error = true;
                    phone_number_error = "Please enter your phone number";
                }
                else
                {
                    bool if_phone_number_vaild = IsPhoneNumber(edit_user.PhoneNumber);

                    if (if_phone_number_vaild == false)
                    {
                        formula_error = true;
                        phone_number_error = "Phone number is not valid!";
                    }
                }
                #endregion

                #region Postcode validation
                if (edit_user.Postcode == "")
                {
                    formula_error = true;
                    postcode_error = "Please enteryour postcode";
                }
                else
                {
                    bool if_postcode_valid = IsValidPostcode(edit_user.Postcode);

                    if (if_postcode_valid == false)
                    {
                        formula_error = true;
                        postcode_error = "Postcode is not valid";
                    }
                    //In real world app it is good to use database of postcodes to validate postcode
                    //It can be something like Postcode Address File (PAF), but since subscibtion is paid in this app ill juse just regex
                    //More info https://ideal-postcodes.co.uk/guides/postcode-validation
                }
                #endregion

                #region Street address validation
                if (edit_user.StreetAddress == "")
                {
                    formula_error = true;
                    street_address_error = "Please enter your street address";
                }
                else
                {
                    //Herewith use of database and postcode determine if address is real
                    //For this app I need to asume that user will input right address
                }
                #endregion

                #region Street number
                if (edit_user.StreetNumber == "")
                {
                    formula_error = true;
                    street_number_error = "Please enter your street number";
                }
                else
                {
                    //Here as well number should come from database based on street and postcode
                }
                #endregion

                if(formula_error == false)
                {
                    loggedin_user = edit_user;
                    bank_users[loggedin_user.Id] = edit_user;

                    #region clear input variables
                    name = "";
                    lastname = "";
                    email = "";
                    phone_number = "";
                    postcode = "";
                    street_address = "";
                    street_number = "";
                    #endregion
                }
            }
        }
        /// <summary>
        /// This function will edit user password
        /// </summary>
        public void EditUserPassword()
        {
            if(is_logged_in == true)
            {
                password_error = "";
                password_confirmation_error = "";

                bool formula_error = false;

                #region Password validation
                if (loggedin_user.Password == "")
                {
                    formula_error = true;
                    password_error = "Please enter password number";
                }
                else
                {
                    bool is_password_valid = IsValidPassword(loggedin_user.Password);

                    if (is_password_valid == false)
                    {
                        formula_error = true;
                        password_error = "Password is not valid!";
                    }
                }

                if (formula_error == false)
                {
                    if (password_confirmation == loggedin_user.Password)
                    {
                        formula_error = true;
                        password_confirmation_error = "Passwords are not equal";
                    }
                }
                #endregion

                #region Password reminder question and answer
                if (password_reminder_question == "")
                {
                    formula_error = true;
                    password_reminder_question_error = "Please write a question for your reminder";
                }

                if (password_reminder_answer == "")
                {
                    formula_error = true;
                    password_reminder_answer_error = "Please write answerto your question";
                }
                #endregion

                if(formula_error == false)
                {
                    loggedin_user = edit_user;
                    bank_users[loggedin_user.Id] = edit_user;

                    password = "";
                    password_confirmation = "";
                }
            }
        }



        #endregion

        #region Login Logoff functions
        /// <summary>
        /// This function will login user to account
        /// </summary>
        public void Login()
        {
            password_error = "";
            email_error = "";

            bool error_form = false;
            if(email == "")
            {
                error_form = true;
                email_error = "Please enter your email";
            }
            if (password == "")
            {
                error_form = true;
                password_error = "Please enter your password";
            }

            if(error_form == false)
            {
                string pin = pin_1 + pin_2 + pin_3;

                for(int i = 0; i < bank_users.Count; i++)
                {
                    if(email == bank_users[i].Email && password == bank_users[i].Password && pin == bank_users[i].PIN)
                    {
                        loggedin_user = bank_users[i];
                        is_logged_in = true;

                        //Give user access to accounts during session
                        if(checking_accounts.Count != 0)
                        {
                            for (int j = 0; j < checking_accounts.Count; j++)
                            {
                                if (checking_accounts[j].UserUniqueId == loggedin_user.UniqueId)
                                {
                                    checking_account = checking_accounts[j];
                                }
                            }
                        }

                        if(saving_accounts.Count != 0)
                        {
                            for (int j = 0; j < saving_accounts.Count; j++)
                            {
                                if (saving_accounts[j].UserUniqueId == loggedin_user.UniqueId)
                                {
                                    saving_account = saving_accounts[j];
                                }
                            }
                        }

                        if(retirement_accounts.Count != 0)
                        {
                            for (int j = 0; j < retirement_accounts.Count; j++)
                            {
                                if (retirement_accounts[j].UserUniqueId == loggedin_user.UniqueId)
                                {
                                    retirment_account = retirement_accounts[j];
                                }
                            }
                        }

                        if(CD_accounts.Count != 0)
                        {
                            for (int j = 0; j < CD_accounts.Count; j++)
                            {
                                if (CD_accounts[j].UserUniqueId == loggedin_user.UniqueId)
                                {
                                    cd_account = CD_accounts[j];
                                }
                            }
                        }

                        if(money_market_accounts.Count != 0)
                        {
                            for (int j = 0; j < money_market_accounts.Count; j++)
                            {
                                if (money_market_accounts[j].UserUniqueId == loggedin_user.UniqueId)
                                {
                                    mmn_account = money_market_accounts[j];
                                }
                            }
                        }
                        OpenUserPage();
                        ReloadServices();
                    }
                }

                if(is_logged_in == false)
                {
                    error_login = "Wrong email, pin or password!";
                }
            }
        }

        /// <summary>
        /// This function will change value for 1st pin number
        /// </summary>
        public void ChangePin1(string n)
        {
            pin_1 = n;
            TogglePin1();
        }

        /// <summary>
        /// This function will change value for 1st pin number
        /// </summary>
        public void ChangePin2(string n)
        {
            pin_2 = n;
            TogglePin2();
        }

        /// <summary>
        /// This function will change value for 1st pin number
        /// </summary>
        public void ChangePin3(string n)
        {
            pin_3 = n;
            TogglePin3();
        }

        public void TogglePin1()
        {
            if (class_pin_1 == "display_none")
            {
                class_pin_1 = "display_grid";
            }
            else
            {
                class_pin_1 = "display_none";
            }
        }

        public void TogglePin2()
        {
            if (class_pin_2 == "display_none")
            {
                class_pin_2 = "display_grid";
            }
            else
            {
                class_pin_2 = "display_none";
            }
        }

        public void TogglePin3()
        {
            if (class_pin_3 == "display_none")
            {
                class_pin_3 = "display_grid";
            }
            else
            {
                class_pin_3 = "display_none";
            }
        }

        /// <summary>
        /// This function will logoff user from account
        /// </summary>
        public void LogOff()
        {
            if (is_logged_in == true)
            {
                loggedin_user = new BankUser();
                edit_user = new BankUser();
                is_logged_in = false;
                ToggleNav();
            }
        }
        #endregion

        #region User Page Functions
        #region Toggle variables
        protected List<string> unsigned_services_names = new List<string>();

        protected bool checking_page = false;
        protected bool transfer_checking = false;

        #endregion

        #region Toggle Accounts and Services Functions
        /// <summary>
        /// This function will change view for users accounts
        /// </summary>
        /// <param name="name"></param>
        public void ChangeAccountView(string name)
        {
            switch (name)
            {
                case "Checking Account":
                    {
                        OpenCheckingAccount();
                        break;
                    }
                case "Saving Account":
                    {
                        OpenSavingAccount();
                        break;
                    }
                case "Money Market Account":
                    {
                        OpenMMAccountPage();
                        break;
                    }
                case "Retirement Account":
                    {
                        OpenRetirmentAccountPage();
                        break;
                    }
                case "Investment Account":
                    {
                        OpenInvestmentAccountPage();
                        break;
                    }
                case "CD Account":
                    {
                        OpenCDAccountPage();
                        break;
                    }
                default:
                    break;
            }
        }
        public void AddNewServicesView(string name)
        {
            switch (name)
            {
                case "Checking Account":
                    {
                        DebitCard debit = new DebitCard();
                        CheckingAccount new_checkingAccount = new CheckingAccount();



                        debit.CardNumber += '4';

                        for(int i = 0; i < 15; i++)
                        {
                            debit.CardNumber += (RandomNumber(0, 9)).ToString();
                        }

                        debit.Id = debit_cards_list.Count;
                        debit.Name = "Debit Card";

                        for (int i = 0; i < 4; i++)
                        {
                            debit.SecurityCode1 += (RandomNumber(0, 9)).ToString();
                        }

                        for (int i = 0; i < 3; i++)
                        {
                            debit.SecurityCode2 += (RandomNumber(0, 9)).ToString();
                        }

                        debit.UserUniqueId = loggedin_user.UniqueId;

                        new_checkingAccount.Balance = 150000;
                        //Generate account number
                        for(int i = 0; i < 11; i++)
                        {
                            new_checkingAccount.AccountNumber += (RandomNumber(0, 9)).ToString();
                        }
                        new_checkingAccount.DebitCard = debit;
                        new_checkingAccount.Id = checking_accounts.Count;
                        new_checkingAccount.Name = loggedin_user.Name;
                        new_checkingAccount.Name = "Checking Account";

                        debit.CheckingAccount = new_checkingAccount;
                        //Generate account number
                        for (int i = 0; i < 6; i++)
                        {
                            new_checkingAccount.SortCode += (RandomNumber(0, 9)).ToString();
                        }
                        new_checkingAccount.UserUniqueId = loggedin_user.UniqueId;

                        loggedin_user.bankAccounts.Add(new_checkingAccount);
                        checking_accounts.Add(new_checkingAccount);
                        checking_account = new_checkingAccount;

                        ReloadServices();
                        OpenUserPage();
                        break;
                    }
                case "Saving Account":
                    {
                        SavingBankAccount new_savingBank = new SavingBankAccount();
                        //Generate account number
                        for (int i = 0; i < 11; i++)
                        {
                            new_savingBank.AccountNumber += (RandomNumber(0, 9)).ToString();
                        }

                        new_savingBank.Balance = 0;
                        new_savingBank.Id = saving_accounts.Count;
                        new_savingBank.Interest = 1.5;
                        new_savingBank.InterestIteration = 1;
                        new_savingBank.Name = "Saving Account";
                        new_savingBank.UserUniqueId = loggedin_user.UniqueId;

                        loggedin_user.bankAccounts.Add(new_savingBank);
                        saving_accounts.Add(new_savingBank);
                        saving_account = new_savingBank;

                        ReloadServices();
                        OpenUserPage();
                        break;
                    }
                case "Money Market Account":
                    {
                        MoneyMarketAccount new_money_m = new MoneyMarketAccount();
                        //Generate account number
                        for (int i = 0; i < 11; i++)
                        {
                            new_money_m.AccountNumber += (RandomNumber(0, 9)).ToString();
                        }

                        new_money_m.Balance = 0;
                        new_money_m.Id = money_market_accounts.Count;
                        new_money_m.MaxInterest = 8;
                        new_money_m.MaxOutput = 2;
                        new_money_m.MinBalance = 20000;
                        new_money_m.Name = "Money Market Account";
                        new_money_m.UserUniqueId = loggedin_user.UniqueId;

                        loggedin_user.bankAccounts.Add(new_money_m);
                        money_market_accounts.Add(new_money_m);
                        mmn_account = new_money_m;

                        ReloadServices();
                        OpenUserPage();
                        break;
                    }
                case "Retirement Account":
                    {
                        RetirmentAccount new_retirment = new RetirmentAccount();
                        //Generate account number
                        for (int i = 0; i < 11; i++)
                        {
                            new_retirment.AccountNumber += (RandomNumber(0, 9)).ToString();
                        }

                        new_retirment.Balance = 0;
                        new_retirment.Id = retirement_accounts.Count;
                        new_retirment.Name = "Retirement Account";
                        new_retirment.UserUniqueId = loggedin_user.UniqueId;

                        loggedin_user.bankAccounts.Add(new_retirment);
                        retirement_accounts.Add(new_retirment);
                        retirment_account = new_retirment;

                        OpenRetirmentAccountPage();
                        break;
                    }
                case "Investment Account":
                    {
                        OpenInvestmentAccountPage();
                        break;
                    }
                case "CD Account":
                    {
                        CDAccount new_cd = new CDAccount();
                        //Generate account number
                        for (int i = 0; i < 11; i++)
                        {
                            new_cd.AccountNumber += (RandomNumber(0, 9)).ToString();
                        }

                        new_cd.Balance = 0;
                        new_cd.Id = CD_accounts.Count;
                        new_cd.Name = "CD Account";
                        new_cd.UserUniqueId = loggedin_user.UniqueId;

                        OpenCDAccountPage();
                        break;
                    }
                case "Loan":
                    {
                        OpenLoanPage();
                        break;
                    }
                default:
                    break;
            }
        }


        #endregion

        #endregion

        #region Transfer Money
        protected bool transfer_to_users = false;

        protected string account_chocie_1 = "";
        protected string account_chocie_2 = "";

        //Input variables for transfer
        protected string sort1 = "";
        protected string sort2 = "";
        protected string sort3 = "";
        protected string reference = "";
        protected string account_number = "";
        protected string pin = "";
        protected double money = 0;

        //Error variables
        protected string sort_error = "";
        protected string reference_error = "";
        protected string account_number_error = "";
        protected string pin_error = "";
        protected string money_error = "";
        protected string balance_error = "";
        protected string account_choice_error = "";
        protected string account_error = "";


        public void ResetTransferErrorVariables()
        {
            sort_error = "";
            reference_error = "";
            account_number_error = "";
            pin_error = "";
            money_error = "";
            password_error = "";
            balance_error = "";
            account_error = "";
        }

        public void ResetTransferVariables()
        {
            sort1 = "";
            sort2 = "";
            sort3 = "";
            reference = "";
            account_number = "";
            pin = "";
            money = 0;
            password = "";
            account_chocie_1 = "";
            account_chocie_2 = "";
            
        }

        public void CloseTransfer()
        {
            transfer_to_users = false;
            transfer_checking = false;
            ResetTransferErrorVariables();
            ResetTransferVariables();
        }
        /// <summary>
        /// Thisfunction will transfer money from one users account to the other
        /// </summary>
        public void TransferToUsersAccount()
        {
            bool form_error = false;
            ResetTransferErrorVariables();

            if(money == 0)
            {
                form_error = true;
                money_error = "Please enter ammount of money you want to send";
            }

            if(pin != loggedin_user.PIN)
            {
                form_error = true;
                pin_error = "Wrong pin number!";
            }

            if (form_error == false)
            {
                if(account_chocie_1 == account_chocie_2)
                {
                    account_choice_error = "You cannot transwer between same accout!";
                }
                else
                {
                    //If transfer will be successfull it will addmoney to the other account
                    bool transfer_success = false;
                    DateTime dt = new DateTime();
                    dt = DateTime.Now;
                    switch (account_chocie_1)
                    {
                        case "Checking Account":
                            {
                                if(checking_account.Balance < money)
                                {
                                    money_error = "Not enough money!";
                                }
                                else
                                {
                                    checking_account.Balance -= money;
                                    checking_account.MoneyInOut.Insert( 0, new MoneyInOut(checking_account.MoneyInOut.Count + 1, dt, "-" + money.ToString(), account_chocie_2));
                                    checking_accounts[checking_account.Id] = checking_account;
                                    transfer_success = true;
                                }
                                break;
                            }

                        case "Retirement Account":
                            {
                                account_error = "You can't transfer from retirment account!";
                                break;
                            }

                        case "CD Account":
                            {
                                if (cd_account.Balance < money)
                                {
                                    money_error = "Not enough money!";
                                }
                                else
                                {
                                    cd_account.Balance -= money;
                                    cd_account.MoneyInOut.Insert( 0, new MoneyInOut(cd_account.MoneyInOut.Count + 1, dt, "-" + money.ToString(), account_chocie_2));
                                    CD_accounts[cd_account.Id] = cd_account;
                                    transfer_success = true;
                                }
                                break;
                            }

                        case "Money Market Account":
                            {
                                if (mmn_account.Balance < 10000)
                                {
                                    money_error = "Balacne on Money Market account needs to be at least 10000";
                                }
                                else if (mmn_account.MaxOutput == 0)
                                {
                                    account_error = "You cant transfer out of money market account more than twice per one month!";
                                }
                                else 
                                { 
                                mmn_account.Balance -= money;
                                    mmn_account.MoneyInOut.Insert(0, new MoneyInOut(mmn_account.MoneyInOut.Count + 1, dt, "-" + money.ToString(), account_chocie_2));
                                    money_market_accounts[mmn_account.Id] = mmn_account;
                                    mmn_account.MaxOutput--;
                                    transfer_success = true;
                                }
                                break;
                            }
                        case "Saving Account":
                            {
                                if (saving_account.Balance < money)
                                {
                                    money_error = "Not enough money!";
                                }
                                else
                                {
                                    saving_account.Balance -= money;
                                    saving_account.MoneyInOut.Insert( 0, new MoneyInOut(saving_account.MoneyInOut.Count + 1, dt, "-" + money.ToString(), account_chocie_2));
                                    saving_accounts[saving_account.Id] = saving_account;
                                    transfer_success = true;
                                }
                                break;
                            }

                        case "Investment Account":
                            {
                                if (investment_account.Balance < money)
                                {
                                    money_error = "Not enough money!";
                                }
                                else
                                {
                                    investment_account.Balance -= money;
                                    investment_account.MoneyInOut.Insert( 0 , new MoneyInOut(investment_account.MoneyInOut.Count + 1, dt, "-" + money.ToString(), account_chocie_2));
                                    investment_accounts[investment_account.Id] = investment_account;
                                    transfer_success = true;
                                }
                                break;
                            }

                        default: break;
                    }

                    if(transfer_success == true)
                    {
                        DateTime d = new DateTime();
                        d = DateTime.Now; 
                        switch (account_chocie_2)
                        {
                            case "Checking Account":
                                {

                                    checking_account.Balance += money;
                                    checking_account.MoneyInOut.Insert(0, new MoneyInOut(checking_account.MoneyInOut.Count, d, "+" + money.ToString(), account_chocie_1));
                                    checking_accounts[checking_account.Id] = checking_account;
                                    transfer_success = true;

                                    break;
                                }

                            case "Retirement Account":
                                {

                                    retirment_account.Balance += money;
                                    retirment_account.MoneyInOut.Insert(0, new MoneyInOut(retirment_account.MoneyInOut.Count, d, "+" + money.ToString(), account_chocie_1));
                                    retirement_accounts[retirment_account.Id] = retirment_account;
                                    transfer_success = true;

                                    break;
                                }

                            case "CD Account":
                                {

                                    cd_account.Balance += money;
                                    cd_account.MoneyInOut.Insert(0, new MoneyInOut(cd_account.MoneyInOut.Count, d, "+" + money.ToString(), account_chocie_1));
                                    CD_accounts[cd_account.Id] = cd_account;
                                    transfer_success = true;

                                    break;

                                }

                            case "Money Market Account":
                                {

                                    mmn_account.Balance += money;
                                    mmn_account.MoneyInOut.Insert(0, new MoneyInOut(mmn_account.MoneyInOut.Count, d, "+" + money.ToString(), account_chocie_1));
                                    money_market_accounts[mmn_account.Id] = mmn_account;
                                    transfer_success = true;

                                    break;
                                }
                            case "Saving Account":
                                {

                                    saving_account.Balance += money;
                                    saving_account.MoneyInOut.Insert(0, new MoneyInOut(saving_account.MoneyInOut.Count, d, "+" + money.ToString(), account_chocie_1));
                                    saving_accounts[saving_account.Id] = saving_account;
                                    transfer_success = true;
                                    
                                    break;
                                }

                            case "Investment Account":
                                {

                                    investment_account.Balance += money;
                                    investment_account.MoneyInOut.Insert(0, new MoneyInOut(investment_account.MoneyInOut.Count, d, "+" + money.ToString(), account_chocie_1));
                                    investment_accounts[investment_account.Id] = investment_account;
                                    transfer_success = true;

                                    break;
                                }

                            default: break;
                        }
                        //When transfer succesfull open user account
                        OpenUserPage();
                    }
                }
            }
        }




        /// <summary>
        /// This function will do transfer of money to other account
        /// </summary>
        public void TransferToOthers()
        {
            ResetTransferErrorVariables();

            bool form_error = false;

            if(money == 0)
            {
                form_error = true;
                money_error = "Please enter ammount";
            }

            if(account_number.Length < 9)
            {
                form_error = true;
                account_number_error = "Please enter valid account number";
            }
            else
            {
                //Here there should be some chceck from database of the accounts connected with its sort codes
            }

            if(sort1.Length < 2 || sort2.Length < 2 || sort3.Length < 2)
            {
                form_error = true;
                sort_error = "Please enter valid sortcode";
            }

            if(reference == "")
            {
                form_error = true;
                reference_error = "Please enter reference";
            }

            if(password != loggedin_user.Password)
            {
                form_error = true;
                password_error = "Password is not valid";
            }

            if(pin != loggedin_user.PIN)
            {
                form_error = true;
                pin_error = "Wrong pin number";
            }

            if(form_error == false)
            {

                if(money > checking_account.Balance)
                {
                    balance_error = "You do not have enough money!";
                }
                else
                {
                    checking_account.Balance -= money;

                    DateTime d = new DateTime();
                    d = DateTime.Now;
                    
                    checking_account.MoneyInOut.Insert(0, new MoneyInOut(checking_account.MoneyInOut.Count, d, "-" + money.ToString(), reference));

                    ten.Clear();

                    if(checking_account.MoneyInOut.Count < 10)
                    {
                        for(int i = 0; i < checking_account.MoneyInOut.Count; i++)
                        {
                            ten.Add(checking_account.MoneyInOut[i]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            ten.Add(checking_account.MoneyInOut[i]);
                        }
                    }

                    ResetTransferVariables();
                    OpenCheckingAccount();
                    //Here there should be implemented some external function which will do transfer of money to other account
                }
            }
        }
        #endregion

        #region Cheking Account Page / Change Card View / See Balance History
        protected int card_show_counter = 1;
        protected int minus = -1;
        protected int plus = 1;
        //This will change view for card 
        public void ChangeCardViewPlus()
        {
            card_show_counter = card_show_counter + 1;
        }

        public void ChangeCardViewMinus()
        {
            card_show_counter = card_show_counter - 1;
        }

        #region Transfer Data Money in out Data modyfier functions

        protected List<MoneyInOut> ten = new List<MoneyInOut>();
        protected int ten_max_iterator = 10;
        protected int ten_min_iterator = 0;


       
        public void ShowTen(List<MoneyInOut> m)
        {
            ten.Clear();

            if(m.Count < 10)
            {
                for (int i = 0; i < m.Count; i++)
                {
                    if (m[i] != null)
                    {
                        ten.Add(m[i]);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    if (m[i] != null)
                    {
                        ten.Add(m[i]);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        public void ShowNextTen(List<MoneyInOut> m)
        {
            ten_max_iterator += 10;
            ten_min_iterator += 10;
            ten.Clear();

            if(m.Count < ten_max_iterator)
            {
                ten_max_iterator = ten_max_iterator - (ten_max_iterator - m.Count);

                for (int i = ten_min_iterator; i < ten_max_iterator; i++)
                {
                    if (m[i] != null)
                    {
                        ten.Add(m[i]);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            {
                for (int i = ten_min_iterator; i < ten_max_iterator; i++)
                {
                    if (m[i] != null)
                    {
                        ten.Add(m[i]);
                    }
                    else
                    {
                        continue;
                    }
                }
            }

        }

        public void ShowPreviousTen(List<MoneyInOut> m)
        {
            ten.Clear();

            ten_min_iterator -= 10;
            ten_max_iterator -= 10;

            if(ten_max_iterator < (ten_min_iterator + 10))
            {
                ten_max_iterator = ten_min_iterator + 10;
                for (int i = ten_min_iterator; i < ten_max_iterator; i++)
                {
                    if (m[i] != null)
                    {
                        ten.Add(m[i]);
                    }
                    else
                    {
                        continue;
                    }
                }

            }
            else
            {
                for (int i = ten_min_iterator; i < ten_max_iterator; i++)
                {
                    if (m[i] != null)
                    {
                        ten.Add(m[i]);
                    }
                    else
                    {
                        continue;
                    }
                }
            }

        }
        #endregion

        #endregion

        #endregion//End of user

        #region Pin number display
        protected bool if_show_pin = false;

        protected string pin_show_1 = "";
        protected string pin_show_2 = "";
        protected string pin_show_3 = "";

        protected string pin_toggle_error = "";

        /// <summary>
        /// This function will randomize pin number when user will request it to log in or to make a payment ect.
        /// </summary>
        public void RandomizePin(BankUser u)
        {
            bank_users[u.Id].PIN = "";
            for (int i = 0; i < 3; i++)
            {
                bank_users[u.Id].PIN += (RandomNumber(0,9)).ToString();
            }
        }

        public void ShowPin()
        {
            pin_toggle_error = "";

            RandomizePin(dave_info);
            bank_users[0] = dave_info;
            if_show_pin = true;

            for(int i = 0; i < bank_users.Count; i++)
            {
                bank_users[i].PIN = dave_info.PIN;
            }

            pin_show_1 = dave_info.PIN[0].ToString();
            pin_show_2 = dave_info.PIN[1].ToString();
            pin_show_3 = dave_info.PIN[2].ToString();
        }


        #endregion

        #region Loan
        protected double max_loan = 0;
        protected double min_loan = 1000;
        protected double overall_balance = 0;
        protected int duration = 1;
        protected double loan_take = 0;
        protected string loan_error = "";

        protected bool show_loan_options = false;
        protected bool show_loan_final = false;

        protected Loan show_case_loan = new Loan();
        protected List<LoanOption> loanOptions = new List<LoanOption>();



        /// <summary>
        /// This function will calculate credit score
        /// </summary>
        /// In this example I will just calculate it with getting 70% of overall balance of bank accounts
        /// In real world example this would be done by algorithm which will check constant balance on users acounts
        /// and overall balance
        public void CalculateCreditScore()
        {

            if(loggedin_user.MaximumCredit == 0)
            {
                for (int i = 0; i < loggedin_user.bankAccounts.Count; i++)
                {
                    overall_balance += loggedin_user.bankAccounts[i].Balance;
                    max_loan = Math.Round(overall_balance * 70 / 100, 2);
                    loggedin_user.MaximumCredit = max_loan;
                }
            }
            else
            {
                max_loan = loggedin_user.MaximumCredit;
            }
            
        }

        /// <summary>
        /// This function will show options to choose for the user
        /// </summary>
        /// <param name="lt"></param>
        public void SeeLoanOptions(double lt)
        {

            if(lt <= max_loan && lt >= 1000) {
            show_loan_options = true;

                if (lt <= 50000)
                {
                    loanOptions.Add(new LoanOption(0, 6, lt, 10));
                    loanOptions.Add(new LoanOption(1, 12, lt, 11));
                    loanOptions.Add(new LoanOption(2, 24, lt, 13));
                }

                if (lt > 50000 && lt < 100000)
                {
                    loanOptions.Add(new LoanOption(0, 12, lt, 5));
                    loanOptions.Add(new LoanOption(1, 24, lt, 7));
                    loanOptions.Add(new LoanOption(2, 48, lt, 9));
                }

                if (lt >= 100000)
                {
                    loanOptions.Add(new LoanOption(0, 12, lt, 4.5));
                    loanOptions.Add(new LoanOption(1, 24, lt, 6.5));
                    loanOptions.Add(new LoanOption(2, 48, lt, 8.5));
                }

            }
            else if(max_loan < lt)
            {
                loan_error = "Your credit score is too low!";
            }
            else if(lt < 1000)
            {
                loan_error = "You need to borrow at least £1000";
            }
        }

        /// <summary>
        /// This function will show loan terms to the user
        /// </summary>
        /// <param name="l"></param>
        public void ChooseLoanOption(LoanOption l)
        {
            show_case_loan.AmmountBorrowed = l.Ammount;
            show_case_loan.AmmountToPay = Math.Round(l.Ammount + (l.Ammount * l.Interest / 100), 2);
            show_case_loan.Duration = l.Duration;
            show_case_loan.Id = 0;
            show_case_loan.Interest = l.Interest;
            show_case_loan.Paid = 0;
            show_case_loan.PartialPay = Math.Round((l.Ammount + (l.Ammount * l.Interest / 100)) / l.Duration, 2);

            show_loan_final = true;
        }

        /// <summary>
        /// Add loan To the account
        /// </summary>
        public void TakeLoan()
        {
            loggedin_user.MaximumCredit = Math.Round(loggedin_user.MaximumCredit - show_case_loan.AmmountBorrowed, 2);
            loggedin_user.loans.Add(show_case_loan);
            bank_users[loggedin_user.Id] = loggedin_user;

            OpenUserPage();
        }
        #endregion

        #region Toggle Pages

        #region Toggle sites variables
        protected bool main_page = true;
        protected bool register_page = false;
        protected bool login_page = false;
        protected bool user_page = false;
        protected bool loan_page = false;

        protected bool cd_account_page = false;
        protected bool saving_account_page = false;
        protected bool mm_account_page = false;
        protected bool retirment_account_page = false;
        protected bool investment_account_page = false;
        #endregion

        #region Toggle sites functions

        public void CloseAllPages()
        {
            register_page = false;
            login_page = false;
            user_page = false;
            checking_page = false;
            transfer_to_users = false;
            saving_account_page = false;
            main_page = false;
            transfer_checking = false;
            cd_account_page = false;
            mm_account_page = false;
            retirment_account_page = false;
            investment_account_page = false;
            loan_page = false;
            if_show_pin = false;

            //Resetvariables
            ResetUserVariables();
            ResetVariablesErrors();
            ResetTransferVariables();
            ResetTransferErrorVariables();
            ResetLoanVariables();
            ResetRetairmentFundsVariables();
            ToggleNav();
        }

        public void OpenLoanPage()
        {
            //Close All pages
            CloseAllPages();

            //Calculate credit score
            CalculateCreditScore();

            //Open page
            loan_page = true;
        }

        public void OpenInvestmentAccountPage()
        {
            //Close all pages
            CloseAllPages();

            //Open page
            investment_account_page = true;
        }

        public void OpenRetirmentAccountPage()
        {
            //Close all pages
            CloseAllPages();

            //Open page
            retirment_account_page = true;
            ShowTen(retirment_account.MoneyInOut);
        }

        public void OpenMMAccountPage()
        {
            //Close all pages
            CloseAllPages();

            //Open page
            mm_account_page = true;
            ShowTen(mmn_account.MoneyInOut);
        }

        public void OpenCDAccountPage()
        {
            //Close all pages
            CloseAllPages();

            //Open page
            cd_account_page = true;
            ShowTen(cd_account.MoneyInOut);
        }

        public void OpenMainPage()
        {
            //Close all pages
            CloseAllPages();
            ResetInterestVariables();

            //Open page
            main_page = true;
        }

        public void OpenLoginPage()
        {
            //Close all pages
            CloseAllPages();

            //Open page
            login_page = true;

        }

        public void OpenCheckingAccount()
        {

            //Close all pages
            CloseAllPages();

            //Open page
            checking_page = true;

            ShowTen(checking_account.MoneyInOut);

        }

        public void OpenSavingAccount()
        {

            //Close all pages
            CloseAllPages();

            //Open page
            saving_account_page = true;


            ShowTen(saving_account.MoneyInOut);

        }

        public void OpenTransferCheckingAccount()
        {
            //Close all pages
            CloseAllPages();

            //Open Page
            transfer_checking = true;
        }

        public void OpenRegisterPage()
        {
            //Close all pages
            CloseAllPages();

            //Open page
            register_page = true;

        }

        public void OpenUserPage()
        {
            //Close all pages
            CloseAllPages();
            ResetInterestVariables();

            //Open page
            user_page = true;

        }

        /// <summary>
        /// It will open transfer of money one of users accounts
        /// </summary>
        public void OpenTransferInsideUsers()
        {
            //Close all pages
            CloseAllPages();

            //Open Section
            transfer_to_users = true;
        }
        #endregion
        #endregion

        #region Calculate Retirment Funds

        protected int user_age = 0;
        protected double funds_on_retirment = 0;
        protected double average_input = 0;
        protected bool see_funds = false;

        public void CalculateRetirmentFunds()
        {
            List<double> money_list = new List<double>();
            double money_sum = 0;
            int counter = 0;
            for (int i = 0; i < retirment_account.MoneyInOut.Count; i++)
            {
                string test = retirment_account.MoneyInOut[i].Reference;

                if (test == "Increase Funds")
                {
                    string mio = retirment_account.MoneyInOut[i].Money.Trim('+', '-');
                    money_list.Add(Convert.ToDouble(mio));
                    money_sum += money_list[counter];
                    counter++;
                }
                else
                {
                    //
                }
            }

            average_input = money_sum / (money_list.Count);

            user_age = DateTime.Now.Year - loggedin_user.BirthDate.Year;

            funds_on_retirment = retirment_account.Balance + (average_input * (12 * (67 - user_age)));
            see_funds = true;


        }
        #endregion

        #region Interest



        #region Lower risk interest
        protected double bank_earnings = 0;
        protected double compound_iterest = 0;
        protected bool show_bank_earnings = false;

        /// <summary>
        /// This function will simiulate earnings from the interest for the account holder and bank.
        /// Banks are earning money from interest after 3rd decimal place
        /// </summary>
        /// <param name="iterator">How many months to simiulate</param>
        /// <param name="account">Account to which apply changes</param>
        /// <param name="interest_rate">Yearly ammount of interest(To get monhly one the value is divided by 12)</param>
        public void SimiulateSafeInterestMonthly(int iterator, BankAccount account, double interest_rate)
        {
            double balance = account.Balance;
            string interest_ammount_string = "";

            double interest_ammount = 0;
            interest_rate = interest_rate / iterator;
            


            for (int i = 0; i < iterator; i++)
            {
                interest_ammount = balance * interest_rate / 100;
                interest_ammount_string = interest_ammount.ToString();

                //If "," is going to be found function will change iteration ammount
                int break_point = interest_ammount_string.Length;

                StringBuilder s = new StringBuilder();

                //Calculate earnings for the bank
                for(int j = 0; j < break_point; j++)
                {
                    char c = interest_ammount_string[j];
                    if(c == ',')
                    {
                        break_point = j + 3;
                    }
                    if(interest_ammount_string.Length < break_point)
                    {
                        break_point = j + 2;
                    }

                    s.Append(interest_ammount_string[j]);
                }

                compound_iterest += interest_rate;
                double interest_fix = Convert.ToDouble(s.ToString());

                //Bank earns from the 3rd decimal point of the interest earnings
                bank_earnings += interest_ammount - interest_fix;

                interest_ammount = interest_fix;

                balance += interest_ammount;

                MoneyInOut m = new MoneyInOut(account.MoneyInOut.Count, DateTime.Now, "+" + interest_ammount.ToString(), "Interest(Rate:" + Math.Round(interest_rate, 3).ToString() + "(3d))");
                account.Balance = Math.Round(balance , 2);
                account.MoneyInOut.Insert(0, m);

            }

            //This function will find to which account apply changes
            switch (account.Name)
            {
                case "Saving Account":
                    {
                        saving_account.MoneyInOut = account.MoneyInOut;
                        saving_account.Balance = account.Balance;
                        saving_accounts[account.Id] = saving_account;
                        OpenSavingAccount();
                        break;
                    }
                default:
                    break;
            }

            show_bank_earnings = true;

        }
        #endregion

        #region Higher Risk Interest


        
        protected double compound_inflation = 0;
        protected string inflation_message = "";

        /// <summary>
        /// This function will simiulate interest rate with higher risk. It means that inflation and some random "market" modyfiers are going to be included
        /// </summary>
        /// <param name="iterator">How many months to simiulate</param>
        /// <param name="account">Account to which apply changes</param>
        /// <param name="interest_rate">Max yearly interest adverised by bank(To get monhly one the value is divided by 12)</param>
        /// <param name="risk_rate">This variable will simiulate if changes are big or small</param>
        /// <param name="rate_risk_occurence_modyfier">This variable will change if ratio of good events is higher than negative. Higher the number better interest</param>
        /// <param name="inflation_modyfier">This value simiulates how big inflation changes occure</param>
        /// <param name="inflation_risk_occurence_modyfier">This value simiulates if changes are increasing or decrasing inflation</param>
        public void SimiulateHigherRiskInterestMonthly(int iterator, BankAccount account, double interest_rate, int risk_rate, int rate_risk_occurence_modyfier, int inflation_modyfier, int inflation_risk_occurence_modyfier)
        {
            double balance = account.Balance;
            string interest_ammount_string = "";

            double interest_ammount = 0;
            interest_rate = interest_rate / iterator;
            

            for (int i = 0; i < iterator; i++)
            {
                //Calculate risk
                double r = RandomNumber(1, risk_rate);
                r = r / 100;
                int positive_negative = RandomNumber(0, rate_risk_occurence_modyfier);
                if(positive_negative == 0)
                {
                    r = r - (2 * r);
                }
                interest_rate = interest_rate + r;
                compound_iterest += interest_rate;
                interest_ammount = balance * interest_rate / 100;
                interest_ammount_string = interest_ammount.ToString();

                //Calculate inflation
                double inflation = RandomNumber(0, inflation_modyfier);

                inflation = inflation / 100;
                positive_negative = RandomNumber(0, inflation_risk_occurence_modyfier);

                if(positive_negative == 0)
                {
                    inflation = inflation - (inflation * 2);
                }

                compound_inflation += inflation;

                //If "," is going to be found function will change iteration ammount
                int break_point = interest_ammount_string.Length;

                StringBuilder s = new StringBuilder();

                //Calculate earnings for the bank
                for (int j = 0; j < break_point; j++)
                {
                    char c = interest_ammount_string[j];
                    if (c == ',')
                    {
                        break_point = j + 3;
                    }
                    if (interest_ammount_string.Length < break_point)
                    {
                        break_point = j + 2;
                    }

                    s.Append(interest_ammount_string[j]);
                }

                double interest_fix = Convert.ToDouble(s.ToString());

                //Bank earns from the 3rd decimal point of the interest earnings
                bank_earnings += interest_ammount - interest_fix;

                interest_ammount = interest_fix;

                balance += interest_ammount;

                MoneyInOut m = new MoneyInOut(account.MoneyInOut.Count, DateTime.Now, "+" + interest_ammount.ToString(), "Interest(Rate:" + Math.Round(interest_rate, 3).ToString() + "(3d))");
                account.Balance = Math.Round(balance, 2);
                account.MoneyInOut.Insert(0, m);

            }

            //This function will find to which account apply changes
            switch (account.Name)
            {
                case "Saving Account":
                    {
                        saving_account.MoneyInOut = account.MoneyInOut;
                        saving_account.Balance = account.Balance;
                        saving_accounts[account.Id] = saving_account;
                        OpenSavingAccount();
                        break;
                    }
                case "Money Market Account":
                    {
                        mmn_account.MoneyInOut = account.MoneyInOut;
                        //When simiulating mma reset ammount of times beeing able to withdraw from account
                        mmn_account.MaxOutput = 2;
                        mmn_account.Balance = account.Balance;
                        money_market_accounts[account.Id] = mmn_account;
                        OpenMMAccountPage();
                        break;
                    }
                case "CD Account":
                    {
                        cd_account.MoneyInOut = account.MoneyInOut;
                        cd_account.Balance = account.Balance;
                        CD_accounts[account.Id] = cd_account;
                        OpenCDAccountPage();
                        break;
                    }
                default:
                    break;
            }

            show_bank_earnings = true;

            if(compound_inflation == 0)
            {
                inflation_message = "There was no changes of inflation this year.";
            }
            else if(compound_inflation < 0)
            {
                inflation_message = "There was negative inflation this year so your money is worth around " + (Math.Round(compound_inflation * 100), 3) + "% more!.";
            }
            else if (compound_inflation > 0)
            {
                inflation_message = "There was positive inflation this year so your money is worth around " + (Math.Round(compound_inflation * 100, 3)) + "% less.";
            }
        }

        #endregion

        #endregion

        #region Navigation Toggle
        protected string nav_toggle = "";
        protected bool show_btn_nav = true;
        public void ToggleNav()
        {
            if(nav_toggle == "")
            {
                nav_toggle = "nav_container_toggled";
                show_btn_nav = false;
            }
            else
            {
                nav_toggle = "";
                show_btn_nav = true;
            }
        }

        #endregion

        #region Help Functions

        /// <summary>
        /// This function will change sortoce code to string with "-"
        /// </summary>
        public string SortcodeToString(string s)
        {
            StringBuilder sort = new StringBuilder();

            sort.Append(s[0]);
            sort.Append(s[1]);
            sort.Append("-");
            sort.Append(s[2]);
            sort.Append(s[3]);
            sort.Append("-");
            sort.Append(s[4]);
            sort.Append(s[5]);

            return sort.ToString();
        }

        /// <summary>
        /// This function will reload services for user page
        /// </summary>
        public void ReloadServices()
        {
            unsigned_services_names.Clear();

            for (int i = 0; i < services_names.Count; i++)
            {
                bool if_signed_in = false;
                for (int j = 0; j < loggedin_user.bankAccounts.Count; j++)
                {
                    if (loggedin_user.bankAccounts[j].Name == services_names[i])
                    {
                        if_signed_in = true;
                    }
                }


                if (if_signed_in == false)
                {
                    unsigned_services_names.Add(services_names[i]);
                }
            }
        }


        /// <summary>
        /// Date Formater
        /// </summary>
        /// <param name="dt">Date input</param>
        /// <returns>Date to string with MM/dd/yyyy format</returns>
        public string DateToString(DateTime dt)
        {
            return dt.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// This function will check if email is vaild
        /// </summary>
        /// <param name="email">This variable will input email</param>
        /// <returns>Email is valid/Email is not valid</returns>
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// This function will reset variables for taking a loan
        /// </summary>
        public void ResetLoanVariables()
        {
            max_loan = 0;
            min_loan = 1000;
            overall_balance = 0;
            duration = 1;
            loan_take = 0;
            loan_error = "";
            show_loan_final = false;
            show_loan_options = false;
            show_case_loan = new Loan();
            loanOptions = new List<LoanOption>();
        }

        /// <summary>
        /// This function will reset variables for calculating retirment funds
        /// </summary>
        public void ResetRetairmentFundsVariables()
        {
            user_age = 0;
            funds_on_retirment = 0;
            average_input = 0;
            see_funds = false;
        }

        /// <summary>
        /// This function will reset variables for simiulating interest
        /// </summary>
        public void ResetInterestVariables()
        {
            compound_inflation = 0;
            inflation_message = "";
            show_bank_earnings = false;
            bank_earnings = 0;
            compound_iterest = 0;
        }

        /// <summary>
        /// This function will reset variables for user input
        /// </summary>
        public void ResetUserVariables()
        {
            name = "";
            lastname = "";
            email = "";
            phone_number = "";
            postcode = "";
            street_address = "";
            street_number = "";
            password = "";
            password_reminder_question = "";
            password_reminder_answer = "";
            password_confirmation = "";
            pin_1 = "0";
            pin_2 = "0";
            pin_3 = "0";
    }

        /// <summary>
        /// This function will reset error messages for input
        /// </summary>
        public void ResetVariablesErrors()
        {
            name_error = "";
            lastname_error = "";
            email_error = "";
            phone_number_error = "";
            postcode_error = "";
            street_address_error = "";
            street_number_error = "";
            password_error = "";
            password_confirmation_error = "";
            password_reminder_question_error = "";
            password_reminder_answer_error = "";
        }

        /// <summary>
        /// This function will validate postcode
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        static public bool IsValidPostcode(string postcode)
        {
            return (
                Regex.IsMatch(postcode, "(^[A-PR-UWYZa-pr-uwyz][0-9][ ]*[0-9][ABD-HJLNP-UW-Zabd-hjlnp-uw-z]{2}$)") ||
                Regex.IsMatch(postcode, "(^[A-PR-UWYZa-pr-uwyz][0-9][0-9][ ]*[0-9][ABD-HJLNP-UW-Zabd-hjlnp-uw-z]{2}$)") ||
                Regex.IsMatch(postcode, "(^[A-PR-UWYZa-pr-uwyz][A-HK-Ya-hk-y][0-9][ ]*[0-9][ABD-HJLNP-UW-Zabd-hjlnp-uw-z]{2}$)") ||
                Regex.IsMatch(postcode, "(^[A-PR-UWYZa-pr-uwyz][A-HK-Ya-hk-y][0-9][0-9][ ]*[0-9][ABD-HJLNP-UW-Zabd-hjlnp-uw-z]{2}$)") ||
                Regex.IsMatch(postcode, "(^[A-PR-UWYZa-pr-uwyz][0-9][A-HJKS-UWa-hjks-uw][ ]*[0-9][ABD-HJLNP-UW-Zabd-hjlnp-uw-z]{2}$)") ||
                Regex.IsMatch(postcode, "(^[A-PR-UWYZa-pr-uwyz][A-HK-Ya-hk-y][0-9][A-Za-z][ ]*[0-9][ABD-HJLNP-UW-Zabd-hjlnp-uw-z]{2}$)") ||
                Regex.IsMatch(postcode, "(^[Gg][Ii][Rr][]*0[Aa][Aa]$)")
                );
        }

        /// <summary>
        /// This function vill check if this is valid phone number
        /// </summary>
        /// <param name="number">phone number</param>
        /// <returns>Phone number is valid/ Phone number is not valid</returns>
        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^([0-9]{9})$").Success;
        }

        /// <summary>
        /// This function will check if this is valid passowrd
        /// </summary>
        /// <param name="password">Password variable</param>
        /// <returns>Password is vaild/ Password is not valid</returns>
        public bool IsValidPassword(string password)
        {
            bool is_error = false;
            if (password == null)
            {
                return false;
                is_error = true;
            }

            if (password == "")
            {
                is_error = true;
                return false;
            }
            if (password.Length < 8)
            {
                is_error = true;
                return false;
            }


            if (is_error == false)
            {
                char[] one = password.ToCharArray();
                char[] two = new char[one.Length];
                int c = 0;
                int n = 0;
                for (int i = 0; i < one.Length; i++)
                {
                    if (!Char.IsLetterOrDigit(one[i]))
                    {
                        two[c] = one[i];
                        c++;
                    }
                    if (one[i] == '1' || one[i] == '2' || one[i] == '3' || one[i] == '4' || one[i] == '5' || one[i] == '6' || one[i] == '7' || one[i] == '8' || one[i] == '9')
                    {
                        n++;
                    }
                }
                if (c < 2)
                {
                    return false;
                }

                if (n < 2)
                {
                    return false;
                }
            }
            return true;
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        #endregion Functions
    }
}
