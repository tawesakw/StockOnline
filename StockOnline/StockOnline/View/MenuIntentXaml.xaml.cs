using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StockOnline.Models;

namespace StockOnline.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuIntentXaml : ContentPage
	{
		public MenuIntentXaml ()
		{
			InitializeComponent ();
			
		}
		public void init() {
		}

		async void receiveOrderkClick(Object o, EventArgs e) {

			
			await Navigation.PushAsync(new Order());
		}


		async void viewOrderClick(Object o, EventArgs e)
		{

			await Navigation.PushAsync(new ListViewPage1());

		}


		async void outOfStockClick(Object o, EventArgs e)
		{

			await Navigation.PushAsync(new ListViewPage1());

		}

		async void productClick(Object o, EventArgs e)
		{

			//await Navigation.PushAsync(new Product());

			await Navigation.PushAsync(new PrintPage());
		}


	}
}

