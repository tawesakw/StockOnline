using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace StockOnline.Models
{
    public class Food
    {

       // [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name {get; set;}

        public string NameEn { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        
        public string Details { get; set; }


        public decimal CostPerUnit { get; set; }

        public int Quantity { get; set; }

        public string Status { get; set; }


        public string LevelSpicy { get; set; }

        //public DateTime OrderDate { get; set; }


        public override string ToString() {
            return Name;
        } 

    }
}
