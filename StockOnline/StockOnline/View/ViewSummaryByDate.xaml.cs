using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Globalization;
using StockOnline.Common;
using StockOnline.Helper;
using StockOnline.Models;


namespace StockOnline.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewSummaryByDate : ContentPage
    {



        FirebaseHelper firebaseHelper = new FirebaseHelper();
        private List<Microcharts.ChartEntry> _entries = new List<Microcharts.ChartEntry>();


        public ViewSummaryByDate()
        {
            InitializeComponent();
            initAsync();

            IsBusy = false;
        }


        async void Logout_Click(object sender, EventArgs e)
        {
            var action = DisplayAlert("Exit App", "ต้องการ ออกจากระบบ ", "Yes", "No");
            if (await action)
            {

                Application.Current.Properties["UserLogin"] = null;
                Application.Current.SavePropertiesAsync();

                var closer = DependencyService.Get<IExitAppInterface>();
                closer.closeApplication();

                //  await Navigation.PushAsync(new ChooseType());
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

        public async Task initAsync()
        {
            /*
            List<FoodMini> listFood = await firebaseHelper.GetAllFood();
            int sizeList = 0;
            if (listFood != null) {
                sizeList = listFood.Count;
            }
            */
            IsBusy = true;

            List<SKColor> listColor = new List<SKColor>();
            listColor.Add(SKColor.Parse("#FF0033"));
            listColor.Add(SKColor.Parse("#FF8000"));
            listColor.Add(SKColor.Parse("#FFE600"));
            listColor.Add(SKColor.Parse("#1AB34D"));
            listColor.Add(SKColor.Parse("#801AB3"));
            listColor.Add(SKColor.Parse("#1A66FF"));
            listColor.Add(SKColor.Parse("#FF0033"));
            listColor.Add(SKColor.Parse("#FF8000"));
            listColor.Add(SKColor.Parse("#FFE600"));
            listColor.Add(SKColor.Parse("#1AB34D"));
            listColor.Add(SKColor.Parse("#801AB3"));
            listColor.Add(SKColor.Parse("#1A66FF"));
            listColor.Add(SKColor.Parse("#FF0033"));
            listColor.Add(SKColor.Parse("#FF8000"));
            listColor.Add(SKColor.Parse("#FFE600"));
            listColor.Add(SKColor.Parse("#1AB34D"));
            listColor.Add(SKColor.Parse("#801AB3"));
            listColor.Add(SKColor.Parse("#1A66FF"));


            string date_str = DateTime.Now.ToString("yyyy'-'MM'-'dd", CultureInfo.InvariantCulture).Replace("-", "");
            List<FoodMini> listFoodBack = new List<FoodMini>();

            List<ReportModel> tableList = new List<ReportModel>();



            _entries = new List<Microcharts.ChartEntry>();
            for (int i = 0; i < 8; i++) {
                List<FoodMini> listFood1 = await firebaseHelper.GetAllFoodByStatus(date_str, "Y");
                int count_sale = 0;
                if (listFood1 == null)
                {
                    count_sale = 0;
                }
                else {
                    count_sale = listFood1.Count();
                }

                Utils utils = new Utils();
                ReportModel models1 = new ReportModel();
                models1.DateValue = "วันที่:" + date_str;
                models1.Quantity = "จำนวน:" + count_sale;
                tableList.Add(models1);
                Microcharts.ChartEntry entry1 = new Microcharts.ChartEntry(count_sale)
                {
                    Label = utils.convertYYYYMMDD(date_str),
                    ValueLabel = utils.convertYYYYMMDD(date_str),
                    Color = listColor[i],
                };
                _entries.Add(entry1);


               // listFoodBack.AddRange(listFood1);
                date_str = DateTime.Now.AddDays(-i).ToString("yyyy'-'MM'-'dd", CultureInfo.InvariantCulture).Replace("-", "");

            }


            listSale.ItemsSource = tableList;

            /*
            List<FoodMini> listFoodBack = new List<FoodMini>();
            for (int i = sizeList - 1; i >= 0; i--) {
                listFoodBack.Add(listFood[i]);


            }
            */

            /*                     
            GFD gg = new GFD();
            
            listFood.Sort(gg);
            _entries = new List<Microcharts.ChartEntry>();
            
            for (int i = 0; i < listFood.Count; i++)
            {
                if (i >= 8) {
                    break;
                } 
                FoodMini mini = listFood[i];

                Microcharts.ChartEntry entry1 = new Microcharts.ChartEntry(mini.Quantity)
                {
                    Label = mini.Name,
                    ValueLabel = mini.OrderDate + "",
                    Color = listColor[i],
                };
                _entries.Add(entry1);

            }
            */
            /*
            for (int i = 0; i < listFoodBack.Count; i++)
            {
                if (i >= 8)
                {
                    break;
                }
                FoodMini mini = listFoodBack[i];

                Microcharts.ChartEntry entry1 = new Microcharts.ChartEntry(mini.Quantity)
                {
                    Label = mini.Name,
                    ValueLabel = mini.OrderDate + "",
                    Color = listColor[i],
                };
                _entries.Add(entry1);

            }
            */
            MyBarChart.Chart = new BarChart { Entries = _entries };

            IsBusy = false;

        }
    }
}