using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StockOnline.Models;

using System.IO;
using System.Reflection;
using StockOnline.Data;

namespace StockOnline.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage1 : ContentPage
    {

        public IList<Food> foodList { get; private set; }

        public ObservableCollection<string> Items { get; set; }

        public ListViewPage1()
        {
            InitializeComponent();

            init();

            getQno();
        }


        void getQno() {

            try
            {

                var db = DependencyService.Get<ISLite>();
                var conn = db.GetConnection();

                // create the tables
                if (conn != null)
                {
                    try
                    {
                        //   conn.CreateTable<Models.Users>();
                        //  conn.CreateTable<Models.Order>();
                        //co .CreateDatabaseAndTables();

                        var maxID = conn.Table<Order>().Max(x => x.Id);
                        if (int.Parse(maxID.ToString()) >   0)
                        {
                            lb_Qno.Text = "Queue No:" + maxID.ToString() + 1;

                        }
                        else {
                            lb_Qno.Text = "Queue No:" + maxID;
                        }
                        /*
                        var orderE = conn.Query<Order>("SELECT * FROM Order order by Id desc);
                        foreach (var s in orderE)
                        {
                            Console.WriteLine("a " + s.Symbol);
                        }
                        */


                    }
                    catch (Exception ee)
                    {
                        string message = ee.Message;
                        lb_Qno.Text = "Queue No:1";
                    }
                }
            }
            catch (Exception ee) { 
            
            }
        
        
        
        }
        void init() {
            foodList = new List<Food>();
            foodList.Add(new Food
            {
                ID = 1,
                Name = "กระเพรา",
                Location = "Test",
                ImageUrl = "food1.jpg",
                //  ImageUrl = "https://images.surfacemag.com/app/uploads/2017/12/08140429/jackson-pollock-strudel-christopher-boffoli-surface.jpg"
            }) ;

            foodList.Add(new Food
            {
                ID = 2,
                Name = "ข้าวผัด",
                Location = "Test2",
                ImageUrl = "food1.jpg"
                // ImageUrl = "https://images.surfacemag.com/app/uploads/2017/12/08140429/jackson-pollock-strudel-christopher-boffoli-surface.jpg"
            });


            foodList.Add(new Food
            {
                ID = 3,
                Name = "ก๋วยเตี๋ยว",
                Location = "Test3",
                ImageUrl = "food1.jpg"
                // ImageUrl = "https://images.surfacemag.com/app/uploads/2017/12/08140429/jackson-pollock-strudel-christopher-boffoli-surface.jpg"
            });

            foodList.Add(new Food   
            {
                ID = 4,
                Name = "กระเพราะหมู เผ็ดน้อย",
                Location = "4",
                ImageUrl = "food1.jpg"
                // ImageUrl = "https://images.surfacemag.com/app/uploads/2017/12/08140429/jackson-pollock-strudel-christopher-boffoli-surface.jpg"
            });

            foodList.Add(new Food
            {
                ID = 5,
                Name = "กระเพราะหมู เผ็ดกลาง",
                Location = "5",
                ImageUrl = "food1.jpg",
                // ImageUrl = "https://images.surfacemag.com/app/uploads/2017/12/08140429/jackson-pollock-strudel-christopher-boffoli-surface.jpg"
                Quantity = 0
            });


            foodList.Add(new Food
            {
                ID = 6,
                Name = "กระเพราะหมู เผ็ดมาก",
                Location = "6",
                ImageUrl = "food1.jpg",
                Quantity = 0
                // ImageUrl = "https://images.surfacemag.com/app/uploads/2017/12/08140429/jackson-pollock-strudel-christopher-boffoli-surface.jpg"
            });


            foodList.Add(new Food
            {
                ID = 7,
                Name = "กระเพราะหมูตุ๋น เผ็ดน้อย",
                Location = "7",
                ImageUrl = "food1.jpg",
                Quantity = 0
                // ImageUrl = "https://images.surfacemag.com/app/uploads/2017/12/08140429/jackson-pollock-strudel-christopher-boffoli-surface.jpg"
             
            });
            foodList.Add(new Food
            {
                ID = 8,
                Name = "กระเพราะหมูตุ๋น เผ็ดกลาง",
                Location = "8",

                ImageUrl = "food1.jpg",
                // ImageUrl = "https://images.surfacemag.com/app/uploads/2017/12/08140429/jackson-pollock-strudel-christopher-boffoli-surface.jpg"
                Quantity = 0
            });

            foodList.Add(new Food
            {
                ID = 9,
                Name = "กระเพราะหมูตุ๋น เผ็ดมาก",
                Location = "9",

                ImageUrl = "food1.jpg",
                // ImageUrl = "https://images.surfacemag.com/app/uploads/2017/12/08140429/jackson-pollock-strudel-christopher-boffoli-surface.jpg"
                Quantity = 0
            }) ;                                 
            BindingContext = this;
            MyListView.ItemsSource = foodList;
        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Food selectedItem = e.SelectedItem as Food;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Food tappedItem = e.Item as Food;
        }

        void CancelOrder_Click(Object o, EventArgs e) { 
        
        }


        void ConfirmOrder_Click(Object o, EventArgs e)
        {
            //  DisplayAlert("Print Order", "Print Order", "Oke");

            // Navigation.PushAsync(new ViewOrder());
            Navigation.PushAsync(new PrintPage());


        }

        void selectPicker(Object o, EventArgs e)
        {
            //  DisplayAlert("Select Value", "Print Order", "Oke");

            var PickerSelect = (Picker)o;

            if (PickerSelect != null) {

  


            }
            /*
            var ob = checkbox.BindingContext as < your model name>;

            if (ob != null)

            {

                AddOrUpdatetheResult(ob, checkbox);

            }
            */
            // Navigation.PushAsync(new ViewOrder());
            //  Navigation.PushAsync(new PrintPage());


        }
        

        void ReceiveOrderkClick(Object o, EventArgs e)
        {
            //  DisplayAlert("Print Order", "Print Order", "Oke");

          //  Navigation.PushAsync(new ViewOrder());



        }


        
        /*
        private void printPdf() {
            //StockOnline.Droid.Common common  

            Stream documentStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("PDFPrintingSample.Assets.GIS Succinctly.pdf");
            pdfViewer.LoadDocument(documentStream);
            Stream printStream = pdfViewer.SaveDocument();
            DependencyService.Get<IPrintService>().Print(printStream, "GIS Succinctly.pdf");


        }
        */
               
            async void ReceiveOrderClick(Object o, EventArgs e)
        {


            //await Navigation.PushAsync(new ListViewPage1());
        }


        async void viewOrderClick(Object o, EventArgs e)
        {

           // await Navigation.PushAsync(new ListViewPage1());

        }


        async void outOfStockClick(Object o, EventArgs e)
        {

           ///await Navigation.PushAsync(new ListViewPage1());

        }



    }
}
