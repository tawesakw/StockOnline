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
    public partial class ListViewAllOrder: ContentPage
    {

        private readonly IBlueToothService _blueToothService;
        public static string message = "Hello Print"; 
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        string date_str = DateTime.Now.ToString("yyyy'-'MM'-'dd", CultureInfo.InvariantCulture).Replace("-", "");

        private string _selectedDevice;

        private IList<string> _deviceList;

        public ListViewAllOrder()
        {
            InitializeComponent();
            init();

            _blueToothService = DependencyService.Get<IBlueToothService>();
            BindDeviceList();

        }

        public async void init()
        {
       

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

        async void selectViewSelected(object sender, EventArgs e){
            String selectedValue = (String)picker.SelectedItem;
            if (selectedValue != null && selectedValue.Equals("Queue No"))
            {

                string status1 = "Y";
                string status2 = "D";

                listViewByQueue.ItemsSource = await firebaseHelper.GetAllFoodByNotStatusStr(date_str, status1, status2);
                listViewByQueue.IsVisible = true;
                listViewByFood.IsVisible = false;

            }
            else if (selectedValue != null && selectedValue.Equals("Food"))
            {
                string status1 = "Y";
                string status2 = "D";
                listViewByFood.ItemsSource = await firebaseHelper.GetAllSumFoodByNotStatusStr(date_str, status1, status2);
                listViewByQueue.IsVisible = false;
                listViewByFood.IsVisible = true;
            }

        }//end function
        
        
        
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