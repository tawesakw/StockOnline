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
    public class TokenDatabaseController
    {

        static object locker = new object();
        SQLiteConnection database;

        public TokenDatabaseController()
        {
            database = DependencyService.Get<ISLite>().GetConnection();
            database.CreateTable<Token>();



        }

        public Token GetToken()
        {
            lock (locker)
            {

                if (database.Table<Token>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Token>().First();
                }
            }

        }

        public int SaveToken(Token token)
        {
            lock (locker)
            {
                if (token.Id != 0)
                {
                    database.Update(token);
                    return token.Id;
                }
                else
                {

                    return database.Insert(token);
                }



            }

        }

        public int DeleteToken(int id)
        {
            lock (locker)
            {

                return database.Delete<TokenDatabaseController>(id);



            }


        }


    }
}
