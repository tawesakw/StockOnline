using System;
using System.Collections.Generic;
using System.Text;

namespace StockOnline.Models
{
    public class MaterialSource
    {

        // [PrimaryKey, AutoIncrement]

        public Guid ID { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }

        public string UnitName { get; set; }

        public decimal Quantity { get; set; }

        public decimal QuantityAlert { get; set; }

        public string QuantityStr { get; set; }

        public string Remark { get; set; }

    }
}
