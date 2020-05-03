using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StockOnline.Models;
using System.Globalization;
using StockOnline.Common;
using StockOnline.Helper;


namespace StockOnline.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        UserFirebaseHelper userFirebaseHelper = new UserFirebaseHelper();

        public LoginPage()
        {
            InitializeComponent();
            init();

            IsBusy = false;
        }


        void init() {
            BackgroundColor = Constants.BackgroundColor;
            Lbl_Username.TextColor = Constants.MainTextColor;
            Lbl_Password.TextColor = Constants.MainTextColor;
            ActivitySpinner.IsVisible = false;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;

            Entry_Username.Completed += (s, e) => Entry_Password.Focus();
            Entry_Password.Completed += (s, e) => SigninProcedure(s, e);

            //new NavigationPage(new MenuIntentXaml());
       ///     new NavigationPage(new MenuIntentXaml());

        }
        async void SigninProcedure(Object sender, EventArgs e) {

            IsBusy = true;


            Users user = new Users(Entry_Username.Text, Entry_Password.Text);

            User userR = await userFirebaseHelper.GetUserPassword(Entry_Username.Text);
            
            if (userR == null) {

                IsBusy = false;
                await DisplayAlert("Login Fail", "Login Fail User=" + user.Username.Trim() + " Not found this User ", "Ok");
            } else {
                if (userR.Password != Entry_Password.Text)
                {
                    IsBusy = false;
                    await DisplayAlert("Login Fail", "Login Fail User=" + user.Username.Trim() + " Password is not correct", "Ok");
                }
                else
                {
                    try{
                        if (userR.Level != null && userR.Level.Trim().Equals("Order"))
                        {
                            Application.Current.Properties["UserLogin"] = Entry_Username.Text.Trim();
                            Application.Current.SavePropertiesAsync();
                            IsBusy = false;
                            await Navigation.PushAsync(new ChooseType());
                        }
                        else if (userR.Level != null && userR.Level.Trim().Equals("Manager"))
                        {
                            Application.Current.Properties["UserLogin"] = Entry_Username.Text.Trim();
                            Application.Current.SavePropertiesAsync();

                            IsBusy = false;
                            // Navigation.PushAsync(new ViewDailyOrder());
                            await Navigation.PushAsync(new ListReport());
                        }
                        else if (userR.Level != null && userR.Level.Trim().Equals("Admin"))
                        {
                            Application.Current.Properties["UserLogin"] = Entry_Username.Text.Trim();
                            Application.Current.SavePropertiesAsync();

                            IsBusy = false;
                            await Navigation.PushAsync(new ManageProduct());
                        }

                        else if (userR.Level != null && userR.Level.Trim().Equals("ViewOrder"))
                        {
                            Application.Current.Properties["UserLogin"] = Entry_Username.Text.Trim();
                            Application.Current.SavePropertiesAsync();

                            IsBusy = false;
                            // Navigation.PushAsync(new ViewDailyOrder());
                            await Navigation.PushAsync(new ListViewAllOrder());
                        }

                        else
                        {
                            IsBusy = false;
                            await DisplayAlert("Login Fail", "Login Fail User=" + user.Username.Trim(), "Ok");
                        }
                    }
                    catch (Exception ee)
                    {
                        IsBusy = false;
                        await DisplayAlert("Login", "Login fail Exception" + ee.Message, "Ok");
                    }
                }
            }
        }
    }
}