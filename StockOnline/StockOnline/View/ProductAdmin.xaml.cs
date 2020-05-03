using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOnline.Data;
using StockOnline.Helper;
using StockOnline.Models;
using StockOnline.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StockOnline.Models;

namespace StockOnline.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductAdmin : ContentPage
    {


		ProductFirebaseHelper firebaseHelper = new ProductFirebaseHelper();

		public Guid product_id { get; set; }

		public ProductAdmin()
        {
            InitializeComponent();
        }


		public ProductAdmin(Guid p_id)
		{
			InitializeComponent();
			this.product_id = p_id;

			initAsync();

		}

		public async Task initAsync() {
			this.picker.SelectedIndex = 0;
			StockOnline.Models.Product productM = await firebaseHelper.GetProduct(this.product_id);
			Entry_ProductName.Text = productM.Name;
			Entry_ProductNameEn.Text = productM.NameEn;
			Entry_Detail.Text = productM.Details;
			Entry_Location.Text = productM.Location;
			Entry_ImageURL.Text = productM.ImageUrl;
			Entry_Cost.Text = productM.CostPerUnit + "" ;
			Entry_Quantity.Text = productM.Quantity + "";
			bt_Confirm.Text = "Confirm Update";

			if (productM.ProductType == "LINE")
			{
				this.picker.SelectedIndex = 0;
			}
			else if (productM.ProductType == "WALK")
			{
				this.picker.SelectedIndex = 1;
			}		
		}


		async void OnSaveButtonClicked(object sender, EventArgs e)
		{

			bool valid = true;
			if (this.Entry_ProductName.Text.Trim().Length == 0) {
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ Product Name", "OK");
			}
			if (valid && this.Entry_Cost.Text.Trim().Length == 0)
			{
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ Cost", "OK");
			}

			if (valid && this.Entry_Cost.Text.Trim().Length > 0)
			{
				try
				{

					decimal.Parse(this.Entry_Cost.Text.Trim());
				}
				catch (Exception ee) {
					valid = false;
					await DisplayAlert("Error", "กรุณาระบุ Cost ให้ถูกต้อง", "OK");

				}
				
			}

			if (valid && this.Entry_Detail.Text.Trim().Length == 0)
			{
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ Detail", "OK");
			}

			if (valid && this.Entry_ImageURL.Text.Trim().Length == 0)
			{
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ Image URL", "OK");
			}

			if (valid && this.Entry_Location.Text.Trim().Length == 0)
			{
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ Location", "OK");
			}

			if (valid && this.Entry_Quantity.Text.Trim().Length == 0)
			{
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ Cost ", "OK");
			}


			if (valid && this.picker.SelectedIndex == -1)
			{
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุประเภทลูกค้า ", "OK");
			}


			if (valid && this.Entry_Cost.Text.Trim().Length > 0)
			{
				try
				{

					decimal.Parse(this.Entry_Cost.Text.Trim());
				}
				catch (Exception ee)
				{
					valid = false;
					await DisplayAlert("Error", "กรุณาระบุ Cost ให้ถูกต้อง" + this.Entry_Cost.Text.Trim(), "OK");
				}		
					
				
			}


			int selectedIndex = picker.SelectedIndex;
			string customerType = "";
			if (selectedIndex != -1)
			{
				customerType = (string)picker.ItemsSource[selectedIndex];
			}



			if (valid) {
				if (bt_Confirm.Text.Equals("Confirm Update"))
				{

					//Update
					await firebaseHelper.UpdateFood(this.product_id, this.Entry_ProductName.Text.Trim(), this.Entry_ProductNameEn.Text.Trim(),  this.Entry_Location.Text.Trim(), this.Entry_Detail.Text.Trim(), Int32.Parse(this.Entry_Quantity.Text.Trim()), decimal.Parse(this.Entry_Cost.Text.Trim()), customerType, this.Entry_ImageURL.Text.Trim());
					await DisplayAlert("Complete", "Update Complete", "OK");
					bt_Confirm.Text = "Confirm";

					await Navigation.PushAsync(new ListProductAdmin());

				}
				else {
					//Insert
					await firebaseHelper.AddProduct(this.Entry_ProductName.Text.Trim(), this.Entry_ProductNameEn.Text.Trim(),  this.Entry_Location.Text.Trim(), this.Entry_Detail.Text.Trim(), Int32.Parse(this.Entry_Quantity.Text.Trim()), decimal.Parse(this.Entry_Cost.Text.Trim()), customerType, this.Entry_ImageURL.Text.Trim());

					await DisplayAlert("Complete", "Insert Complete", "OK");
					bt_Confirm.Text = "Confirm";

					await Navigation.PushAsync(new ListProductAdmin());


				}//end if 
			}//end if 
		}

		async void CancelOrder_Click(object sender, EventArgs e)
		{		

			var action = await DisplayAlert("Complete", "ต้องการยกเลิกการเพิ่มหรือแก้ไขข้อมูล", "Yes", "No");
			if (action) {

				await Navigation.PushAsync(new ListProductAdmin());
			}


		}
	}
}