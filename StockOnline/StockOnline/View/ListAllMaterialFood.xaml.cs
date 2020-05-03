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
    public partial class ListAllMaterialFood : ContentPage
    {

        MaterialFirebaseHelper firebaseHelper = new MaterialFirebaseHelper();
        Models.MaterialHeader headerObject;
        public ListAllMaterialFood()
        {
            InitializeComponent();
            initAsync();
        }

        public async Task initAsync() {

            List<Models.MaterialHeader> listP = await firebaseHelper.GetAllMaterial();

            listAllMaterialForFood.ItemsSource = listP;
        }

        async void OnNoteAddedClicked(object sender, EventArgs e)
        {


            await Navigation.PushAsync(new MaterialFoodAdmin());


        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Xamarin.Forms.ListView listView1 = (Xamarin.Forms.ListView)sender;
            Models.MaterialHeader material_select = (Models.MaterialHeader)listView1.SelectedItem; 
            

            await Navigation.PushAsync(new MaterialFoodAdmin(material_select.ID));

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