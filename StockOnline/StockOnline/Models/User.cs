using System;
using System.Collections.Generic;
using System.Text;

namespace StockOnline.Models
{
    public class User
    {

        // [PrimaryKey, AutoIncrement]
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Level { get; set; }

        public string Description { get; set; }
    }
}
