using System;
using System.Collections.Generic;
using System.Text;



using System.Threading.Tasks;
using SQLite;
using StockOnline.Models;

namespace StockOnline.Data
{
    public class FoodDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public FoodDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Food>().Wait();
        }
        
        public Task<List<Food>> GetNotesAsync()
        {
               return _database.Table<Food>().ToListAsync();
            // firebaseHelper.GetAllFood();



        }

        public Task<Food> GetNoteAsync(int id)
        {
            /*
            return _database.Table<Food>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
                            */
            return null;
        }

        public Task<int> SaveNoteAsync(Food note)
        {
            
            if (note.ID != 0)
            {
                return _database.UpdateAsync(note);
            }
            else
            {
                return _database.InsertAsync(note);
            }
            
        }

        public Task<int> DeleteNoteAsync(Food note)
        {
            return _database.DeleteAsync(note);
        }
    }
}
