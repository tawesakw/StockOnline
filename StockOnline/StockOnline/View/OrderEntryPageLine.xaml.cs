using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOnline.Data;
using StockOnline.Helper;
using StockOnline.Models;
using StockOnline.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace StockOnline.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrderEntryPageLine : ContentPage
	{

		ProductFirebaseHelper productFirebaseHelper = new ProductFirebaseHelper();

		FirebaseHelper firebaseHelper = new FirebaseHelper();
		string date_str = DateTime.Now.ToString("yyyy'-'MM'-'dd", CultureInfo.InvariantCulture).Replace("-", "");
		//string date_str = DateTime.Now.ToString("dd/MM/yyyy").Replace("/", "");
		public int queueNo { get; set; }

		public Guid GID { get; set; }

		int IndexValue;
		String NameValue;
		String LocationValue;
		String ImageUrlValue;
		String DetailsValue;

		String levelSpicyValue;

		public static IList<Food> Foods { get; private set; }

		Food selectedFood;
		public Food SelectedFood
		{
			get { return selectedFood; }
			set
			{
				if (selectedFood != value)
				{
					selectedFood = value;
					OnPropertyChanged();
				}
			}
		}


		public OrderEntryPageLine()
		{
			InitializeComponent();
			// BindingContext = new FoodsPageViewModel();
			//  //BindingContext = new MonkeysPageViewModel();
			//  init();
			initAsync();
		}

		public OrderEntryPageLine(int queueNo)
		{
			this.queueNo = queueNo;

			InitializeComponent();
			initAsync();
		}


		public OrderEntryPageLine(int queueNo, Guid gID)
		{
			this.queueNo = queueNo;
			this.GID = gID;
			InitializeComponent();
			initAsync();
		}

		async Task initAsync()
		{
			try
			{
				Foods = new List<Food>();
				List<Models.Product> listP = await productFirebaseHelper.GetAllProduct();
				var itemSourceListStr = new List<string>();


				for (int i = 0; i < listP.Count; i++)
				{
					Models.Product productM = listP[i];
					itemSourceListStr.Add(productM.Name);
					int count = i + 1;
					Foods.Add(new Food
					{
						ID = count,
						Name = productM.Name,
						NameEn = productM.NameEn,
						Location = productM.Location,
						Details = productM.Details,
						//	ImageUrl = "food1.jpg",
						ImageUrl = productM.ImageUrl,
						CostPerUnit = productM.CostPerUnit,
						Quantity = productM.Quantity,
						LevelSpicy = productM.LevelSpicy
						//
					});
				}

				picker.ItemsSource = itemSourceListStr;

				if (this.GID != null)
				{

					Models.Product product = await productFirebaseHelper.GetProduct(GID);

				}
			}
			catch (Exception ee)
			{
				string error_str = ee.Message;
				await DisplayAlert("Connection Problem", ee.Message, "OK");
			}


		}
		private void picker_SelectedIndexChanged(object sender, EventArgs e)
		{
			Picker oo = (Picker)sender;
			int select_index = oo.SelectedIndex;
			IndexValue = select_index;
			selectedFood = Foods[select_index];

			Name.Text = selectedFood.Name;
			NameEn.Text = selectedFood.NameEn;
			Location.Text = selectedFood.Location;
			Details.Text = selectedFood.Details;
			Entry_CostPerUnit.Text = selectedFood.CostPerUnit + "";

			Entry_CostPerUnit.Text = selectedFood.CostPerUnit + "";
			ImageUrl.Source = selectedFood.ImageUrl;


			if (selectedFood.LevelSpicy.Trim().Length > 0)
			{
				if (selectedFood.LevelSpicy.Trim().Equals("ไม่เผ็ด"))
				{

					pickerSpicy.SelectedIndex = 0;
				}
				else if (selectedFood.LevelSpicy.Trim().Equals("เผ็ดน้อย"))
				{
					pickerSpicy.SelectedIndex = 1;

				}
				else if (selectedFood.LevelSpicy.Trim().Equals("เผ็ดกลาง"))
				{
					pickerSpicy.SelectedIndex = 2;

				}
				else if (selectedFood.LevelSpicy.Trim().Equals("เผ็ดมาก"))
				{
					pickerSpicy.SelectedIndex = 3;

				}



			}
		}

			async void CancelOrder_Click(object sender, EventArgs e)
			{

			}
			async void OnSaveButtonClicked(object sender, EventArgs e)
			{

				bool error = false;
				Food food = Foods[IndexValue];
				int qq = 0;
				decimal costP = 0;
				try
				{
					qq = int.Parse(Entry_Quantity.Text.Trim());
					costP = decimal.Parse(Entry_CostPerUnit.Text.Trim());
					food.Quantity = qq;
					food.CostPerUnit = costP;
				}
				catch (Exception ee)
				{
					error = true;
					await DisplayAlert("Error", "จำนวนต้องเป็นตัวเลขจำนวนเต็ม", "OK");
					return;
				}

				string levelSpicyStr = "";

				if (picker.SelectedIndex >= 0)
				{

					if (picker.SelectedIndex == 0)
					{

						levelSpicyStr = "ไม่เผ็ด";
					}
					else if (picker.SelectedIndex == 1)
					{

						levelSpicyStr = "เผ็ดน้อย";

					}
					else if (picker.SelectedIndex == 2)
					{

						levelSpicyStr = "เผ็ดกลาง";

					}
					else if (picker.SelectedIndex == 3)
					{

						levelSpicyStr = "เผ็ดมาก";

					}

				}

				if (!error)
				{

					//await firebaseHelper.AddFood(food.Name, food.Location, food.Details, qq, DateTime.Now);
					//await firebaseHelper.AddFood(food.Name, food.Location, food.Details, qq, DateTime.Now, this.queueNo, "LINE", "P", costP,  date_str);
					await firebaseHelper.AddFood(food.Name, food.NameEn, food.Location, food.Details, qq, DateTime.Now, "P", this.queueNo, food.CostPerUnit, "LINE", date_str, levelSpicyStr);
					await DisplayAlert("Complete ", "Order Complete", "Ok");

					Utils utils = new Utils();
					string alertMessage = await utils.adjustRemainStockAsync(food.Name);
					if (alertMessage != null && alertMessage.Length > 0) {
						await DisplayAlert("Stock Alert ", alertMessage, "Ok");

					}


				await Navigation.PushAsync(new SummaryOrderL(this.queueNo));

				}



			}

			async void OnDeleteButtonClicked(object sender, EventArgs e)
			{
				var food = (Food)BindingContext;


				await App.Database.DeleteNoteAsync(food);
				await Navigation.PopAsync();

			}


		}
	
}