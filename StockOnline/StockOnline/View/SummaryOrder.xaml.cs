using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOnline.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StockOnline.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SummaryOrder : ContentPage
    {
        public SummaryOrder()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = await App.Database.GetNotesAsync();
        }

        async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new OrderEntryPage
           // {
           //    BindingContext = new Food()
           // });

            await Navigation.PushAsync(new OrderEntryPage
            {
                BindingContext = new Food()
            });

        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new OrderEntryPage
                {
                  //BindingContext = e.SelectedItem as Monkey
                     BindingContext = e.SelectedItem as Food
                });
            }
        }


    }
}