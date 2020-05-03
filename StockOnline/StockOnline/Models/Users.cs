using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockOnline.Models
{
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username {get; set; }
          
        public string Password {get; set;}
    public Users() { 
    
    }
    public Users(String Username, String Password) {
            this.Username = Username;
            this.Password = Password;
    }

        public bool checkInformation() {
            if (! this.Username.Equals("") && ! this.Username.Equals(""))
            {
                return true;
            }
            else {
                return false;
            }


        }
}
}
