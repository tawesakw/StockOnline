using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using StockOnline.Models;

namespace StockOnline.Helper
{
    public class MaterialSourceFirebaseHelper
    {
        private string ChildName = "MaterialSource";

        readonly FirebaseClient firebase = new FirebaseClient("https://xamarinfirebase-4f832.firebaseio.com");

        public async Task<List<MaterialSource>> GetAllMaterialSource()
        {
            try
            {
                return (await firebase
                    .Child(ChildName)
                    .OnceAsync<MaterialSource>()).Select(item => new MaterialSource
                    {
                        Name = item.Object.Name,
                        NameEn = item.Object.NameEn,
                        ID = item.Object.ID,
                        Quantity = item.Object.Quantity,
                        QuantityAlert = item.Object.QuantityAlert,
                        UnitName = item.Object.UnitName,
                        Remark = item.Object.Remark,
                        QuantityStr = "จำนวนทั้งหมด " + item.Object.Quantity + "  หน่วย" 

                    }).ToList();
            }
            catch (Exception ee)
            {
                string error_str = ee.Message;
                return null;
            }
        }


        public async Task<MaterialSource> GetMaterialSourceByNameEn(string nameEn)
        {
            MaterialSource returnValue = new MaterialSource();
            try
            {
               
                List<MaterialSource> listSource = await GetAllMaterialSource();
                if (listSource != null)
                {
                    for (int i = 0; i < listSource.Count; i++)
                    {
                        MaterialSource source = listSource[i];
                        if (source.NameEn != null && source.NameEn.Equals(nameEn))
                        {
                            returnValue = source;
                            break;
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                string error_str = ee.Message;
                returnValue = null;
            }

            return returnValue;
        }



        public async Task<Models.MaterialSource> GetMaterialSourceByID(string gID)
        {
            MaterialSource returnValue = new MaterialSource();
            Utils utils = new Utils();
            try
            {

                List<MaterialSource> listSource = await GetAllMaterialSource();
                if (listSource != null)
                {
                    for (int i = 0; i < listSource.Count; i++)
                    {
                        MaterialSource source = listSource[i];
                        if (source.ID != null && utils.convertObject(source.ID).Equals(gID))
                        {
                            returnValue = source;
                            break;
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                string error_str = ee.Message;
                returnValue = null;
            }

            return returnValue;
        }

        public async Task AddMaterialSource(string name, string nameen, string unitName, decimal quantity, decimal quantityAlert, string remark)
        {
            await firebase
                .Child(ChildName)
                .PostAsync(new MaterialSource() { ID = Guid.NewGuid(), Name = name, NameEn = nameen, UnitName = unitName, Quantity = quantity, QuantityAlert = quantityAlert, Remark = remark });
        }

        public async Task<MaterialSource> GetMaterialSource(Guid sourceId)
        {
            var allMaterialSource = await GetAllMaterialSource();
            await firebase
                .Child(ChildName)
                .OnceAsync<MaterialSource>();
            return allMaterialSource.FirstOrDefault(a => a.ID == sourceId);
        }
        /*
        public async Task<MaterialSource> GetMaterialSourceByNameEn(string sourceName)
        {
            var allMaterialSource = await GetAllMaterialSource();
            await firebase
                .Child(ChildName)
                .OnceAsync<MaterialSource>();
            return allMaterialSource.FirstOrDefault(a => a.NameEn == sourceName);
        }
        */

        public async Task UpdateMaterialSource(Guid sourceId, string name, string nameen, string unitName, decimal quantity, decimal quantityAlert, string remark)
        {
            var toUpdateFood = (await firebase
                .Child(ChildName)
                .OnceAsync<MaterialSource>()).FirstOrDefault(a => a.Object.ID == sourceId);

            await firebase
                .Child(ChildName)
                .Child(toUpdateFood.Key)
                .PutAsync(new MaterialSource() { ID = sourceId, Name = name, NameEn = nameen, UnitName = unitName, Quantity = quantity, QuantityAlert = quantityAlert, Remark = remark });
        }

        public async Task DeleteMaterialSource(Guid sourceId)
        {
            var toDeleteProduct = (await firebase
                .Child(ChildName)
                .OnceAsync<MaterialSource>()).FirstOrDefault(a => a.Object.ID == sourceId);
            await firebase.Child(ChildName).Child(toDeleteProduct.Key).DeleteAsync();
        }


    }
}
