using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace StockOnline.Models
{
    public class FoodMini
    {

       // [PrimaryKey, AutoIncrement]
        public Guid ID { get; set; }
        public string Name {get; set;}
        public string NameEn { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        
        public string Details { get; set; }


        public decimal CostPerUnit { get; set; }
        public string CostPerUnitStr { get; set; }

        public int Quantity { get; set; }
        public string QuantityStr { get; set; }

        public int QuequNo { get; set; }

        public DateTime OrderDate { get; set; }

        public string CustomerType { get; set; }

        public string Status { get; set; }

        public string  QuequNoDisplay { get; set; }

        public string LevelSpicy { get; set; }


        //public DateTime OrderDate { get; set; }


        public override string ToString() {
            return Name;
        } 

    }
}
