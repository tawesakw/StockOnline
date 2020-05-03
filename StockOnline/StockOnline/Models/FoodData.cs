using System;
using System.Collections.Generic;
using System.Text;

namespace StockOnline.Models
{
    public class FoodData
    {

		public static IList<Food> Foods { get; private set; }

		static FoodData()
		{
			Foods = new List<Food>();

			Foods.Add(new Food
			{
				ID = 1,
				Name = "กระเพราหมู เผ็ดน้อย",
				Location = "ตะลิวปลิว",
				Details = "อร่อยสุดๆ ไปเลยจ้า. ใส่พริกเม็ดเดียว",
				ImageUrl = "food1.jpg",
				CostPerUnit = 30,
				Quantity = 0

				//	ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
			}); 


			Foods.Add(new Food
			{
				ID = 2,
				Name = "กระเพราหมู เผ็ดกลาง",
				Location = "ตะลิวปลิว",
				Details = "อร่อยสุดๆ ไปเลยจ้า. ใส่พริก 3 เม็ด",
				ImageUrl = "food1.jpg",
				CostPerUnit = 30,
				Quantity = 0

				//	ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
			});


			Foods.Add(new Food
			{
				ID = 3,
				Name = "กระเพราหมู เผ็ดมาก",
				Location = "ตะลิวปลิว",
				Details = "อร่อยสุดๆ ไปเลยจ้า. ใส่พริก 8 เม็ด",
				ImageUrl = "food1.jpg",
				CostPerUnit = 30,
				Quantity = 0

				//	ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
			});


			Foods.Add(new Food
			{
				ID = 4,
				Name = "กระเพราหมูตุ๋น เผ็ดน้อย",
				Location = "ตะลิวปลิว",
				Details = "อร่อยสุดๆ ไปเลยจ้า. ใส่พริกเม็ดเดียว",
				ImageUrl = "food1.jpg",
				CostPerUnit = 30,
				Quantity = 0

				//	ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
			});


			Foods.Add(new Food
			{
				ID = 5,
				Name = "กระเพราหมูตุ๋น เผ็ดกลาง",
				Location = "ตะลิวปลิว",
				Details = "อร่อยสุดๆ ไปเลยจ้า. ใส่พริก 3 เม็ด",
				ImageUrl = "food1.jpg",
				CostPerUnit = 30,
				Quantity = 0

				//	ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
			});


			Foods.Add(new Food
			{
				ID = 6,
				Name = "กระเพราหมูตุ๋น เผ็ดมาก",
				Location = "ตะลิวปลิว",
				Details = "อร่อยสุดๆ ไปเลยจ้า. ใส่พริก 8 เม็ด",
				ImageUrl = "food1.jpg",
				CostPerUnit = 30,
				Quantity = 0

				//	ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
			});


		}
	}
}
