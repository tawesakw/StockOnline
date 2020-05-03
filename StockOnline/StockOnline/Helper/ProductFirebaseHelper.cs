using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using StockOnline.Models;

namespace StockOnline.Helper
{
    public class ProductFirebaseHelper
    {
        private string ChildName = "Product";

        readonly FirebaseClient firebase = new FirebaseClient("https://xamarinfirebase-4f832.firebaseio.com");
        


        public async Task<List<Product>> GetAllProduct()
        {
            try
            {
                return (await firebase
                    .Child(ChildName)
                    .OnceAsync<Product>()).Select(item => new Product
                    {
                        Name = item.Object.Name,
                        NameEn = item.Object.NameEn,
                        ID = item.Object.ID,
                        Location = item.Object.Location,
                        Quantity = item.Object.Quantity,
                        CostPerUnit = item.Object.CostPerUnit,
                        Details = item.Object.Details,
                        ProductType = item.Object.ProductType,
                        ImageUrl = item.Object.ImageUrl
                    }).ToList();
            }
            catch (Exception ee) {
                string error_str = ee.Message;
                return null;
            }
        }
                          
        public async Task AddProduct(string name, string nameen, string location, string detail, int quantity, decimal cost, string productType, string imageURL)
        {
            await firebase
                .Child(ChildName)
                .PostAsync(new Product() { ID = Guid.NewGuid(), Name = name, NameEn = nameen, Location = location, Details =  detail, Quantity = quantity, CostPerUnit=cost, ProductType = productType, ImageUrl= imageURL });
        }
        
        public async Task<Product> GetProduct(Guid productId)
        {
            var allProduct = await GetAllProduct();
            await firebase
                .Child(ChildName)
                .OnceAsync<Product>();
            return allProduct.FirstOrDefault(a => a.ID == productId);
        }

        /**
         * Get Product By NameEn
         * 
         */

        public async Task<Product> GetProductByNameEn(string productNameEn)
        {
            var allProduct = await GetAllProduct();
            await firebase
                .Child(ChildName)
                .OnceAsync<Product>();
            return allProduct.FirstOrDefault(a => a.NameEn == productNameEn);
        }


        public async Task UpdateFood(Guid productId, string name, string nameen, string location, string detail, int quantity, decimal cost, string productType, string imageURL)
        {
            var toUpdateFood = (await firebase
                .Child(ChildName)
                .OnceAsync<Product>()).FirstOrDefault(a => a.Object.ID == productId);

            await firebase
                .Child(ChildName)
                .Child(toUpdateFood.Key)
                .PutAsync(new Product() { ID = productId, Name = name, NameEn  = nameen, Location = location, Details = detail, Quantity = quantity, CostPerUnit = cost, ProductType = productType, ImageUrl = imageURL});
        }

        public async Task DeleteProduct(Guid productId)
        {
            var toDeleteProduct = (await firebase
                .Child(ChildName)
                .OnceAsync<Product>()).FirstOrDefault(a => a.Object.ID == productId);
            await firebase.Child(ChildName).Child(toDeleteProduct.Key).DeleteAsync();
        }
        

    }
}
