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
    public partial class ListReport : ContentPage
    {
        public ListReport()
        {
            InitializeComponent();

            init();
        }

        public void init() {

            List<Models.ReportMini> listP = new List<Models.ReportMini>();
            Models.ReportMini mini = new Models.ReportMini();
            mini.ReportName = "รายงานยอดขายประจำวัน";
            listP.Add(mini);

            mini = new Models.ReportMini();
            mini.ReportName = "รายงานยอดขาย Menu ยอดนิยม";
            listP.Add(mini);

            /*
            mini = new Models.ReportMini();
            mini.ReportName = "รายงานยอดขายประจำวัน";
            listP.Add(mini);
            */

            listReport.ItemsSource = listP;

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

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                ListView listView = (ListView)sender;
                Models.ReportMini mini = (Models.ReportMini)listView.SelectedItem;
                if (mini != null && mini.ReportName.Equals("รายงานยอดขายประจำวัน"))
                {
                  //  await Navigation.PushAsync(new ViewDailyOrder());
                    await Navigation.PushAsync(new ViewSummaryByDate());
                }
               else if (mini != null && mini.ReportName.Equals("รายงานยอดขาย Menu ยอดนิยม"))
                {
                    await Navigation.PushAsync(new ViewFavoritesMenu());
                }
                else {
                    await DisplayAlert("Choose Report", "Please select Report", "Yes");
                }
            }
        }

    }
}