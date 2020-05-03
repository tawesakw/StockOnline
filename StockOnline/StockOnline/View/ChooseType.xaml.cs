using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOnline.Common;
using StockOnline.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StockOnline.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseType : ContentPage
    {

       // DateTime now = DateTime.Now;
        string date_str = DateTime.Now.ToString("yyyy'-'MM'-'dd", CultureInfo.InvariantCulture).Replace("-","");

      /// string date_str = DateTime.Now.ToString("dd/MM/yyyy").Replace("/", "");
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ChooseType()
        {
            InitializeComponent();
        }
        async void Lineman_Click(Object sender, EventArgs e)
        {
            //Gen new Queue today

            List<Models.FoodMini> listF = await firebaseHelper.GetAllFood(date_str);
            int queueNo = 0;
            if (listF != null)
            {
                queueNo = listF.Count();
                queueNo = queueNo + 1;
            }
            else {
                queueNo = 1;
            }
            await Navigation.PushAsync(new SummaryOrderL(queueNo));

        }

        async void Waiting_Click(Object sender, EventArgs e)
        {

            await Navigation.PushAsync(new ListWaitingQueue());
        }

        async void Walkin_Click(Object sender, EventArgs e)
        {

            //Gen new Queue today

            List<Models.FoodMini> listF = await firebaseHelper.GetAllFood(date_str);
            int queueNo = 0;
            if (listF != null)
            {

                queueNo = listF.Count();
                queueNo = queueNo + 1;
            }else
            {
                queueNo = 1;

            }
            await Navigation.PushAsync(new SummaryOrderT(queueNo));

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