using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using StockOnline.Models;

namespace StockOnline.Helper
{
    public class FirebaseHelper
    {
        private string ChildName = "Foods";

        readonly FirebaseClient firebase = new FirebaseClient("https://xamarinfirebase-4f832.firebaseio.com/");
  
        

        //date_str format ddmmyyyy
        public async Task<List<FoodMini>> GetAllFood(string date_str)
        {
            string ChildName_str = ChildName + "_" + date_str;
            return (await firebase
                .Child(ChildName_str)
                .OnceAsync<FoodMini>()).Select(item => new FoodMini
                {
                    Name = item.Object.Name,
                    NameEn= item.Object.NameEn,
                    ID = item.Object.ID,
                    Location = item.Object.Location,
                    Quantity = item.Object.Quantity,
                    CostPerUnit = item.Object.CostPerUnit,
                    Details = item.Object.Details,
                    OrderDate = item.Object.OrderDate,
                    QuequNo = item.Object.QuequNo,
                    Status = item.Object.Status,
                    LevelSpicy = item.Object.LevelSpicy
                }).ToList();


        }

        //date_str format ddmmyyyy
        public async Task<List<FoodMini>>  GetAllFood(string date_str, int queueNo)
        {
           List<FoodMini> list2 = new List<FoodMini>();

            var allFoods = await GetAllFood(date_str);
            if (allFoods != null) {
                for (int i = 0; i < allFoods.Count; i++) {
                    allFoods[i].QuantityStr = "จำนวน" + allFoods[i].Quantity + " หน่วย";
                    allFoods[i].CostPerUnitStr = "ราคาต่อหน่วย "  + allFoods[i].CostPerUnit + "";
                    if (allFoods[i].QuequNo == queueNo) {
                        list2.Add(allFoods[i]);
                    }


                }
            
            }

            return list2;

        }
             


        //date_str format ddmmyyyy
        public async Task<List<FoodMini>> GetAllFoodByStatus(string date_str, string status)
        {
            List<FoodMini> list2 = new List<FoodMini>();

            var allFoods = await GetAllFood(date_str);
            if (allFoods != null)
            {
                for (int i = 0; i < allFoods.Count; i++)
                {
                    allFoods[i].QuantityStr = "จำนวน" + allFoods[i].Quantity + " หน่วย";
                    allFoods[i].CostPerUnitStr = "ราคาต่อหน่วย " + allFoods[i].CostPerUnit + "";
                    allFoods[i].QuequNoDisplay = "Queue No "  + allFoods[i].QuequNo + "";
                    if (allFoods[i].Status == status)
                    {
                        list2.Add(allFoods[i]);
                    }


                }

            }

            return list2;

        }


        //date_str format ddmmyyyy
        public async Task<List<FoodMini>> GetAllFoodByNotStatusStr(string date_str, string status1, string status2)
        {
            List<FoodMini> list2 = new List<FoodMini>();

            var allFoods = await GetAllFood(date_str);
            if (allFoods != null)
            {
                for (int i = 0; i < allFoods.Count; i++)
                {
                    allFoods[i].QuantityStr = "จำนวน" + allFoods[i].Quantity + " หน่วย";
                    allFoods[i].CostPerUnitStr = "ราคาต่อหน่วย " + allFoods[i].CostPerUnit + "";
                    allFoods[i].QuequNoDisplay = "Queue No " + allFoods[i].QuequNo + "";
                    if ((allFoods[i].Status != status1 || allFoods[i].Status != status2))
                    {
                        list2.Add(allFoods[i]);
                    }


                }

            }

            return list2;

        }


        //date_str format ddmmyyyy
        public async Task<List<FoodMini>> GetAllSumFoodByNotStatusStr(string date_str, string status1, string status2)
        {
            List<FoodMini> list2 = new List<FoodMini>();

            ProductFirebaseHelper productFirebaseHelper = new ProductFirebaseHelper();
            List<Product> listProduct  = await productFirebaseHelper.GetAllProduct();
            var allFoods = await GetAllFood(date_str);
            if (allFoods != null && listProduct != null)
            {
                int totalQ = 0;
                for (int k = 0; k < listProduct.Count; k++)
                {
                    totalQ = 0;
                    Models.Product pro = listProduct[k];
                    for (int i = 0; i < allFoods.Count; i++)
                    {
                        FoodMini fMini = allFoods[i];
                        if (fMini.NameEn != null && fMini.NameEn.Equals(pro.NameEn)) {
                            totalQ = totalQ + fMini.Quantity;
                        }
                    }                    
                    FoodMini resultFMini = new FoodMini();
                    resultFMini.Name = pro.Name;
                    resultFMini.NameEn = pro.NameEn;
                    resultFMini.Quantity = totalQ;
                    resultFMini.QuantityStr = "จำนวนทั้งหมด " + totalQ;
                    list2.Add(resultFMini);

                }
                


            }

            return list2;

        }




        //date_str format ddmmyyyy
        public async Task<List<FoodMini>> GetAllFood(string date_str, int queueNo, string status)
        {
            List<FoodMini> list2 = new List<FoodMini>();

            var allFoods = await GetAllFood(date_str);
            if (allFoods != null)
            {
                for (int i = 0; i < allFoods.Count; i++)
                {
                    allFoods[i].QuantityStr = "จำนวน" + allFoods[i].Quantity + " หน่วย";
                    allFoods[i].CostPerUnitStr = "ราคาต่อหน่วย " + allFoods[i].CostPerUnit + "";
                    if (allFoods[i].QuequNo == queueNo && allFoods[i].Status==status)
                    {
                        list2.Add(allFoods[i]);
                    }


                }

            }

            return list2;

        }



        /*

        public async Task AddFood(string name, string location, string detail, int quantity, DateTime orderDate, int queqeNo, String customerType,String status, Decimal costPerUnit,   String date_str)
        {
            string ChildName_str = ChildName + "_" + date_str;
            await firebase
                .Child(ChildName_str)
                .PostAsync(new FoodMini() { ID = Guid.NewGuid(), Name = name, Location = location, Details = detail, Quantity = quantity, QuequNo = queqeNo, OrderDate = orderDate, Status = status, CostPerUnit=costPerUnit,  CustomerType= customerType });
        }
        */


        public async Task AddFood(string name, string nameEn, string location, string detail, int quantity, DateTime orderDate, string status, int queqeNo, Decimal costPerUnit, string customerType, String date_str, string levelSpicy)
        {

            string ChildName_str = ChildName + "_" + date_str;
            await firebase
                .Child(ChildName_str)
                .PostAsync(new FoodMini() { ID = Guid.NewGuid(), Name = name, NameEn=nameEn, Location = location, Details = detail, Quantity = quantity, OrderDate = orderDate, Status=status,  QuequNo = queqeNo, CostPerUnit = costPerUnit, CustomerType = customerType, LevelSpicy= levelSpicy });
        }

      

        public async Task UpdateFood(Guid foodId, string name, string nameEn, string location, string detail, int quantity, DateTime orderDate, int queqeNo,string status, String date_str, string levelSpicy)
        {
            string ChildName_str = ChildName + "_" + date_str;
            var toUpdateFood = (await firebase
                .Child(ChildName_str)
                .OnceAsync<FoodMini>()).FirstOrDefault(a => a.Object.ID == foodId);

            await firebase
                .Child(ChildName_str)
                .Child(toUpdateFood.Key)
                .PutAsync(new FoodMini() { ID = foodId, Name = name, NameEn = nameEn, Location = location, Details = detail, Quantity = quantity, QuequNo = queqeNo, Status = status, OrderDate = orderDate, LevelSpicy = levelSpicy });
        }

        public async Task DeleteFood(Guid foodId, string date_str)
        {
            string ChildName_str = ChildName + "_" + date_str;
            var toDeleteFood = (await firebase
                .Child(ChildName_str)
                .OnceAsync<FoodMini>()).FirstOrDefault(a => a.Object.ID == foodId);
            await firebase.Child(ChildName_str).Child(toDeleteFood.Key).DeleteAsync();
        }
     


    }
}
