using System;
using System.Collections.Generic;
using System.Text;

namespace StockOnline.Models
{
    public class MaterialDetail
    {

        // [PrimaryKey, AutoIncrement]

        public Guid ID { get; set; }
        public string HeaderID { get; set; }

        public Guid MaterialID { get; set; }
        
        public string UnitName { get; set; }

        public decimal Quantity { get; set; }

        public decimal QuantityStr { get; set; }



        public String MaterialNameDisplay { get; set; }

    }
}
