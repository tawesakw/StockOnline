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
    public partial class MaterialDetail : ContentPage
    {

		MaterialSourceFirebaseHelper sourcefirebaseHelper = new MaterialSourceFirebaseHelper();
		MaterialFirebaseHelper headerfirebaseHelper = new MaterialFirebaseHelper();
		MaterialDetailFirebaseHelper detailFirebaseHelper = new MaterialDetailFirebaseHelper();


		public Guid guid_id { get; set; }
		public string mHeader_id { get; set; }

		public MaterialDetail()
        {
            InitializeComponent();
        }


		public MaterialDetail(Guid g_id, string headerId)
		{
			InitializeComponent();
			this.guid_id =g_id;
			this.mHeader_id = headerId;

			initAsync();

		}

		public async Task initAsync() {
			this.picker.SelectedIndex = 0;
			//StockOnline.Models.MaterialHeader materialM = await firebaseHelper.GetMaterial(this.material_id);

		//	StockOnline.Models.MaterialSource materialSource = await sourcefirebaseHelper.GetMaterialSource(this.material_id);

			listAllMaterial.ItemsSource = await sourcefirebaseHelper.GetAllMaterialSource();
			/*

			Entry_MaterialName.Text = materialSource.Name;
			Entry_MaterialNameEn.Text = materialSource.NameEn;
			
			Entry_Quantity.Text = materialSource.Quantity + "";
			Entry_QuantityAlert.Text = materialSource.QuantityAlert + "";
			Entry_Remark.Text = materialSource.Remark;


			bt_Confirm.Text = "Confirm Update";


			if (materialSource.UnitName == "กิโลกรัม")
			{
				this.picker.SelectedIndex = 0;
			}
			else if (materialSource.UnitName == "ช้อนชา")
			{
				this.picker.SelectedIndex = 1;
			}
			*/

		}

		async void OnSaveButtonClicked(object sender, EventArgs e)
		{

			bool valid = true;
			if (valid && this.Entry_Quantity.Text.Trim().Length == 0)
			{
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ Cost", "OK");
			}

			if (valid && this.Entry_Quantity.Text.Trim().Length > 0)
			{
				try
				{

					decimal.Parse(this.Entry_Quantity.Text.Trim());
				}
				catch (Exception ee) {
					valid = false;
					await DisplayAlert("Error", "กรุณาระบุ จำนวน ให้ถูกต้อง", "OK");

				}				
			}	

	
			if (picker.SelectedIndex < 0) {

				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ หน่วย", "OK");
			}

			int selectedIndex = picker.SelectedIndex;
			string unitType = "";
			if (selectedIndex != -1)
			{
				unitType = (string)picker.ItemsSource[selectedIndex];
			}


			string mat_select = listAllMaterial.SelectedItem.ToString();
			MaterialSource mSource = await sourcefirebaseHelper.GetMaterialSourceByNameEn(mat_select);


			if (valid) {
				if (bt_Confirm.Text.Equals("Confirm Update"))
				{

					//Update
					await detailFirebaseHelper.UpdateMaterial(this.guid_id, this.mHeader_id , mSource.ID, unitType, decimal.Parse(this.Entry_Quantity.Text.Trim()));
					await DisplayAlert("Complete", "Update Complete", "OK");
					bt_Confirm.Text = "Confirm";

					await Navigation.PushAsync(new ListMaterialAdmin());

				}
				else {
					//Insert
					await detailFirebaseHelper.AddMaterialDetail(this.mHeader_id, mSource.ID, unitType, decimal.Parse(this.Entry_Quantity.Text.Trim()));

					await DisplayAlert("Complete", "Insert Complete", "OK");
					bt_Confirm.Text = "Confirm";

					await Navigation.PushAsync(new ListMaterialAdmin());


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