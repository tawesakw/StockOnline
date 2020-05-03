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
    public partial class MaterialFoodAdmin : ContentPage
    {

		MaterialSourceFirebaseHelper sourcefirebaseHelper = new MaterialSourceFirebaseHelper();
		MaterialFirebaseHelper headerfirebaseHelper = new MaterialFirebaseHelper();
		MaterialDetailFirebaseHelper detailbaseHelper = new MaterialDetailFirebaseHelper();

		public Guid materialHeader_id { get; set; }



		public MaterialFoodAdmin()
        {
            InitializeComponent();
			bt_Confirm.IsEnabled = false;
			GetAllMaterialNotSetInit();


		}


		public MaterialFoodAdmin(Guid m_id)
		{
			InitializeComponent();
			this.materialHeader_id = m_id;

			initAsync(m_id);


		}


		public async void initAsync(Guid g_id)
		{
			try
			{

				Utils utils = new Utils();
				//Models.MaterialHeader h = await headerfirebaseHelper.GetMaterialByProductId(utils.convertObject(g_id));
			//	Models.Product product = await new ProductFirebaseHelper().GetProduct(g_id);
				MaterialHeader h = await headerfirebaseHelper.GetMaterial(g_id);
				
				List<Models.MaterialDetail> listDetail = await detailbaseHelper.GetAllMaterialByHeader(utils.convertObject(h.ID));
				
				listView.ItemsSource =  listDetail;

				List<Models.MaterialSource> listDetailAll2 = new List<MaterialSource>();
				MaterialSourceFirebaseHelper sourceFire = new MaterialSourceFirebaseHelper();
				//List<Models.MaterialSource> listDetailAll = await sourcefirebaseHelper.GetAllMaterialSource();
				/*
				for (int i = 0; i < listDetailAll.Count; i++) {
					Models.MaterialSource source = listDetailAll[i];
					


				}
				*/

			//	listViewMaterialDetail.ItemsSource = listDetailAll;
			}
			catch (Exception ee)
			{
				string error_str = ee.Message;
				//return null;
			}
			
		}


		public async Task<List<Models.Product>> GetAllMaterialNotSetInit()
		{
			List<Models.Product> listP = await new ProductFirebaseHelper().GetAllProduct();
			try
			{

				List<MaterialHeader> listM = await new MaterialFirebaseHelper().GetAllMaterial();
				for (int i = 0; i < listM.Count; i++)
				{
					MaterialHeader mat = listM[i];


					for (int j = 0; j < listP.Count; j++)
					{
						Models.Product p = listP[j];

						if (p.ID.ToString().Equals(mat.ProductID.ToString()))
						{
							listP.Remove(p);
						}
					}
				}
				listView.ItemsSource = listP;


			}
			catch (Exception ee)
			{
				string error_str = ee.Message;
				return null;
			}
			return listP;
		}


		public async Task<List<Models.Product>> GetAllMaterialNotSet()
		{

			List<Models.Product> listP = await new ProductFirebaseHelper().GetAllProduct();
			try
			{

				List<MaterialHeader> listM = await new MaterialFirebaseHelper().GetAllMaterial();
				for (int i = 0; i < listM.Count; i++)
				{
					MaterialHeader mat = listM[i];


					for (int j = 0; j < listP.Count; j++)
					{
						Models.Product p = listP[j];

						if (p.ID.ToString().Equals(mat.ProductID.ToString()))
						{
							listP.Remove(p);
						}
					}

				}


				listView.ItemsSource = listP;


			}
			catch (Exception ee)
			{
				string error_str = ee.Message;
				return null;
			}
			return listP;
		}
		async void OnListViewItemSelected(object sender, EventArgs e)
		{
			Xamarin.Forms.ListView listView1 = (Xamarin.Forms.ListView)sender;
			Models.MaterialDetail detail_select = (Models.MaterialDetail)listView1.SelectedItem;
			Utils utils = new Utils();

			Models.MaterialHeader header_select  = await headerfirebaseHelper.GetMaterial(new Guid(detail_select.HeaderID));

			//header_select.ProductID

			if (this.materialHeader_id != header_select.ID)
			{
				ProductFirebaseHelper productbaseHelper = new ProductFirebaseHelper();

				Models.Product product_select = await productbaseHelper.GetProduct(new Guid(header_select.ProductID));
				await headerfirebaseHelper.AddMaterialHeader(utils.convertObject(product_select.Name), utils.convertObject(product_select.NameEn), utils.convertObject(product_select.ID));
				await Navigation.PushAsync(new MaterialFoodDetail(utils.convertObject(product_select.ID)));
			}
			else
			{
				ProductFirebaseHelper productbaseHelper = new ProductFirebaseHelper();
				Models.Product product_select = await productbaseHelper.GetProduct(new Guid(header_select.ProductID));
				await Navigation.PushAsync(new MaterialFoodDetail(header_select.ID,"Update"));
			}


		}

		async void OnListMaterialDetailItemSelected(object sender, EventArgs e)
		{
			Xamarin.Forms.ListView listView1 = (Xamarin.Forms.ListView)sender;
			Models.MaterialSource source_select = (Models.MaterialSource)listView1.SelectedItem;
			Utils utils = new Utils();

			//await headerfirebaseHelper.AddMaterialHeader(utils.convertObject(product_select.Name), utils.convertObject(product_select.NameEn), utils.convertObject(product_select.ID));

			await Navigation.PushAsync(new MaterialFoodDetail(source_select.ID));
		}

		/*
		public async Task initAsync() {
			this.picker.SelectedIndex = 0;
			//StockOnline.Models.MaterialHeader materialM = await firebaseHelper.GetMaterial(this.material_id);

			StockOnline.Models.MaterialSource materialSource = await sourcefirebaseHelper.GetMaterialSource(this.material_id);

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

		}


		async void OnSaveButtonClicked(object sender, EventArgs e)
		{

			bool valid = true;
			if (this.Entry_MaterialName.Text.Trim().Length == 0) {
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ Material Name", "OK");
			}

			if (this.Entry_MaterialNameEn.Text.Trim().Length == 0)
			{
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ Material Name English", "OK");
			}


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





			if (valid && this.Entry_QuantityAlert.Text.Trim().Length == 0)
			{
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ จำนวนแจ้งเตือน", "OK");
			}

			if (valid && this.Entry_QuantityAlert.Text.Trim().Length > 0)
			{
				try
				{

					decimal.Parse(this.Entry_Quantity.Text.Trim());
				}
				catch (Exception ee)
				{
					valid = false;
					await DisplayAlert("Error", "กรุณาระบุ จำนวนแจ้งเตือน ให้ถูกต้อง", "OK");

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


			Utils utils = new Utils();

			if (valid) {
				if (bt_Confirm.Text.Equals("Confirm Update"))
				{

					//Update
					await sourcefirebaseHelper.UpdateMaterialSource(this.material_id,this.Entry_MaterialName.Text.Trim(), this.Entry_MaterialNameEn.Text.Trim(), unitType, int.Parse(this.Entry_Quantity.Text.Trim()), int.Parse(this.Entry_QuantityAlert.Text.Trim()), utils.convertObject(this.Entry_Remark.Text));
					await DisplayAlert("Complete", "Update Complete", "OK");
					bt_Confirm.Text = "Confirm";

					await Navigation.PushAsync(new ListMaterialAdmin());

				}
				else {
					//Insert


					await sourcefirebaseHelper.AddMaterialSource(this.Entry_MaterialName.Text.Trim(), this.Entry_MaterialNameEn.Text.Trim(), unitType, int.Parse(this.Entry_Quantity.Text.Trim()), int.Parse(this.Entry_QuantityAlert.Text.Trim()), utils.convertObject(this.Entry_Remark.Text));

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

	*/
		async void OnAddButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new MaterialFoodDetail(this.materialHeader_id, "header"));
			
		}
		
		async void OnSaveButtonClicked(object sender, EventArgs e)
		{

		}

		async void CancelOrder_Click(object sender, EventArgs e)
		{
		}

		}
	}