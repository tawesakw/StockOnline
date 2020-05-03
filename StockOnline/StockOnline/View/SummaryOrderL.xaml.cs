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
    public partial class SummaryOrderL : ContentPage
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public int queueNo { get; set; }

      //  string date_str = DateTime.Now.ToString("dd/MM/yyyy").Replace("/", "");
        string date_str = DateTime.Now.ToString("yyyy'-'MM'-'dd", CultureInfo.InvariantCulture).Replace("-", "");

        public SummaryOrderL()
        {
            InitializeComponent();
        }

    
        public SummaryOrderL(int queqeNo)
        {
            this.queueNo = queqeNo;
           
            InitializeComponent();
             QueqeNoStr.Text = "Queue No:" + this.queueNo + "";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await firebaseHelper.GetAllFood(date_str, this.queueNo);
            List<FoodMini> list = await firebaseHelper.GetAllFood(date_str, this.queueNo);
            string error = "";

            //PrintPageViewModel.message = "";

            StringBuilder sb = new StringBuilder();
            sb.Append("--------------r\n");

            decimal totalAmount = 0;
            for (int i = 0; i < list.Count; i++)
            {
                FoodMini mini = list[i];
                sb.Append(" Order No = " + (i + 1) + "r\n");
                sb.Append(" Food  = " + mini.Name + "r\n");
                sb.Append(" จำนวน  = " + mini.Quantity + "r\n");
                sb.Append(" ราคา  = " + mini.CostPerUnit + "r\n");
                totalAmount = totalAmount + (mini.CostPerUnit * mini.Quantity);
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

            for (int i = 0; i < list.Count; i++)
            {
                FoodMini mini = list[i];
                sb.Append(" Order No = " + (i + 1) + "r\n");
                sb.Append(" Food  = " + mini.Name + "r\n");
                sb.Append(" จำนวน  = " + mini.Quantity + "r\n");
                sb.Append(" ราคา  = " + mini.CostPerUnit + "r\n");
                totalAmount = totalAmount + (mini.CostPerUnit * mini.Quantity);
                totalQuan = totalQuan + mini.Quantity;
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


            SummaryView.Text = "Total ชาม:" + totalQuan + "  Amount:" + totalAmount;


            PrintPageViewModel.message = sb.ToString();


        }

        async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrderEntryPageLine(this.queueNo));

        }



        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {

                var action = DisplayAlert("Edit Order ", "ต้องการแก้ไขรายการนี้", "Yes", "No");
                if (await action)  {

                    ListView listView = (ListView)sender;
                    FoodMini mini = (FoodMini)listView.SelectedItem;
                    await Navigation.PushAsync(new OrderEntryPageLine(this.queueNo, mini.ID));
                }


                 
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



        async void CancelOrder_Click(object sender, EventArgs e)
        {
            var action = DisplayAlert("Cancel", "ยกเลิก Order ", "Yes", "No");
            if (await action) {

                List<FoodMini> list = await firebaseHelper.GetAllFood(date_str, this.queueNo);
                for (int i = 0; i < list.Count; i++)
                {
                    FoodMini mini = list[i];
                    await firebaseHelper.DeleteFood(mini.ID, date_str);
                }


                await Navigation.PushAsync(new ChooseType());
            }
        }




    }
}