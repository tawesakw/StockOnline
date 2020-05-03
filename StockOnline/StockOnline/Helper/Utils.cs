using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockOnline.Helper
{
    public class Utils
    {
        //Convert from YYYYMMDD to  DD/MM/YYYY
        public string convertYYYYMMDD(string date1) {
            string value = "";
            if (date1 != null && date1.Length == 8) {
                value = date1.Substring(6, 2) + "/" + date1.Substring(4, 2) + "/" + date1.Substring(0, 4);
            }
            return value;


        }

        public string convertObject(Object o)
        {
            if (o == null) {
                return "";
            }else{
                return o.ToString().Trim();
            }
        }
        public async System.Threading.Tasks.Task<string> adjustRemainStockAsync(string foodName)
        {

            string alertMessage = "";

            ProductFirebaseHelper productFirebase = new ProductFirebaseHelper();
            MaterialFirebaseHelper materialFirebaseHelper = new MaterialFirebaseHelper();
            MaterialDetailFirebaseHelper detailFirebaseHelper = new MaterialDetailFirebaseHelper();
            MaterialSourceFirebaseHelper sourceFirebaseHelper = new MaterialSourceFirebaseHelper();
            Models.Product pro = await productFirebase.GetProductByNameEn(foodName);
            if (pro != null)
            {
                Models.MaterialHeader header = await materialFirebaseHelper.GetMaterialByProductId(pro.ID.ToString());
                List<Models.MaterialDetail>  listDetail = await detailFirebaseHelper.GetAllMaterialByHeader(convertObject(header.ID));
                for (int i = 0; i < listDetail.Count; i++) {
                    Models.MaterialDetail detail = listDetail[i];
                    Models.MaterialSource source = await sourceFirebaseHelper.GetMaterialSourceByID(convertObject(detail.MaterialID));
                    source.Quantity = source.Quantity - detail.Quantity;
                    if (source.Quantity < 0)
                    {
                        if (alertMessage.Trim().Length == 0)
                        {
                            alertMessage = "สินค่า " + foodName + " วัตุดิบ " + source.NameEn + " หมด";
                        }
                        else
                        {
                            alertMessage = alertMessage + " วัตุดิบ " + source.NameEn + " หมด";
                        }
                    }
                    else if (source.Quantity <= source.QuantityAlert) {
                        if (alertMessage.Trim().Length == 0)
                        {
                            alertMessage = "สินค่า " + foodName + " วัตุดิบ " + source.NameEn + " เหลือน้อยกว่าที่กำหนดไว้" + source.QuantityAlert;
                        }
                        else
                        {
                            alertMessage = alertMessage + " วัตุดิบ " + source.NameEn + " เหลือน้อยกว่าที่กำหนดไว้ " + source.QuantityAlert;
                        }


                    }
                }


            }
            return alertMessage;


        }

        public async Task<string> getMaterialNameAsync(Guid mat_id) {
            string sourceName = "";
            try
            {
                MaterialSourceFirebaseHelper sourceFire = new MaterialSourceFirebaseHelper();
                Models.MaterialSource source = await sourceFire.GetMaterialSourceByID(convertObject(mat_id));

                sourceName = source.NameEn;
            }
            catch (Exception ee) {
                sourceName = "";
            }
            return sourceName;
        
        }
        MaterialSourceFirebaseHelper source = new MaterialSourceFirebaseHelper();

    }//end class


 }//end namespace
