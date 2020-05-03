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
    public partial class UserAdmin : ContentPage
    {
		UserFirebaseHelper firebaseHelper = new UserFirebaseHelper();

		public Guid user_id { get; set; }
		string dUser = "";
		public UserAdmin()
		{
			InitializeComponent();
		}


		public UserAdmin(Guid p_id)
		{
			InitializeComponent();
			this.user_id = p_id;

			initAsync();

		}

		public async Task initAsync()
		{
			StockOnline.Models.User userM = await firebaseHelper.GetUser(this.user_id);
			this.Entry_UserName.Text = userM.UserName;
			this.Entry_Password.Text = userM.Password;
			this.Entry_RePassword.Text = userM.Password;
			this.Entry_Detail.Text = userM.Description;


			if (userM.Level == "Order")
			{
				this.picker.SelectedIndex= 0;
			}
			else if (userM.Level == "Manager")
			{
				this.picker.SelectedIndex = 1;
			}
			else if (userM.Level == "Admin")
			{
				this.picker.SelectedIndex = 2;
			}


				//	picker.FindByName(userM.Level);
				//comboBox1.SelectedIndex = index;


				//this.picker.SelectedIndex = 0;
				bt_Confirm.Text = "Confirm Update";


		}




		async void OnSaveButtonClicked(object sender, EventArgs e)
		{

			bool valid = true;
			if (this.Entry_UserName.Text.Trim().Length == 0)
			{
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ User Name", "OK");
			}
			if (valid && this.Entry_Password.Text.Trim().Length == 0)
			{
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ Password", "OK");
			}

			if (valid && this.Entry_RePassword.Text.Trim().Length == 0)
			{
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ Re Password", "OK");
			}
					   
			if (valid && ! this.Entry_RePassword.Text.Trim().Equals(this.Entry_Password.Text.Trim()))
			{
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ Password เท่ากับ Re Password", "OK");
			}


			if (valid && this.picker.SelectedIndex == -1)
			{
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ User Level ", "OK");
			}

			string userLevel = "";
			int selectedIndex = picker.SelectedIndex;

			if (selectedIndex != -1)
			{
				userLevel = (string)picker.ItemsSource[selectedIndex];
			}



			if (valid)
			{
				if (bt_Confirm.Text.Equals("Confirm Update"))
				{			

					//Update
					await firebaseHelper.UpdateUser(this.user_id, this.Entry_UserName.Text.Trim(), this.Entry_Password.Text.Trim(),  this.Entry_Detail.Text.Trim(), userLevel);
					await DisplayAlert("Complete", "Update Complete", "OK");
					bt_Confirm.Text = "Confirm";

					await Navigation.PushAsync(new ListUserAdmin());

				}
				else
				{
					//Insert
					await firebaseHelper.AddUser(this.Entry_UserName.Text.Trim(), this.Entry_Password.Text.Trim(),  this.Entry_Detail.Text.Trim(), userLevel);

					await DisplayAlert("Complete", "Insert Complete", "OK");
					bt_Confirm.Text = "Confirm";

					await Navigation.PushAsync(new ListUserAdmin());


				}//end if 
			}//end if 
		}

		async void CancelOrder_Click(object sender, EventArgs e)
		{

			var action = await DisplayAlert("Complete", "ต้องการยกเลิกการเพิ่มหรือแก้ไขข้อมูล", "Yes", "No");
			if (action)
			{

				await Navigation.PushAsync(new ListUserAdmin());
			}


		}
	}
}