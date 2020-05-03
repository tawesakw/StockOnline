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
    public partial class MaterialFoodDetail : ContentPage
    {

		MaterialSourceFirebaseHelper sourcefirebaseHelper = new MaterialSourceFirebaseHelper();
		MaterialFirebaseHelper headerfirebaseHelper = new MaterialFirebaseHelper();
		MaterialDetailFirebaseHelper detailbaseHelper = new MaterialDetailFirebaseHelper();
		ProductFirebaseHelper productbaseHelper = new ProductFirebaseHelper();
		public String product_id { get; set; }
		public Guid detail_id;
		public Guid header_id;
		public String type;
		public Models.MaterialSource source_select { get; set; }

		public MaterialFoodDetail()
        {
            InitializeComponent();

			
		}


		public MaterialFoodDetail(string pId)
		{
			InitializeComponent();
			this.product_id = pId;


			InitAsync();

		}

		public MaterialFoodDetail(Guid detailId)
		{
			InitializeComponent();
			this.detail_id = detailId;


			InitAsyncDetail();

		}


		public MaterialFoodDetail(Guid headerId, string type0)
		{
			InitializeComponent();
			this.header_id = headerId;
			this.type = type0;
			initialKeyNew();
		}


		public async Task initialKeyNew()
		{

			listMatrialView.ItemsSource = await sourcefirebaseHelper.GetAllMaterialSource();
			Models.MaterialHeader header = await headerfirebaseHelper.GetMaterial(this.header_id);
			Models.Product product = await productbaseHelper.GetProduct(new Guid(header.ProductID));

			Lbl_FoodLabel.Text = product.NameEn;

		}

		public async Task InitAsync() {
			Utils utils = new Utils();
			try
			{
				StockOnline.Models.Product pro = await productbaseHelper.GetProduct(new Guid(product_id));
				StockOnline.Models.MaterialHeader header = await headerfirebaseHelper.GetMaterialByProductId(product_id);
				Lbl_FoodLabel.Text = pro.NameEn;
				listMatrialView.ItemsSource = await sourcefirebaseHelper.GetAllMaterialSource();

			}
			catch (Exception ee) { }
	
		}
		public async Task InitAsyncDetail()
		{
			Utils utils = new Utils();
			MaterialDetailFirebaseHelper detailFirebase = new MaterialDetailFirebaseHelper();
			listMatrialView.ItemsSource = await detailbaseHelper.GetAllMaterialDetail();

			try
			{
				Models.MaterialDetail detailObject = await detailbaseHelper.GetDetailById(this.detail_id);
				if (detailObject != null)
				{
					MaterialSourceFirebaseHelper sourceFirebase = new MaterialSourceFirebaseHelper();
					MaterialSource sourceObject = await sourceFirebase.GetMaterialSourceByID(utils.convertObject(detailObject.MaterialID));

					MaterialFirebaseHelper headerFirebase = new MaterialFirebaseHelper();
					ProductFirebaseHelper productFirebase = new ProductFirebaseHelper();
					MaterialHeader headerObject = await headerFirebase.GetMaterial(new Guid(detailObject.HeaderID));
					Models.Product productObject = await productFirebase.GetProduct(new Guid(headerObject.ProductID));

					Lbl_FoodLabel.Text = productObject.NameEn;
					Lbl_UnitDisplay.Text = detailObject.UnitName;
					Entry_Quantity.Text = detailObject.Quantity + "";
					listMatrialView.ItemsSource = null;
					Lbl_MaterialLabel.Text = sourceObject.NameEn;
				}
			}
			catch (Exception ee) { }
		}

		async void BackButtonClicked(object sender, EventArgs e) {
			await Navigation.PushAsync(new ListAllMaterialFood());

		}

	  async void OnSaveButtonClicked(object sender, EventArgs e)
		{

			bool valid = true;
			decimal quantity = 0;

			if (valid && this.Entry_Quantity.Text.Trim().Length == 0)
			{
				valid = false;
				await DisplayAlert("Error", "กรุณาระบุ Cost", "OK");
			}

			if (valid && this.Entry_Quantity.Text.Trim().Length > 0)
			{
				try
				{
					quantity = decimal.Parse(this.Entry_Quantity.Text.Trim());				
				}
				catch (Exception ee) {
					valid = false;
					await DisplayAlert("Error", "กรุณาระบุ จำนวน ให้ถูกต้อง", "OK");
				}				
			}

			if (valid && this.source_select == null) {
				valid = false;
			}
			Utils utils = new Utils();



			//StockOnline.Models.Product pro = await productbaseHelper.GetProductByNameEn(this.source_select.NameEn);
			StockOnline.Models.MaterialHeader header = await headerfirebaseHelper.GetMaterial(this.header_id);

			StockOnline.Models.Product pro = await productbaseHelper.GetProduct(new Guid(header.ProductID));

			Lbl_FoodLabel.Text = this.source_select.NameEn;
			if (valid) {
				if (this.type.Equals("Update"))
				{
					/*
					detailbaseHelper.UpdateMaterial()
					//Update
					await sourcefirebaseHelper.UpdateMaterialSource(this.material_id,this.Entry_MaterialName.Text.Trim(), this.Entry_MaterialNameEn.Text.Trim(), unitType, int.Parse(this.Entry_Quantity.Text.Trim()), int.Parse(this.Entry_QuantityAlert.Text.Trim()), utils.convertObject(this.Entry_Remark.Text));
					await DisplayAlert("Complete", "Update Complete", "OK");
					bt_Confirm.Text = "Confirm";

					await Navigation.PushAsync(new ListMaterialAdmin());
					*/
				}
				else {
					//Insert
					

					await detailbaseHelper.AddMaterialDetail(utils.convertObject(header_id), source_select.ID, source_select.UnitName, quantity);

					await DisplayAlert("Complete", "Insert Complete", "OK");
					bt_Confirm.Text = "Confirm";

					//await Navigation.PushAsync(new ListMaterialF());


				}//end if 
			}//end if 
		}

		async void OnListViewItemSelected(object sender, EventArgs e)
		{
			Xamarin.Forms.ListView listView1 = (Xamarin.Forms.ListView)sender;
			//Models.MaterialDetail detail =  (Models.MaterialDetail)listMatrialView.SelectedItem;

			this.source_select =  (Models.MaterialSource)listMatrialView.SelectedItem;
		//	this.source_select = await sourcefirebaseHelper.GetMaterialSource(detail.MaterialID);
			//this.source_select =
			Lbl_UnitDisplay.Text = source_select.UnitName;
			//Utils utils = new Utils();
		}
		async void OnAddButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new MaterialFoodDetail(header_id, "header"));

		}

		async void CancelOrder_Click(object sender, EventArgs e)
		{		
			var action = await DisplayAlert("Complete", "ต้องการยกเลิกการเพิ่มหรือแก้ไขข้อมูล", "Yes", "No");
			if (action) {

			//	await Navigation.PushAsync(new ListProductAdmin());
			}

		}
	}
}