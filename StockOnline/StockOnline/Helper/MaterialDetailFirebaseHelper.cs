using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using StockOnline.Models;

namespace StockOnline.Helper
{
    public class MaterialDetailFirebaseHelper
    {
        private string ChildName = "MaterialDetail";

        readonly FirebaseClient firebase = new FirebaseClient("https://xamarinfirebase-4f832.firebaseio.com");



        public async Task<List<MaterialDetail>> GetAllMaterialDetail()
        {
            try { 

                
                Utils utils = new Utils();

                return (await firebase
                    .Child(ChildName)
                    .OnceAsync<MaterialDetail>()).Select(item => new MaterialDetail
                    {
                        ID = item.Object.ID,
                        HeaderID = item.Object.HeaderID,
                        MaterialID = item.Object.MaterialID,
                        UnitName = item.Object.UnitName,
                        Quantity = item.Object.Quantity
                      }).ToList();
            }
            catch (Exception ee)
            {
                string error_str = ee.Message;
                return null;
            }
        }




        public async Task<Models.MaterialDetail> GetDetailById(Guid detailId)
        {
            var allProduct = await GetAllMaterialDetail();
            await firebase
                .Child(ChildName)
                .OnceAsync<Models.MaterialDetail>();
            return allProduct.FirstOrDefault(a => a.ID == detailId);
        }


        public async Task<List<MaterialDetail>> GetAllMaterialByHeader(string materialHeaderID)
        {
            try
            {
                List<MaterialDetail> listReturn = new List<MaterialDetail>();
                List<MaterialDetail> listDetail = await GetAllMaterialDetail();

                if (listDetail != null)
                {
                    for (int i = 0; i < listDetail.Count; i++)
                    {
                        MaterialDetail d = listDetail[i];
                        if (d.HeaderID == materialHeaderID)
                        {
                            listReturn.Add(d);
                        }//end if 
                    }//end for
                }
                return listReturn;
            }
            catch (Exception ee) {
                string error_str = ee.Message;
                return null;
            }
        }//end function
                     
        
        public async Task AddMaterialDetail(string headerId, Guid materialID, string unitName, decimal quantity)
        {
            await firebase
                .Child(ChildName)
                .PostAsync(new MaterialDetail() { ID = Guid.NewGuid(), HeaderID=headerId, MaterialID = materialID, UnitName = unitName, Quantity = quantity });
        }
        
        /*
        public async Task<MaterialHeader> GetMaterial(Guid materialId)
        {
            var allMaterial = await GetAllMaterial();
            await firebase
                .Child(ChildName)
                .OnceAsync<MaterialHeader>();
            return allMaterial.FirstOrDefault(a => a.ID == materialId);
        }
        */
        
        public async Task UpdateMaterial(Guid materialGuidId, string headerId, Guid materialID, string unitName, decimal quantity)
        {
            var toUpdateMaterial = (await firebase
                .Child(ChildName)
                .OnceAsync<MaterialDetail>()).FirstOrDefault(a => a.Object.ID == materialGuidId);

            await firebase
                .Child(ChildName)
                .Child(toUpdateMaterial.Key)
                .PutAsync(new MaterialDetail() { ID = materialGuidId, HeaderID = headerId, MaterialID = materialID, UnitName = unitName, Quantity = quantity });
        }

        public async Task DeleteMaterial(Guid materialGuidId)
        {
            var toDeleteMaterial = (await firebase
                .Child(ChildName)
                .OnceAsync<MaterialDetail>()).FirstOrDefault(a => a.Object.ID == materialGuidId);
            await firebase.Child(ChildName).Child(toDeleteMaterial.Key).DeleteAsync();
        }


        public async Task DeleteMaterialByHeaderId(Guid materialGuidId, string headerId)
        {
            var toDeleteMaterial = (await firebase
                .Child(ChildName)
                .OnceAsync<MaterialDetail>()).FirstOrDefault(a => a.Object.ID == materialGuidId);
            await firebase.Child(ChildName).Child(toDeleteMaterial.Key).DeleteAsync();
        }


    }
}
