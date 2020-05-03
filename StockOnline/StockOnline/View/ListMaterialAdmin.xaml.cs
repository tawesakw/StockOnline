using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOnline.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StockOnline.Models;

using System.Globalization;
using StockOnline.Common;


namespace StockOnline.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListMaterialAdmin : ContentPage
    {

        MaterialFirebaseHelper matFirebaseHelper = new MaterialFirebaseHelper();
        MaterialSourceFirebaseHelper matSourceFirebaseHelper = new MaterialSourceFirebaseHelper();
        // ProductFirebaseHelper productfirebaseHelper = new ProductFirebaseHelper();
        public ListMaterialAdmin()
        {
            InitializeComponent();
            initAsync();
        }

        public async Task initAsync() {

            List<Models.MaterialSource> listP = await matSourceFirebaseHelper.GetAllMaterialSource();

            listView.ItemsSource = listP;
            /*

            List<Models.Material> listFood = await firebaseHelper.GetAllMaterial();

            listView.ItemsSource = listP;
            */
        }

        async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MaterialAdmin());
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Xamarin.Forms.ListView listView1 = (Xamarin.Forms.ListView)sender;
            Models.MaterialSource material_select = (Models.MaterialSource)listView1.SelectedItem; 
            

            await Navigation.PushAsync(new MaterialAdmin(material_select.ID));

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

                await Navigation.PushAsync(new LoginPage());
            }
        }


    }
}