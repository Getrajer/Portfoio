using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BuisnessApp.Pages.Clothe
{
    public class ClotheBase:ComponentBase
    {
        //Class Declaration

        /// <summary>
        /// This is product class
        /// </summary>
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Descritpion { get; set; }
            public string ImgPath { get; set; }
            public double Price { get; set; }
            public int AmmountOfProduct { get; set; }
            public bool IsOnPromotion { get; set; }
            public double PromotionAmmount { get; set; }
            //Brand_Product
            public Brand Brand_P { get; set; }

            public Product() { }
        }

        /// <summary>
        /// This is brand class
        /// </summary>
        public class Brand
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ImgPath { get; set; }

            public Brand() { }
        }

        /// <summary>
        /// This is promotion class
        /// </summary>
        public class Promotion
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double PromotionAmmount { get; set; }
            public string ImgPath { get; set; }
            public DateTime PromotionStart { get; set; }
            public DateTime PromotionEnd { get; set; }
            //PromotionType_Promotion
            public PromotionType PromotionType_Pr { get; set; }

            public Promotion() { }
        }


        /// <summary>
        /// This class determines what type of promotion it is
        /// </summary>
        public class PromotionType
        {
            public int Id { get; set; }
            public bool IfBrandPromotion { get; set; }
            public bool IfSpecificProductPromotion { get; set; }
            public Brand PromotionBrand { get; set; }
            public Product PromotionProduct { get; set; }

            public PromotionType() { }
        }

        /// <summary>
        /// This is user class
        /// </summary>
        public class User
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string StreetAddress { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Password { get; set; }

            public User() { }
        }

        /// <summary>
        /// This is Chart Class
        /// </summary>
        public class Chart
        {
            public int Id { get; set; }
            public User customer { get; set; }
            public double Price { get; set; }

            public List<ChartItem> chartItems = new List<ChartItem>();

            public Chart()
            {
                this.Price = 0;
            }
        }

        /// <summary>
        /// This is Chart Class
        /// </summary>
        public class ChartItem
        {
            public int Id { get; set; }
            public Product product { get; set; }

            public ChartItem() { }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Mock data

        /// <summary>
        /// This list will contain all the products/In real world app it would be changed with databse connection string
        /// </summary>
        protected List<Product> products = new List<Product>();

        /// <summary>
        /// This list will contain 3 latest products
        /// </summary>
        protected List<Product> latestProducts = new List<Product>();

        /// <summary>
        /// This list will contain all brands/In real world app it would be changed with databse connection string
        /// </summary>
        protected List<Brand> brands = new List<Brand>();

        /// <summary>
        /// This list will contain all promotions/In real world app it would be changed with databse connection string
        /// </summary>
        protected List<Promotion> promotions = new List<Promotion>();

        /// <summary>
        /// This list will contain all promotion types/In real world app it would be changed with databse connection string
        /// </summary>
        protected List<PromotionType> promotionTypes = new List<PromotionType>();


        /// <summary>
        /// This list will contain all user accounts/In real world app it would be changed with databse connection string
        /// </summary>
        protected List<User> userAccounts = new List<User>();

        //On initialization create mock data
        protected override async Task OnInitializedAsync()
        {
            //Initialize mock data for brands
            Brand b1 = new Brand(); b1.Id = 0; b1.Name = "Blackoo"; b1.ImgPath = "../ClotheImages/black_jacket.jpg";
            Brand b2 = new Brand(); b2.Id = 1; b2.Name = "Yelloo"; b2.ImgPath = "../ClotheImages/yellow.jpg";
            Brand b3 = new Brand(); b3.Id = 2; b3.Name = "Reddoo"; b3.ImgPath = "../ClotheImages/red.jpg";
            Brand b4 = new Brand(); b4.Id = 3; b4.Name = "Purrpleloo"; b4.ImgPath = "../ClotheImages/purple.jpg";
            Brand b5 = new Brand(); b5.Id = 4; b5.Name = "Pinkoo"; b5.ImgPath = "../ClotheImages/pink.jpg";
            Brand b6 = new Brand(); b6.Id = 5; b6.Name = "Greenoo"; b6.ImgPath = "../ClotheImages/green_dress.jpg";
            //Add mock data for brands
            brands.Add(b1);
            brands.Add(b2);
            brands.Add(b3);
            brands.Add(b4);
            brands.Add(b5);
            brands.Add(b6);

            //Initialize mock data for products
            Product p1 = new Product(); p1.Id = 0; p1.AmmountOfProduct = 10; p1.Name = "Black Jacket"; p1.Descritpion = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tellus velit, molestie ut molestie a, facilisis at purus. Curabitur blandit."; p1.Price = 30; p1.ImgPath = "../ClotheImages/black_jacket.jpg"; p1.Brand_P = brands[0];
            Product p2 = new Product(); p2.Id = 1; p2.AmmountOfProduct = 10; p2.Name = "Yellow T-Shirt"; p2.Descritpion = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tellus velit, molestie ut molestie a, facilisis at purus. Curabitur blandit."; p2.Price = 10; p2.ImgPath = "../ClotheImages/yellow.jpg"; p2.Brand_P = brands[1];
            Product p3 = new Product(); p3.Id = 2; p3.AmmountOfProduct = 10; p3.Name = "Red Shirt"; p3.Descritpion = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tellus velit, molestie ut molestie a, facilisis at purus. Curabitur blandit."; p3.Price = 15; p3.ImgPath = "../ClotheImages/red.jpg"; p3.Brand_P = brands[2];
            Product p4 = new Product(); p4.Id = 3; p4.AmmountOfProduct = 10; p4.Name = "Purple Dress"; p4.Descritpion = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tellus velit, molestie ut molestie a, facilisis at purus. Curabitur blandit."; p4.Price = 40; p4.ImgPath = "../ClotheImages/purple.jpg"; p4.Brand_P = brands[3];
            Product p5 = new Product(); p5.Id = 4; p5.AmmountOfProduct = 10; p5.Name = "Pink Jumper"; p5.Descritpion = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tellus velit, molestie ut molestie a, facilisis at purus. Curabitur blandit."; p5.Price = 20; p5.ImgPath = "../ClotheImages/pink.jpg"; p5.Brand_P = brands[4];
            Product p6 = new Product(); p6.Id = 5; p6.AmmountOfProduct = 10; p6.Name = "Green Dress"; p6.Descritpion = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tellus velit, molestie ut molestie a, facilisis at purus. Curabitur blandit."; p6.Price = 40; p6.ImgPath = "../ClotheImages/green_dress.jpg"; p6.Brand_P = brands[5];
            //Add mock data for products
            products.Add(p1);
            products.Add(p2);
            products.Add(p3);
            products.Add(p4);
            products.Add(p5);
            products.Add(p6);

            for (int i = products.Count - 1; i > products.Count - 4; i--)
            {
                latestProducts.Add(products[i]);
            }

            //Initialize mock data for promotions(This mock promotions are weekly, when implemented they can be diffrent)
            PromotionType pt1 = new PromotionType(); pt1.Id = 0; pt1.IfBrandPromotion = true; pt1.IfSpecificProductPromotion = false; pt1.PromotionBrand = brands[1];
            Promotion d1 = new Promotion(); d1.Id = 0; d1.ImgPath = "../ClotheImages/happy.png"; d1.Name = "Yello Week"; d1.PromotionAmmount = 0.2; d1.PromotionStart = new DateTime(2020, 3, 22, 00, 00, 00); d1.PromotionEnd = new DateTime(2021, 3, 22, 00, 00, 00); d1.PromotionType_Pr = pt1;

            promotions.Add(d1);

            if (d1.PromotionType_Pr.IfBrandPromotion == true)
            {
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].Brand_P == d1.PromotionType_Pr.PromotionBrand)
                    {
                        products[i].Price = products[i].Price - (products[i].Price * d1.PromotionAmmount);
                        products[i].IsOnPromotion = true;
                        products[i].PromotionAmmount = d1.PromotionAmmount * 100;
                    }
                }
            }

            User ex_user = new User();

            ex_user.Id = 0;
            ex_user.City = "London";
            ex_user.Email = "user@user.com";
            ex_user.LastName = "Doe";
            ex_user.Name = "John";
            ex_user.Password = "123";
            ex_user.PhoneNumber = "123456";
            ex_user.PostCode = "N14LT";
            ex_user.StreetAddress = "Street 654";
            ex_user.UserName = "JohnDoe";

            userAccounts.Add(ex_user);

        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Functions and variables to open pages

        //Bool variables wchich are going to check if "sub sites" are going to be open
        protected bool if_main_page_open = true;
        protected bool if_all_product_page_open = false;
        protected bool if_product_page_open = false;
        protected bool if_chart_page_open = false;
        protected bool if_register_page_open = false;
        protected bool if_login_page_open = false;
        protected bool if_shipping_page_open = false;
        protected bool if_admin_page_open = false;
        protected bool if_manage_products_page_open = false;
        protected bool if_manage_users_page_open = false;
        protected bool if_sales_page_open = false;
        protected bool if_edit_product_page_open = false;
        protected bool if_edit_user_page_open = false;

        //This checks if some one tried to go to shippingpage without logging in so it redirect user to shipping page again
        protected bool if_shipping_page_was_open = false;

        //Head Class
        protected string head_class = "head_class_1";


        //Variables for the pages
        protected string head_text = "Clothee Online";

        //Variable for product page
        protected Product product_detail = new Product();



        /// <summary>
        /// This will "open" main page and close others
        /// </summary>
        public void OpenMainPage()
        {
            //Open Main Page
            if_main_page_open = true;

            //Close other pages
            if_product_page_open = false;
            if_all_product_page_open = false;
            if_chart_page_open = false;
            if_register_page_open = false;
            if_login_page_open = false;
            if_shipping_page_open = false;
            if_shipping_page_was_open = false;
            if_admin_page_open = false;
            if_manage_products_page_open = false;
            if_manage_users_page_open = false;
            if_edit_product_page_open = false;
            if_edit_user_page_open = false;

            head_class = "head_class_1";

            //Change head text
            head_text = "Clothee Online";
        }

        /// <summary>
        /// This will "open" single product page by its ID and close others
        /// </summary>
        public void OpenProductPage(int id)
        {
            if_out_of_stock = "";

            //Open Main Page
            if_product_page_open = true;

            //Close other pages
            if_main_page_open = false;
            if_all_product_page_open = false;
            if_chart_page_open = false;
            if_register_page_open = false;
            if_login_page_open = false;
            if_shipping_page_open = false;
            if_shipping_page_was_open = false;
            if_admin_page_open = false;



            head_text = products[id].Name;
            ChangeProductDetail(id);
        }

        /// <summary>
        /// This will "open" all products page and close others
        /// </summary>
        public void OpenAllProductsPage()
        {
            //Open Main Page
            if_all_product_page_open = true;


            //Close other pages
            if_main_page_open = false;
            if_product_page_open = false;
            if_all_product_page_open = true;
            if_chart_page_open = false;
            if_register_page_open = false;
            if_login_page_open = false;
            if_shipping_page_open = false;
            if_shipping_page_was_open = false;
            if_admin_page_open = false;

            head_class = "head_class_2";

            //Change head text
            head_text = "Products";
        }

        /// <summary>
        /// This will "open" chart page and close others
        /// </summary>
        public void OpenChartPage()
        {
            //Open Main Page
            if_chart_page_open = true;

            //Close other pages
            if_main_page_open = false;
            if_product_page_open = false;
            if_all_product_page_open = false;
            if_register_page_open = false;
            if_login_page_open = false;
            if_shipping_page_open = false;
            if_shipping_page_was_open = false;
            if_admin_page_open = false;

            head_text = "Your chart";
        }


        /// <summary>
        /// This will "open" register page and close others
        /// </summary>
        public void OpenRegisterPage()
        {
            //Open Main Page
            if_register_page_open = true;

            //Close other pages
            if_main_page_open = false;
            if_product_page_open = false;
            if_all_product_page_open = false;
            if_chart_page_open = false;
            if_login_page_open = false;
            if_shipping_page_open = false;
            if_shipping_page_was_open = false;
            if_admin_page_open = false;
        }

        /// <summary>
        /// This will "open" login page and close others
        /// </summary>
        public void OpenLoginPage()
        {
            //Open Main Page
            if_login_page_open = true;

            //Close other pages
            if_main_page_open = false;
            if_product_page_open = false;
            if_all_product_page_open = false;
            if_chart_page_open = false;
            if_register_page_open = false;
            if_shipping_page_open = false;
            if_shipping_page_was_open = false;
            if_admin_page_open = false;
        }

        /// <summary>
        /// This will "open" shipping page and close others
        /// </summary>
        public void OpenShippingPage()
        {
            if (chart.Price != 0)
            {

                //Close other pages
                if_main_page_open = false;
                if_product_page_open = false;
                if_all_product_page_open = false;
                if_chart_page_open = false;
                if_register_page_open = false;
                if_login_page_open = false;
                if_admin_page_open = false;

                if (If_Logged_In == false)
                {
                    if_login_page_open = true;
                    if_main_page_open = false;
                    if_product_page_open = false;
                    if_all_product_page_open = false;
                    if_chart_page_open = false;
                    if_register_page_open = false;
                    if_admin_page_open = false;
                    if_shipping_page_was_open = true;

                }
                else
                {
                    if_shipping_page_open = true;
                    if_main_page_open = false;
                    if_product_page_open = false;
                    if_all_product_page_open = false;
                    if_chart_page_open = false;
                    if_register_page_open = false;
                    if_login_page_open = false;
                    if_shipping_page_was_open = false;
                    if_admin_page_open = false;

                }
            }

        }

        /// <summary>
        /// This will "open" amin page and close others
        /// </summary>
        public void OpenAdminPage()
        {
            //Open Main Page
            if_admin_page_open = true;

            //Close other pages
            if_register_page_open = false;
            if_main_page_open = false;
            if_product_page_open = false;
            if_all_product_page_open = false;
            if_chart_page_open = false;
            if_login_page_open = false;
            if_shipping_page_open = false;
            if_shipping_page_was_open = false;
            if_manage_products_page_open = false;
            if_manage_users_page_open = false;
            if_edit_user_page_open = false;

        }


        /// <summary>
        /// This will "open" manage_products page and close others
        /// </summary>
        public void OpenManageProductsPage()
        {
            //Open Main Page
            if_manage_products_page_open = true;

            //Close other pages
            if_admin_page_open = false;
            if_manage_users_page_open = false;
            if_sales_page_open = false;
            if_edit_product_page_open = false;
            if_edit_user_page_open = false;

        }

        /// <summary>
        /// This will "open" manage_users page and close others
        /// </summary>
        public void OpenManageUsersPage()
        {
            //Open Main Page
            if_manage_users_page_open = true;

            //Close other pages
            if_admin_page_open = false;
            if_manage_products_page_open = false;
            if_sales_page_open = false;
            if_edit_product_page_open = false;
            if_edit_user_page_open = false;

        }

        /// <summary>
        /// This will "open" sales page and close others
        /// </summary>
        public void OpenSalesPage()
        {
            //Open Main Page
            if_sales_page_open = true;

            //Close other pages
            if_admin_page_open = false;
            if_manage_products_page_open = false;
            if_manage_users_page_open = false;
            if_edit_product_page_open = false;
            if_edit_user_page_open = false;

        }

        /// <summary>
        /// This will "open" edit product page
        /// </summary>
        public void OpenEditProductPage()
        {
            //Open Main Page
            if_edit_product_page_open = true;

            //Close other pages
            if_admin_page_open = false;
            if_manage_products_page_open = false;
            if_manage_users_page_open = false;
            if_sales_page_open = false;
            if_edit_user_page_open = false;
        }

        /// <summary>
        /// This will "open" edit user page
        /// </summary>
        public void OpenEditUserPage()
        {
            //Open Main Page
            if_edit_user_page_open = true;

            //Close other pages
            if_main_page_open = false;
            if_product_page_open = false;
            if_all_product_page_open = false;
            if_register_page_open = false;
            if_login_page_open = false;
            if_shipping_page_open = false;
            if_shipping_page_was_open = false;
            if_admin_page_open = false;
        }

        /// <summary>
        /// This function will change product_detail object for showing it on detail page
        /// </summary>
        /// <param name="id">This id will get product from products List</param>
        public void ChangeProductDetail(int id)
        {
            if (products.Count != 0 || products != null)
            {
                product_detail = products[id];
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        ///

        ///Chart

        //Chart variables
        protected Chart chart = new Chart();

        //Error for stock
        protected string if_out_of_stock = "";

        ///Chart functions

        /// <summary>
        /// This function will add item to chart List
        /// </summary>
        public void AddToChart(int id)
        {
            if_out_of_stock = "";
            //This is just for presentation when iplemented Id will be unique for each current customer
            chart.Id = 0;
            ChartItem chartItem = new ChartItem();

            if (products[id].AmmountOfProduct != 0)
            {
                chartItem.Id = chart.chartItems.Count;
                chartItem.product = products[id];
                products[id].AmmountOfProduct -= 1;

                chart.chartItems.Add(chartItem);

                double price = 0;

                for (int i = 0; i < chart.chartItems.Count; i++)
                {
                    price += chart.chartItems[i].product.Price;
                }

                chart.Price = price;
            }
            else
            {
                if_out_of_stock = "It is out of stock!";
            }

        }

        /// <summary>
        /// This function will remove item form the chart
        /// </summary>
        public void RemoveFromChart(int id)
        {
            //Update ammount of product
            products[chart.chartItems[id].product.Id].AmmountOfProduct += 1;

            chart.chartItems.RemoveAt(id);

            double price = 0;

            for (int i = 0; i < chart.chartItems.Count; i++)
            {
                chart.chartItems[i].Id = i;
                price += chart.chartItems[i].product.Price;
            }

            chart.Price = price;


        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////

        ///Register
        ///

        //Register variables
        //Create account variables for input
        //ca = create account
        protected string ca_user_name = "";
        protected string ca_name = "";
        protected string ca_lastname = "";
        protected string ca_street_address = "";
        protected string ca_city = "";
        protected string ca_country = "";
        protected string ca_postcode = "";
        protected string ca_email = "";
        protected string ca_phone_number = "";
        protected string ca_password = "";
        protected string ca_confirm_password = "";
        /// /////////////////
        //Create account error variables
        protected string ca_user_name_error = "";
        protected string ca_name_error = "";
        protected string ca_lastname_error = "";
        protected string ca_street_address_error = "";
        protected string ca_city_error = "";
        protected string ca_country_error = "";
        protected string ca_postcode_error = "";
        protected string ca_email_error = "";
        protected string ca_phone_number_error = "";
        protected string ca_password_error = "";
        protected string ca_confirm_password_error = "";



        //Register functions

        /// <summary>
        /// This function will create user account
        /// </summary>
        public void CreateAccount()
        {
            //User declaration
            User user = new User();
            //Reset Error variables
            ca_user_name_error = "";
            ca_name_error = "";
            ca_lastname_error = "";
            ca_email_error = "";
            ca_phone_number_error = "";
            ca_postcode_error = "";

            //If error would ocurre this bool will check
            bool if_error = false;


            ///Username validation/////////////////////////
            if (ca_user_name == "")
            {
                if_error = true;
                ca_user_name_error = "Please write username/At least 5 characters";
            }
            if (ca_user_name.Length < 5 && if_error == false)
            {
                if_error = true;
                ca_user_name_error = "User name needs to be at least 5 characters long";
            }
            for (int i = 0; i < userAccounts.Count; i++)
            {
                if (user.Name == ca_user_name && if_error == false)
                {
                    if_error = true;
                    ca_user_name_error = "This username alredy exists!";
                }
            }
            if (if_error == false)
            {
                user.UserName = ca_user_name;
            }
            /////////////////////////////////////////////////

            //Name validation////////////////////////////////
            if (ca_name == "")
            {
                if_error = true;
                ca_name_error = "Please write your name";
            }
            if (if_error == false)
            {
                user.Name = ca_name;
            }
            /////////////////////////////////////////////////

            //Lastname validation////////////////////////////
            if (ca_lastname == "")
            {
                if_error = true;
                ca_lastname_error = "Please write your lastname";
            }
            if (if_error == false)
            {
                user.LastName = ca_lastname;
            }
            ////////////////////////////////////////////////

            //Validation for email//////////////////////////
            bool is_valid_email = IsValidEmail(ca_email);

            if (ca_email == "")
            {
                ca_email_error = "Please enter valid email address";
            }

            if (is_valid_email == false && ca_email != "")
            {
                if_error = true;
                ca_email_error = "This is not valid email address!";
            }

            if (is_valid_email == true)
            {
                user.Email = ca_email;
            }

            ////////////////////////////////////////////////

            //Validation for phone number///////////////////
            bool is_valid_phone_number = IsPhoneNumber(ca_phone_number);

            if (ca_phone_number == "")
            {
                if_error = true;
                ca_phone_number_error = "Please enter valid phone number";
            }

            if (is_valid_phone_number == false && ca_phone_number != "")
            {
                if_error = true;
                ca_phone_number_error = "Phone number is not valid";
            }

            if (is_valid_phone_number == true)
            {
                user.PhoneNumber = ca_phone_number;
            }
            ////////////////////////////////////////////////

            //Validation for Postcode///////////////////////
            if (ca_postcode == "")
            {
                if_error = true;
                ca_postcode_error = "Please enter valid postcode";
            }

            if (ca_postcode.Length < 6)
            {
                if_error = true;
                ca_postcode_error = "Please enter valid postcode";
            }

            if (if_error == false)
            {
                user.PostCode = ca_postcode;
            }
            ////////////////////////////////////////////////

            //Validation for street address/////////////////
            if (ca_street_address == "")
            {
                if_error = true;
                ca_street_address_error = "Please enter valid street address";
            }

            if (if_error == false)
            {
                user.StreetAddress = ca_street_address;
            }
            ////////////////////////////////////////////////

            //Validation for password///////////////////////
            if (ca_password == "")
            {
                if_error = true;
                ca_password_error = "Please enter valid password";
            }

            if (ca_password.Length < 8 && ca_password != "")
            {
                if_error = true;
                ca_password_error = "Password needs to be at least 8 characters long!";
            }

            bool is_valid_password = IsValidPassword(ca_password);

            if (is_valid_password == false && ca_password.Length >= 8)
            {
                if_error = true;
                ca_password_error = "Please enter valid password!";
            }

            if (if_error == false)
            {
                user.Password = ca_password;
            }
            ////////////////////////////////////////////////

            //Validation for confirming the password

            if (ca_password != ca_confirm_password)
            {
                if_error = true;
                ca_confirm_password_error = "Passwords are not equal!";
            }
            ////////////////////////////////////////////////

            //Adding user to the user List
            //userAccounts.Count < 5 is just because there is no need for more just for the demonstration
            if (if_error == false && userAccounts.Count < 5)
            {
                user.Id = userAccounts.Count;
                userAccounts.Add(user);
                ca_user_name = "";
                ca_name = "";
                ca_lastname = "";
                ca_street_address = "";
                ca_city = "";
                ca_country = "";
                ca_postcode = "";
                ca_email = "";
                ca_phone_number = "";
                ca_password = "";
                ca_confirm_password = "";

                //"Redirect" to login
                if_login_page_open = true;
                if_register_page_open = false;
            }
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

        //////////////////////////////////////////////////////////////////
        ///

        ///Login Section

        //Variable to check if user is logged in

        ///Variable for logged in user
        protected User LoggedUser = new User();

        protected bool If_Logged_In = false;

        protected string login_user_name = "";
        protected string login_password = "";

        protected string error_login = "";

        /// <summary>
        /// This function log in user to account
        /// </summary>
        public void Login()
        {
            error_login = "";

            for (int i = 0; i < userAccounts.Count; i++)
            {
                if (userAccounts[i].UserName == login_user_name && userAccounts[i].Password == login_password)
                {
                    if (if_shipping_page_was_open == true)
                    {
                        if_login_page_open = false;
                        if_shipping_page_open = true;
                        LoggedUser = userAccounts[i];
                    }
                    else
                    {
                        If_Logged_In = true;
                        LoggedUser = userAccounts[i];
                        if_main_page_open = true;
                        if_login_page_open = false;
                        head_text = "Clothe Online";
                    }

                }
                else
                {
                    error_login = "Wrong username or password!";
                }
            }
        }

        //////////////////////////////////////////////////////////////////
        ///

        //Admin Panel

        //Manage Products

        protected Product edit_product = new Product();

        /// <summary>
        /// This function will remove product from the list by its Id
        /// </summary>
        /// /// <param name="Id">This variable will point on object which will be removed</param>
        public void RemoveProduct(int Id)
        {
            if (products.Count != 0)
            {
                products.RemoveAt(Id);

                for (int i = 0; i < products.Count; i++)
                {
                    products[i].Id = i;
                }

                if (products.Count > 3)
                {
                    for (int i = products.Count - 1; i > products.Count - 4; i--)
                    {
                        latestProducts.Add(products[i]);
                    }
                }
                else
                {

                    latestProducts.Clear();
                    for (int i = 0; i < products.Count; i++)
                    {
                        latestProducts.Add(products[i]);
                    }
                }

            }
        }

        /// <summary>
        /// This function will toggle edit page for certain product
        /// </summary>
        /// /// <param name="Id">This variable will target which product will be removed</param>
        public void EditProductToggle(int Id)
        {
            if (products.Count != 0)
            {
                OpenEditProductPage();
                edit_product = products[Id];
            }
        }

        /// <summary>
        /// This function will edit product by its id
        /// </summary>
        /// /// <param name="Id">This variable will target which product will be edited</param>
        public void EditProduct(int Id)
        {
            products[Id].Name = edit_product.Name;
            products[Id].AmmountOfProduct = edit_product.AmmountOfProduct;
            products[Id].Price = edit_product.Price;
            products[Id].Descritpion = edit_product.Descritpion;

            edit_product = new Product();

            OpenManageProductsPage();
        }

        //////////////////////////////////////////////////////////////////
        ///

        //Edit Account Page

        protected User edit_user = new User();

        /// <summary>
        /// This function will open edit user by its id
        /// </summary>
        /// /// <param name="Id">This variable will target which user will be edited</param>
        public void EditUserToggle(int Id)
        {
            if (userAccounts.Count != 0)
            {
                edit_user = userAccounts[Id];
                OpenEditUserPage();
            }
        }


        /// <summary>
        /// This function will edit user by its id
        /// </summary>
        /// /// <param name="Id">This variable will target which user will be edited</param>
        public void EditUser(int Id)
        {

            userAccounts[Id].Name = edit_user.Name;
            userAccounts[Id].LastName = edit_user.LastName;
            userAccounts[Id].City = edit_user.City;
            userAccounts[Id].PostCode = edit_user.PostCode;
            userAccounts[Id].StreetAddress = edit_user.StreetAddress;
            userAccounts[Id].Password = edit_user.Password;
            userAccounts[Id].PhoneNumber = edit_user.PhoneNumber;
            edit_user = new User();
            OpenMainPage();
        }

    }
}
