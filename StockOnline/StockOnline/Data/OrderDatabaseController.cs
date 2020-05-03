using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;
using System.Threading.Tasks;
using StockOnline.Models;
using StockOnline.Data;


namespace StockOnline.Data
{
    public class OrderDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public OrderDatabaseController() {
            
            database = DependencyService.Get<ISLite>().GetConnection();
            database.CreateTable<Order>();
                       
        }

        public Order GetOrder() {
            lock (locker) {
                if (database.Table<Order>().Count() == 0)
                {
                    return null;
                }
                else {
                    return database.Table<Order>().First();
                }
            }
        }
        public int SaveOrder(Order order) {
            if (order.Id != 0)
            {
                database.Update(order);
                return order.Id;
            }
            else
            {

                return database.Insert(order);
            }

        }



        public int DeleteOrder(int id)
        {
            lock (locker)
            {
                return database.Delete<Order>(id);
            }
        }







    }
}
