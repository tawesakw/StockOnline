using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using StockOnline.Models;

namespace StockOnline.Helper
{
    public class UserFirebaseHelper
    {
        private string ChildName = "User";

        readonly FirebaseClient firebase = new FirebaseClient("https://xamarinfirebase-4f832.firebaseio.com");
        /*
        public async Task<List<Person>> GetAllPersons()
        {
            return (await firebase
                .Child(ChildName)
                .OnceAsync<Person>()).Select(item => new Person
                {
                    Name = item.Object.Name,
                    PersonId = item.Object.PersonId,
                    Phone = item.Object.Phone
                }).ToList();
        }

        public async Task AddPerson(string name, string phone)
        {
            await firebase
                .Child(ChildName)
                .PostAsync(new Person() { PersonId = Guid.NewGuid(), Name = name, Phone = phone });
        }

        public async Task<Person> GetPerson(Guid personId)
        {
            var allPersons = await GetAllPersons();
            await firebase
                .Child(ChildName)
                .OnceAsync<Person>();
            return allPersons.FirstOrDefault(a => a.PersonId == personId);
        }

        public async Task<Person> GetPerson(string name)
        {
            var allPersons = await GetAllPersons();
            await firebase
                .Child(ChildName)
                .OnceAsync<Person>();
            return allPersons.FirstOrDefault(a => a.Name == name);
        }

        public async Task UpdatePerson(Guid personId, string name, string phone)
        {
            var toUpdatePerson = (await firebase
                .Child(ChildName)
                .OnceAsync<Person>()).FirstOrDefault(a => a.Object.PersonId == personId);

            await firebase
                .Child(ChildName)
                .Child(toUpdatePerson.Key)
                .PutAsync(new Person() { PersonId = personId, Name = name, Phone = phone });
        }

        public async Task DeletePerson(Guid personId)
        {
            var toDeletePerson = (await firebase
                .Child(ChildName)
                .OnceAsync<Person>()).FirstOrDefault(a => a.Object.PersonId == personId);
            await firebase.Child(ChildName).Child(toDeletePerson.Key).DeleteAsync();
        }

    */


        public async Task<List<User>> GetAllUser()
        {
            try
            {
                return (await firebase
                    .Child(ChildName)
                    .OnceAsync<User>()).Select(item => new User
                    {
                        UserName = item.Object.UserName,
                        ID = item.Object.ID,
                        Password = item.Object.Password,
                        Description = item.Object.Description,
                        Level = item.Object.Level
                    }).ToList();
            }
            catch (Exception ee) {
                string error_str = ee.Message;
                return null;
            }
        }
                          
        public async Task AddUser(string name, string passwd, string description, string level)
        {
            await firebase
                .Child(ChildName)
                .PostAsync(new User() { ID = Guid.NewGuid(), UserName = name, Password = passwd, Description = description, Level = level });
        }
        
        public async Task<User> GetUser(Guid userId)
        {
            var allUser = await GetAllUser();
            await firebase
                .Child(ChildName)
                .OnceAsync<User>();
            return allUser.FirstOrDefault(a => a.ID == userId);
        }



        public async Task<User> GetUserPassword(string UserName)
        {
            var allUser = await GetAllUser();
            await firebase
                .Child(ChildName)
                .OnceAsync<User>();
            return allUser.FirstOrDefault(a => a.UserName == UserName);
        }

        public async Task UpdateUser(Guid userId, string name, string passwd, string description, string level)
        {
            var toUpdateFood = (await firebase
                .Child(ChildName)
                .OnceAsync<User>()).FirstOrDefault(a => a.Object.ID == userId);

            await firebase
                .Child(ChildName)
                .Child(toUpdateFood.Key)
                .PutAsync(new User() { ID = userId, UserName = name, Password = passwd, Description = description, Level = level});
        }

        public async Task DeleteUser(Guid userId)
        {
            var toDeleteUser = (await firebase
                .Child(ChildName)
                .OnceAsync<User>()).FirstOrDefault(a => a.Object.ID == userId);
            await firebase.Child(ChildName).Child(toDeleteUser.Key).DeleteAsync();
        }
        

    }
}
