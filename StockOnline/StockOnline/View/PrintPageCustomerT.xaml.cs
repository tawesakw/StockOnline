using System.Text;
using StockOnline.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StockOnline.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrintPageCustomerT : ContentPage
	{
		public PrintPageCustomerT()
		{
			InitializeComponent ();
			StringBuilder sb = new StringBuilder();
			sb.Append("Queue No 1 \r'n");
			sb.Append("-----------------------------------\r\n");
			sb.Append("กระเพรา หมูสับไม่เผ็ด   1      50   บาท  \r\n");
			sb.Append("กระเพรา หมูสับไม่เผ็ด   2      100   บาท \r\n");
			sb.Append("รวม                3      150   บาท \r\n");
			sb.Append("-----------------------------------\r\n");
			sb.Append("-----------------------------------\r\n");

			PrintPageViewModel.message = sb.ToString();
		
		}

		/*
		protected override void OnStart()
		{
		//	Debug.WriteLine("OnStart");

		}*/
	}
}