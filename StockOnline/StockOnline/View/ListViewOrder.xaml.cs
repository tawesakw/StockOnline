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


using StockOnline.DependencyServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using StockOnline.Data;
using StockOnline.ViewModels;


namespace StockOnline.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewOrder : ContentPage
    {

        private readonly IBlueToothService _blueToothService;
        public static string message = "Hello Print"; 
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        string date_str = DateTime.Now.ToString("yyyy'-'MM'-'dd", CultureInfo.InvariantCulture).Replace("-", "");

        private string _selectedDevice;

        private IList<string> _deviceList;

        public ListViewOrder()
        {
            InitializeComponent();
            init();

            _blueToothService = DependencyService.Get<IBlueToothService>();
            BindDeviceList();

        }

        public async void init()
        {
            string status = "P";
            // listView.ItemsSource = await App.Database.GetNotesAsync();           
            
          //  List<FoodMini> listFood2 = await firebaseHelper.GetAllFoodByStatus(date_str, status);
            
            
            listView.ItemsSource = await firebaseHelper.GetAllFoodByStatus(date_str, status);

        }

        void BindDeviceList()
        {
            if (_blueToothService != null)
            {
                var list = _blueToothService.GetDeviceList();
                if (list != null)
                {
                    DeviceList.Clear();
                    foreach (var item in list)
                    {
                        DeviceList.Add(item);
                    }
                }
            }
        }


        public IList<string> DeviceList
        {
            get
            {
                if (_deviceList == null)
                    _deviceList = new ObservableCollection<string>();
                return _deviceList;
            }
            set
            {
                _deviceList = value;
            }
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {

                var action = DisplayAlert("Edit Order ", "ต้องการชำระเงินรายการนี้", "Yes", "No");
                if (await action)
                {

                    ListView listView = (ListView)sender;
                    FoodMini mini = (FoodMini)listView.SelectedItem;

                    
                    List<FoodMini> listFood1 = await firebaseHelper.GetAllFood(date_str, mini.QuequNo, "P");

                    decimal totalAmount = 0;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("--------------r\n");
                    for (int i = 0; i < listFood1.Count; i++)
                    {
                        FoodMini mini2 = listFood1[i];
                        mini2.Status = "Y";
                        await firebaseHelper.UpdateFood(mini2.ID, mini2.Name, mini2.NameEn, mini2.Location, mini2.Details, mini2.Quantity, mini2.OrderDate, mini2.QuequNo, mini2.Status, date_str, mini2.LevelSpicy);

                        sb.Append(" Order No = " + (i + 1) + "r\n");
                        sb.Append(" Food  = " + mini2.Name + "r\n");
                        sb.Append(" จำนวน  = " + mini2.Quantity + "r\n");
                        sb.Append(" ราคา  = " + mini2.CostPerUnit + "r\n");
     
                        totalAmount = totalAmount + (mini2.CostPerUnit * mini2.Quantity);

                    }


                    sb.Append("-------------");
                    sb.Append("รวมราคา  " + totalAmount);
                    sb.Append("\r\n ");
                    sb.Append("\r\n ");
                    sb.Append("-------------");
                    sb.Append("Order By  " + Application.Current.Properties["UserLogin"] + "");
                    sb.Append("\r\n ");
                    sb.Append("\r\n ");
                    sb.Append("\r\n ");

                    totalAmount = 0;
                    decimal totalQuan = 0;

                    for (int i = 0; i < listFood1.Count; i++)
                    {
                        FoodMini mini2 = listFood1[i];
                        sb.Append(" Order No = " + (i + 1) + "r\n");
                        sb.Append(" Food  = " + mini2.Name + "r\n");
                        sb.Append(" จำนวน  = " + mini2.Quantity + "r\n");
                        sb.Append(" ราคา  = " + mini2.CostPerUnit + "r\n");
                        totalAmount = totalAmount + (mini2.CostPerUnit * mini2.Quantity);
                        totalQuan = totalQuan + mini2.Quantity;
                    }
                    sb.Append("-------------");
                    sb.Append("รวมราคา  " + totalAmount);
                    sb.Append("\r\n ");
                    sb.Append("\r\n ");
                    sb.Append("-------------");
                    sb.Append("Order By  " + Application.Current.Properties["UserLogin"] + "");
                    sb.Append("\r\n ");
                    sb.Append("\r\n ");
                    sb.Append("\r\n ");

                    message = sb.ToString();
                    await _blueToothService.Print(SelectedDevice, message);

                }//end if

            }//end if
        }

        public string SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }
            set
            {
                _selectedDevice = value;
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


    }

    }