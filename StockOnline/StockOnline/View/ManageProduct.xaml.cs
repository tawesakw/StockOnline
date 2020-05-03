using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOnline.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StockOnline.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageProduct : ContentPage
    {
        public ManageProduct()
        {
            InitializeComponent();
        }


        void viewProductClick(Object o, EventArgs e)
        {

            Navigation.PushAsync(new ListProductAdmin());

        }


        void viewUserClick(Object o, EventArgs e)
        {

            Navigation.PushAsync(new ListUserAdmin());


        }


        void viewMaterialClick(Object o, EventArgs e)
        {

            Navigation.PushAsync(new ListMaterialAdmin());


        }
                     
        void viewMaterialFoodClick(Object o, EventArgs e)
        {

            Navigation.PushAsync(new ListAllMaterialFood());
        }


        async void Login_Click(object sender, EventArgs e)
        {
            var action = DisplayAlert("Exit App", "ต้องการ Login ใหม่ ", "Yes", "No");
            if (await action)
            {

                await Navigation.PushAsync(new LoginPage());
            }
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


    }
}