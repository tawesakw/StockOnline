using StockOnline.DependencyServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOnline.Data;
using StockOnline.Helper;
using StockOnline.Models;
using StockOnline.ViewModels;




namespace StockOnline.Models
{
    public class PrintPageViewModel
    {
        private readonly IBlueToothService _blueToothService;

        public static string message = "Hello Print";

        public static int queqeNo= 0;
        string date_str = DateTime.Now.ToString("yyyy'-'MM'-'dd", CultureInfo.InvariantCulture).Replace("-", "");


        FirebaseHelper firebaseHelper = new FirebaseHelper();

        private IList<string> _deviceList;
        public IList<string> DeviceList
        {
            get
            {
                if (StockOnline.Helper.Settings.GeneralSettings != null && StockOnline.Helper.Settings.GeneralSettings.ToString().Length > 0)
                {
                    _deviceList = new List<string>(); 
                    _deviceList.Add(StockOnline.Helper.Settings.GeneralSettings.ToString());

                } else { 
                    if (_deviceList == null)
                        _deviceList = new ObservableCollection<string>();
                }
                return _deviceList;
            }
            set
            {
                _deviceList = value;

                if (value != null)
                    StockOnline.Helper.Settings.GeneralSettings = value.ToString();

            }
        }

        private string _printMessage;
        public string PrintMessage
        {
            get
            {
                return _printMessage;
            }
            set
            {
                _printMessage = value;
            }
        }

        private string _selectedDevice;
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

        /// <summary>
        /// Print text-message
        /// </summary>
        public ICommand PrintCommand => new Command(async () =>
        {
            if (SelectedDevice == null || SelectedDevice.Trim().Length == 0)
            {
                //   DisplayAlert("Please Select Device", "Please Select Device" , "Ok");
            }else {
                //Update Status QueqeNO

                //queqeNo

                List<FoodMini> list = await firebaseHelper.GetAllFood(date_str, queqeNo);
                for (int i = 0; i < list.Count; i++)
                {
                    FoodMini mini = list[i];
                    mini.Status = "Y";

                    await firebaseHelper.UpdateFood(mini.ID, mini.Name, mini.NameEn, mini.Location, mini.Details, mini.Quantity, mini.OrderDate, mini.QuequNo, mini.Status, date_str, mini.LevelSpicy);
                }

                //PrintMessage += " Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
                //await _blueToothService.Print(SelectedDevice, PrintMessage);

                PrintMessage += message;
                await _blueToothService.Print(SelectedDevice, PrintMessage);
            }

        });

        public PrintPageViewModel()
        {
            _blueToothService = DependencyService.Get<IBlueToothService>();
            BindDeviceList();
        }

        /// <summary>
        /// Get Bluetooth device list with DependencyService
        /// </summary>
        void BindDeviceList()
        {
            if (_blueToothService != null) { 
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
    }
}