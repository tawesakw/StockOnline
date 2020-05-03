using System.Collections.Generic;
using StockOnline.Models;
using StockOnline.ViewModels;

namespace StockOnline.ViewModels
{
	public class FoodsPageViewModel : ViewModelBase
	{
		public IList<Food> Foods { get { return FoodData.Foods; } }

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
	}
}
