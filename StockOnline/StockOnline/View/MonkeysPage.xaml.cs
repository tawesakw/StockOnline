using StockOnline.ViewModels;
using Xamarin.Forms;

namespace StockOnline.View
{
	public partial class MonkeysPage : ContentPage
	{
		public MonkeysPage()
		{
			InitializeComponent();
			BindingContext = new MonkeysPageViewModel();
		}
	}
}
