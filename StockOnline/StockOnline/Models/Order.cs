using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using System.Linq;

namespace StockOnline.Models
{
    public class Order
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }

        public int QNo { get; set; }
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }

        public decimal CostPerUnit { get; set; }

        public string Description { get; set; }

        public DateTime OrderDate { get; set; }

    }
}
