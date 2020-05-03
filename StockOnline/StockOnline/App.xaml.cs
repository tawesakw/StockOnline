using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StockOnline.Data;
using StockOnline.View;
using System.IO;
using StockOnline.Models;
using System.Threading;
using StockOnline.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using StockOnline.Helper;

namespace StockOnline
{
    public partial class App : Application
    {
        static TokenDatabaseController tokenDatabase;
        static UserDatabaseController userDatabase;
        static RestService restService;
        private static Label labelScreen;
        private static Timer timer;

        ProductFirebaseHelper firebaseHelper = new ProductFirebaseHelper();
        UserFirebaseHelper userFirebaseHelper = new UserFirebaseHelper();
        public App()
        {
            InitializeComponent();
            //   MainPage = new Product();

            //  MainPage = new ListViewPage1();

            //MainPage = new MenuIntentXaml();


            // get the connection
           var db = DependencyService.Get<ISLite>();
            var conn = db.GetConnection();

            // create the tables
            if (conn != null)
            {
                try
                {
                    conn.CreateTable<Models.Users>();
                    conn.CreateTable<Models.Order>();
                    //co .CreateDatabaseAndTables();
                }
                catch (Exception ee) {
                    string message = ee.Message; 
                }
            }

            initialDataAsync();


            MainPage = new NavigationPage(new LoginPage());

            //MainPage = new ToolbarItemXaml();
        }

        private async Task initialDataAsync()
        {

            List<User> listUser = await userFirebaseHelper.GetAllUser();

            if (listUser.Count == 0) {
                await userFirebaseHelper.AddUser("Order", "1", "", "Order");
                await userFirebaseHelper.AddUser("Manager", "1", "", "Manager");
                await userFirebaseHelper.AddUser("Admin", "1", "", "Admin");
            }
        }

        static FoodDatabase database;
        private static bool hasInternet;
        private static Page currentPage;
        private static bool noInterShow;

        public static FoodDatabase Database
        {
            get
            {
            if (database == null)
            {
                database = new FoodDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Food3.db3"));
            }
            return database;
              }
         }



        public static void StartCheckInternet(Label label, Page page) {
            labelScreen = label;
            label.Text = Constants.NoInternetText;
            label.IsVisible = false;
            hasInternet = true;
            currentPage = page;
            if (timer == null) { 
                timer = new Timer((e) => 
                {
                    CheckIfInternetOvertime();            
                }, null, 10,(int)TimeSpan.FromSeconds(3).TotalMilliseconds);

            }

         }

    private static void CheckIfInternetOvertime()
    {
    var networkConnection = DependencyService.Get<INetworkConnection>();
    networkConnection.CheckNetworkConnection();
    if (!networkConnection.IsConnected) {

        Device.BeginInvokeOnMainThread(async () =>
        {
            if (hasInternet) {
                if (!noInterShow) {
                    hasInternet = false;
                    labelScreen.IsVisible = true;
                    await ShowDisplayAlert();
                }

            }

        });


    }
    }

    private static async Task ShowDisplayAlert()
    {
    noInterShow = false;
    await currentPage.DisplayAlert("Internet", "Device has no internet, Please reconnect ", "OK");
    noInterShow = false;
    }

    protected override void OnStart()
    {
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }

    public static TokenDatabaseController TokenDatabase
    {
    get {
        if (tokenDatabase == null) {
            tokenDatabase = new TokenDatabaseController();
        }
        return tokenDatabase;

    }


    }



    public static UserDatabaseController UserDatabase
    {
    get
    {
        if (userDatabase == null)
        {
            userDatabase = new UserDatabaseController();
        }
        return userDatabase;

    }


    }

    public static RestService RestService {
    get
    {
        if (restService == null)
        {
            restService = new RestService();
        }
        return restService;

    }

    }



    }
}
