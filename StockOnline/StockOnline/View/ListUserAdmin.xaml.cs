using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOnline.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StockOnline.Models;
using StockOnline.Common;

namespace StockOnline.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListUserAdmin : ContentPage
    {
        UserFirebaseHelper firebaseHelper = new UserFirebaseHelper();
        public ListUserAdmin()
        {
            InitializeComponent();
            initAsync();
        }

        public async Task initAsync()
        {

            List<Models.User> listP = await firebaseHelper.GetAllUser();

            listView.ItemsSource = listP;
        }

        async void OnNoteAddedClicked(object sender, EventArgs e)
        {


            await Navigation.PushAsync(new UserAdmin());


        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Xamarin.Forms.ListView listView1 = (Xamarin.Forms.ListView)sender;
            Models.User user_select = (Models.User)listView1.SelectedItem;


            await Navigation.PushAsync(new UserAdmin(user_select.ID));

        }

        async void exitClick(object sender, EventArgs e)
        {
            var action = DisplayAlert("Exit App", "ต้องการ ออกจากระบบ ", "Yes", "No");
            if (await action)
            {
                Application.Current.Properties["UserLogin"] = null;
                Application.Current.SavePropertiesAsync();

                var closer = DependencyService.Get<IExitAppInterface>();
                closer.closeApplication();
            }


        }


        async void Login_Click(object sender, EventArgs e)
        {
            var action = DisplayAlert("Exit App", "ต้องการ Login ใหม่ ", "Yes", "No");
            if (await action)
            {
                Application.Current.Properties["UserLogin"] = null;
                Application.Current.SavePropertiesAsync();

                await Navigation.PushAsync(new LoginPage());
            }
        }
    }
}