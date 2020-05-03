using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using StockOnline.Models;

namespace StockOnline.Helper
{
    public class MaterialFirebaseHelper
    {
        private string ChildName = "MaterialHeader";

        readonly FirebaseClient firebase = new FirebaseClient("https://xamarinfirebase-4f832.firebaseio.com");
        


        public async Task<List<MaterialHeader>> GetAllMaterial()
        {
            try
            {
                return (await firebase
                    .Child(ChildName)
                    .OnceAsync<MaterialHeader>()).Select(item => new MaterialHeader
                    {
                        ID = item.Object.ID,
                        Name = item.Object.Name,
                        NameEn = item.Object.NameEn,
                        ProductID = item.Object.ProductID
       
                    }).ToList();
            }
            catch (Exception ee) {
                string error_str = ee.Message;
                return null;
            }
        }



        public async Task<MaterialHeader> GetMaterialByProductId(string productId)
        {
            try
            {
                var allMaterial = await GetAllMaterial();
                await firebase
                    .Child(ChildName)
                    .OnceAsync<MaterialHeader>();
                return allMaterial.FirstOrDefault(a => a.ProductID == productId);
            }
            catch (Exception ee)
            {
                string error_str = ee.Message;
                return null;
            }
        }






        public async Task<List<Product>> GetAllMaterialNotSet()
        {
                        
            List<Product> listP = await new ProductFirebaseHelper().GetAllProduct();
            try
            {

                
                
                List<MaterialHeader> listM = await GetAllMaterial();

                if (listM != null)
                {
                    for (int i = 0; i < listM.Count; i++)
                    {
                        MaterialHeader mat = listM[i];


                        for (int j = 0; j < listP.Count; j++)
                        {
                            Product p = listP[j];

                            if (p.ID.ToString().Equals(mat.ProductID.ToString()))
                            {
                                listP.Remove(p);
                            }
                        }

                    }
                }
              
            }
            catch (Exception ee)
            {
                string error_str = ee.Message;
                return null;
            }
            return listP;
        }

        public async Task AddMaterialHeader(string name, string nameen, string productID)
        {
            await firebase
                .Child(ChildName)
                .PostAsync(new MaterialHeader() { ID = Guid.NewGuid(), Name = name, NameEn = nameen, ProductID = productID });
        }
        
        public async Task<MaterialHeader> GetMaterial(Guid materialId)
        {
            var allMaterial = await GetAllMaterial();
            await firebase
                .Child(ChildName)
                .OnceAsync<MaterialHeader>();
            return allMaterial.FirstOrDefault(a => a.ID == materialId);
        }
        
        
        public async Task UpdateMaterial(Guid materialId, string name, string nameen, string productID)
        {
            var toUpdateMaterial = (await firebase
                .Child(ChildName)
                .OnceAsync<MaterialHeader>()).FirstOrDefault(a => a.Object.ID == materialId);

            await firebase
                .Child(ChildName)
                .Child(toUpdateMaterial.Key)
                .PutAsync(new MaterialHeader() { ID = materialId, Name = name, NameEn = nameen, ProductID = productID });
        }

        public async Task DeleteMaterial(Guid materialId)
        {
            var toDeleteMaterial = (await firebase
                .Child(ChildName)
                .OnceAsync<MaterialHeader>()).FirstOrDefault(a => a.Object.ID == materialId);
            await firebase.Child(ChildName).Child(toDeleteMaterial.Key).DeleteAsync();
        }
        

    }
}
