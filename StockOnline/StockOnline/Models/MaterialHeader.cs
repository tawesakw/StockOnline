using System;
using System.Collections.Generic;
using System.Text;

namespace StockOnline.Models
{
    public class MaterialHeader
    {

        // [PrimaryKey, AutoIncrement]

        public Guid ID { get; set; }
        public string ProductID { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }


    }
}
