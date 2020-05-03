using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOnline.Common;
using StockOnline.Helper;
using StockOnline.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StockOnline.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewDailyOrder : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        string date_str = DateTime.Now.ToString("yyyy'-'MM'-'dd", CultureInfo.InvariantCulture).Replace("-", "");

        public ViewDailyOrder()
        {
            InitializeComponent();
            initAsync();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();    
        }

        public async Task initAsync() {
            try
            {
                await DisplayAlert("", "แสดงรายการ วันที่" + date_str, "OK");

                //  summaryTxt.Text = "รายการขายประจำวันที่:" + DateTime.Now.ToString("yyyy'-'MM'-'dd", CultureInfo.InvariantCulture);
                List<FoodMini> listFood = await firebaseHelper.GetAllFood(date_str);
                await DisplayAlert("", "แสดงรายการ วันที่ 22 " + date_str, "OK");
                int totalQuan = 0;
                decimal amount = 0;
                int totalQ = 0;

                await Task.Delay(100);
                if (listFood != null)
                {
                    listView.ItemsSource = listFood;                   
                    for (int i = 0; i < listFood.Count; i++)
                    {
                        FoodMini mini = listFood[i];
                        amount = amount + mini.CostPerUnit;
                        totalQuan = totalQuan + mini.Quantity;
                        totalQ = totalQ + 1;

                    }
                }
            }
            catch (Exception ee) { 
                
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


        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
          

          
        }

    }
}