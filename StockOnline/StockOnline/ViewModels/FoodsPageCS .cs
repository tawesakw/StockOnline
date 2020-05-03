using Xamarin.Forms;

namespace StockOnline.ViewModels
{
	public class FoodsPageCS : ContentPage
	{
		public FoodsPageCS()
		{
			var picker = new Picker { Title = "Select Order", TitleColor = Color.Red };
			picker.SetBinding(Picker.ItemsSourceProperty, "Foods");
			picker.SetBinding(Picker.SelectedItemProperty, "SelectedFood");
			picker.ItemDisplayBinding = new Binding("Name");

			var nameLabel = new Label { HorizontalOptions = LayoutOptions.Center };
			nameLabel.SetBinding(Label.TextProperty, "SelectedFood.Name");
			nameLabel.SetDynamicResource(VisualElement.StyleProperty, "TitleStyle");


			var quantityText = new Label { FontAttributes = FontAttributes.Italic, HorizontalOptions = LayoutOptions.Center };
			quantityText.SetBinding(Label.TextProperty, "SelectedFood.Quantity");
					   			 
			
			var locationLabel = new Label { FontAttributes = FontAttributes.Italic, HorizontalOptions = LayoutOptions.Center };
			locationLabel.SetBinding(Label.TextProperty, "SelectedFood.Location");

			var costPerUnitLabel = new Label { FontAttributes = FontAttributes.Italic, HorizontalOptions = LayoutOptions.Center };
			costPerUnitLabel.SetBinding(Label.TextProperty, "SelectedFood.CostPerUnit");


			var image = new Image { HeightRequest = 200, WidthRequest = 200, HorizontalOptions = LayoutOptions.CenterAndExpand };
			image.SetBinding(Image.SourceProperty, "SelectedFood.ImageUrl");

			var detailsLabel = new Label();
			detailsLabel.SetBinding(Label.TextProperty, "SelectedFood.Details");
			detailsLabel.SetDynamicResource(VisualElement.StyleProperty, "BodyStyle");

			Content = new ScrollView
			{
				Content = new StackLayout
				{
					Margin = new Thickness(20,35,20,20),
					Children =
					{
						new Label { Text = "Foods", FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },
						picker,
						nameLabel,
						quantityText,
						locationLabel,
						costPerUnitLabel,
						image,
						detailsLabel
					}
				}
			};

			BindingContext = new FoodsPageViewModel();
		}
	}
}
