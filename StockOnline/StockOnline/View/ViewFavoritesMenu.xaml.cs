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
    public partial class ViewFavoritesMenu : ContentPage
    {



        FirebaseHelper firebaseHelper = new FirebaseHelper();
        private List<Microcharts.ChartEntry> _entries = new List<Microcharts.ChartEntry>();
        /*

        private readonly List<Microcharts.ChartEntry> _entries = new List<Microcharts.ChartEntry>()
        {
            new Microcharts.ChartEntry(200)
            {
                Label = "January",
                ValueLabel = "200",
                Color = SKColor.Parse("#FF0033"),
            },
            new Microcharts.ChartEntry(400)
            {
                Label = "February",
                ValueLabel = "400",
                Color = SKColor.Parse("#FF8000"),
            },
            new Microcharts.ChartEntry(300)
            {
                Label = "March",
                ValueLabel = "300",
                Color = SKColor.Parse("#FFE600"),
            },
            new Microcharts.ChartEntry(250)
            {
                Label = "April",
                ValueLabel = "250",
                Color = SKColor.Parse("#1AB34D"),
            },
            new Microcharts.ChartEntry(650)
            {
                Label = "May",
                ValueLabel = "650",
                Color = SKColor.Parse("#1A66FF"),
            },
            new Microcharts.ChartEntry(500)
            {
                Label = "June",
                ValueLabel = "500",
                Color = SKColor.Parse("#801AB3"),
            },
        };
        */

        public ViewFavoritesMenu()
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


        public async Task initAsync() {
            try
            {

                IsBusy = true;
                List<FoodMini> listFood = new List<FoodMini>();
                string date_str = DateTime.Now.ToString("yyyy'-'MM'-'dd", CultureInfo.InvariantCulture).Replace("-", "");
                for (int i = 1; i <= 30; i++)
                {
                    
                    listFood.AddRange(await firebaseHelper.GetAllFoodByStatus(date_str, "Y"));
                    int d = i * -1;
                    date_str = DateTime.Now.AddDays(d).ToString("yyyy'-'MM'-'dd", CultureInfo.InvariantCulture).Replace("-", "");

                }


                //    List<FoodMini> listFood = await firebaseHelper.GetAllFood();
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


                /*
                GFG gg = new GFG();
                listFood.Sort(gg);
                */

                List<FavoritesFoodModel> listFavorites = new List<FavoritesFoodModel>();
                listFavorites = getFavoritesFood(listFood);
                _entries = new List<Microcharts.ChartEntry>();
                for (int i = 0; i < listFavorites.Count; i++)
                {
                    FavoritesFoodModel mini = listFavorites[i];

                    Microcharts.ChartEntry entry1 = new Microcharts.ChartEntry(mini.Quantity)
                    {
                        Label = mini.NameEn,
                        ValueLabel = mini.Quantity + "",
                        Color = listColor[i],
                    };
                    _entries.Add(entry1);

                }

                MyBarChart.Chart = new BarChart { Entries = _entries };

                IsBusy = false;
            }
            catch (Exception exx) {
                string error = exx.Message;
                IsBusy = false;
            }

        }

        private List<FavoritesFoodModel> getFavoritesFood(List<FoodMini> listFood)
        {
            List<FavoritesFoodModel> listF = new List<FavoritesFoodModel>();
            bool found = false;
            for (int i = 0; i < listFood.Count; i++) {

                FoodMini mini1 = listFood[i];

                if (i == 0)
                {
                    
                    FavoritesFoodModel model = new FavoritesFoodModel();
                    model.NameEn = mini1.NameEn;
                    model.Name = mini1.Name;
                    model.Quantity = mini1.Quantity;
                    listF.Add(model);

                }
                else {
                 
                    for (int k = 0; k < listF.Count; k++) {
                        FavoritesFoodModel model2 = listF[k];
                        if (model2.NameEn == mini1.NameEn)
                        {
                            model2.Quantity = model2.Quantity + mini1.Quantity;
                            listF.RemoveAt(k);
                            listF.Add(model2);
                            found = true;
                            break;
                        }
                        /*
                        else {
                            FavoritesFoodModel model = new FavoritesFoodModel();
                            model.NameEn = mini1.NameEn;
                            model.Name = mini1.Name;
                            model.Quantity = mini1.Quantity;
                            listF.Add(model);

                        }
                        */
                        
                    }//end for
                    if (!found) {

                        FavoritesFoodModel model = new FavoritesFoodModel();
                        model.NameEn = mini1.NameEn;
                        model.Name = mini1.Name;
                        model.Quantity = mini1.Quantity;
                        listF.Add(model);

                    }


                }//end else
            }//end for

            return listF;

        }
    }
}