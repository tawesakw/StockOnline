using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using System.Threading.Tasks;
using StockOnline.Models;
using Xamarin.Forms;

namespace StockOnline.Data
{
    public class UserDatabaseController
    {
        static object locker = new object();
        SQLiteConnection database;

        public UserDatabaseController() {
            database = DependencyService.Get<ISLite>().GetConnection();
            database.CreateTable<Users>();


        
        }

        public Users GetUsers() {
            lock (locker)
            {

                if (database.Table<Users>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Users>().First();
                }
            }    

        }

        public int SaveUser(Users user) {
            lock (locker) {
                if (user.Id != 0)
                {
                    database.Update(user);
                    return user.Id;
                }
                else {

                    return database.Insert(user);
                }
                
                
                
            }
            
        }

        public int DeleteUser(int id) {
            lock (locker) {
                return database.Delete<Users>(id);
           }        
        }



    }
}
