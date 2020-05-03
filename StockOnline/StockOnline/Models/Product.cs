using System;
using System.Collections.Generic;
using System.Text;

namespace StockOnline.Models
{
    public class Product
    {

        // [PrimaryKey, AutoIncrement]

        public Guid ID { get; set; }
        public string Name { get; set; }

        public string NameEn { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }

        public string Details { get; set; }


        public decimal CostPerUnit { get; set; }

        public int Quantity { get; set; }

        public string ProductType { get; set; }

        public string LevelSpicy { get; set; }

    }
}
