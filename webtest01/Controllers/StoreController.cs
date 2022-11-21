using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using webtest01.Models;
using static webtest01.Models.StoreInfo;

namespace webtest01.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditStoreBoss()
        {

            if(Session["Store"] != null)
            {
                int id = Convert.ToInt32(Session["Store"]);

                trytryEntities t = new trytryEntities();
                var storeInfo = from x in t.Store
                                where x.StoreID == id
                                select x;
                return View(storeInfo);

            }
            return RedirectToAction("Loging", "Member");
        }
        [HttpPost]
        public ActionResult EditStoreBoss(int id,string Email,string own_name,string Phone)
        {
            StoreBossInfoEditOut outModel = new StoreBossInfoEditOut();
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(own_name) || string.IsNullOrEmpty(Phone))
            {
                outModel.ErrMsg = "欄位不可空白!";
            }
            else
            {
                bool EmailChange = false;
                trytryEntities t = new trytryEntities();
                var StoreBossInfo = from x in t.Store
                              where x.StoreID == id
                              select x;
                if(StoreBossInfo.ElementAt<Store>(0).Email_Account != Email)
                {
                    EmailChange = true;
                }
                foreach(var Info in StoreBossInfo)
                {
                    Info.Owner_Name = own_name;
                    Info.Phone = Phone;
                    Info.Email_Account = Email;
                }
                t.SaveChanges();
                if (EmailChange)
                {
                    outModel.ResultMsg = "資料變更完成! 請重新登入!";
                    outModel.relogin = "relogin";
                }else
                {
                    outModel.ResultMsg = "資料變更完成!";
                }
            }
            return Json(outModel);
        }

        public ActionResult ShowStore(int id)
        {
            trytryEntities t = new trytryEntities();
            
            //int id = Convert.ToInt32(inModel.storeID);
            //var card = from x in t.Store
            //           join o in t.Store_Business on x.StoreID equals o.StoreID into ps
            //           from o in ps.DefaultIfEmpty()
            //           where x.StoreID == id
            //           select new ClickStoreCardOut()   
            //           {
            //               storeID = x.StoreID,
            //               Store_Name = x.Store_Name,
            //               Store_Class = x.Store_Class,
            //               Address_City = x.Address_City,
            //               Address_Local = x.Address_Local,
            //               Phone = x.Phone,
            //               longitude = o.longitude,
            //               latitude = o.latitude,
            //               OnBusiness = (bool)o.OnBusiness,
            //               Picture = x.Picture
            //           };
            var card_days = from x in t.Store
                            join o in t.Store_Business on x.StoreID equals o.StoreID
                            join y in t.Calendar on o.CalendarID equals y.CalendarID into ps
                            from y in ps.DefaultIfEmpty()
                            where x.StoreID == id
                            select new ClickStoreCardOut()
                            {
                                storeID = x.StoreID,
                                CalendarID = y.CalendarID,
                                Store_Name = x.Store_Name,
                                Introduce = x.Introduce,
                                Store_Class = x.Store_Class,
                                Address_City = x.Address_City,
                                Address_Local = x.Address_Local,
                                Phone = x.Phone,
                                longitude = o.longitude,
                                latitude = o.latitude,
                                Picture = x.Picture
                            };
              var comment = from x in t.Message_Board
                            join o in t.Customer on x.CustomerID equals o.CustomerID into py
                            from y in py.DefaultIfEmpty()
                            where x.StoreID == id
                            select new MessageCommentCardOut()
                            {
                                CustomerID = x.CustomerID,
                                Text_Content =x.Text_Content,
                                Star_Rating =(int)x.Star_Rating,
                                Picture=x.Picture,
                                CustomerName = y.Name
                            };
            ViewBag.comment = comment;
            ViewBag.commentcount = comment.Count().ToString();

            return View(card_days);
        }

        public ActionResult EditCarInfo()
        {
            if (Session["Store"] != null)
            {
                int id = Convert.ToInt32(Session["Store"]);

                trytryEntities t = new trytryEntities();
                var storeInfo = from x in t.Store
                                where x.StoreID == id
                                select x;
                return View(storeInfo);

            }
            return RedirectToAction("Loging", "Member");

        }
        [HttpPost]
        public ActionResult EditCarInfo(FormCollection formCollection)
        {
            StoreCarInfoEditOut outModel = new StoreCarInfoEditOut();
            StoreCarInfoEditIn list = new StoreCarInfoEditIn()
            {
                CarName = formCollection.Get("CarName"),
                county = formCollection.Get("county"),
                district = formCollection.Get("district"),
                address = formCollection.Get("address")
                //Introduce = formCollection.Get("Introduce")

            };
            int storeID = Convert.ToInt32(formCollection.Get("StoreID"));

            string fullFilePath = "";
            string newFileName = "";
            string fileSavedPath = "";

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase httpPostedFileBase = Request.Files[0];
                if (string.IsNullOrEmpty(list.CarName) || string.IsNullOrEmpty(list.county) || string.IsNullOrEmpty(list.address))
                {
                    outModel.ErrMsg = "不可有空白欄位!";
                }
                else
                {
                    trytryEntities t = new trytryEntities();
                    var x = from store in t.Store
                            where store.StoreID == storeID
                            select store;

                    if (httpPostedFileBase != null)
                    {
                        if (Request.Files["imageFile"].ContentLength > 0)
                        {
                            string extension = Path.GetExtension(httpPostedFileBase.FileName);

                            if (extension == ".jpg" || extension == ".png")
                            {
                                fileSavedPath = WebConfigurationManager.AppSettings["StoreUploadPath"];
                                newFileName = x.ElementAt<Store>(0).Email_Account + string.Concat(DateTime.Now.ToString("yyyy-MM-ddHH-mm-ss"), extension.ToLower());
                                fullFilePath = Path.Combine(Server.MapPath(fileSavedPath), newFileName);
                                Request.Files["imageFile"].SaveAs(fullFilePath);
                                Response.Write("<script language=javascript> alert('檔案上傳成功');</" + "script>");
                            }
                            else
                            {
                                Response.Write("<script language=javascript> alert('請上傳.jpg 或 .png格式的檔案');</" + "script>");
                            }

                        }
                        else
                        {
                            Response.Write("<script language=javascript> alert('請重新選擇檔案');</" + "script>");
                        }
                    }

                    //記得加上判斷，不然誤按enter會爆炸
                    fileSavedPath = fileSavedPath.Replace("~", "");
                    foreach(var item in x)
                    {
                        item.Store_Name = list.CarName;
                        //Store_Class = list.Store_class;
                        //Introduce = list.Introduce;
                        item.Address_Area = list.district;
                        item.Address_City = list.county;
                        item.Address_Local = list.address;
                        item.Picture = fileSavedPath + "/" + newFileName;
                    };

                    try
                    {
                        t.SaveChanges();
                        outModel.ResultMsg = "資料更新完成";
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    
                }
            }
            else
            {
                if (string.IsNullOrEmpty(list.CarName) || string.IsNullOrEmpty(list.county) || string.IsNullOrEmpty(list.address))
                {
                    outModel.ErrMsg = "不可有空白欄位!";
                }
                else
                {
                    trytryEntities t = new trytryEntities();
                    var x = from store in t.Store
                            where store.StoreID == storeID
                            select store;


                    //記得加上判斷，不然誤按enter會爆炸
                    fileSavedPath = fileSavedPath.Replace("~", "");
                    foreach (var item in x)
                    {
                        item.Store_Name = list.CarName;
                        //Store_Class = list.Store_class;
                        //Introduce = list.Introduce;
                        item.Address_Area = list.district;
                        item.Address_City = list.county;
                        item.Address_Local = list.address;
                    };

                    try
                    {
                        t.SaveChanges();
                        outModel.ResultMsg = "資料更新完成";
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }




            return Json(outModel);
        }

        //public ActionResult ShowStoreInfo_page(ClickStoreCardIn inModel)
        //{
        //    //點擊後傳入商店ID，輸出商店的資訊
        //    trytryEntities t = new trytryEntities();
        //    int id = Convert.ToInt32(inModel.storeID);
        //    var card = from x in t.Store
        //               join o in t.Store_Business on x.StoreID equals o.StoreID into ps from o in ps.DefaultIfEmpty()
        //               where x.StoreID == id
        //               select new {
        //                   storeID = x.StoreID,
        //                   Store_Name =x.Store_Name,
        //                   Store_Class =x.Store_Class,
        //                   Address_City =x.Address_City,
        //                   Address_Local =x.Address_Local,
        //                   Phone =x.Phone,
        //                   longitude =o.longitude,
        //                   latitude =o.latitude,
        //                   OnBusiness =o.OnBusiness,
        //                   Picture =x.Picture};
        //    //        ClickStoreCardOut store = new ClickStoreCardOut()
        //    //        {
        //    //            storeID =card.FirstOrDefault().x.StoreID;
        //    //            Store_Name = card.FirstOrDefault().x.Store_Name;
        //    //            Store_Class =;
        //    //            Address_City =;
        //    //            Address_Local =;
        //    //            Phone =;
        //    //            longitude =;
        //    //            latitude =;
        //    //            OnBusiness =;
        //    //            Picture =;
        //    //}
        //    //EF間因導覽連結導致的參考迴圈，
        //    //解法1.用Ignore屬性忽略，但會導致傳出的資料過多(如下方)
        //    //解法2.參考https://blog.miniasp.com/post/2012/12/24/ASPNET-Web-API-Self-referencing-loop-detected-for-property-solutions的4種方法
        //    //解法3.直接使用上面的方式，一個對一個先把需要的資料抓好，輸出陣列後用JSON轉換，到網頁後再foreach抓資料
        //    //var json = JsonConvert.SerializeObject(b, 
        //    //    Formatting.Indented, new JsonSerializerSettings { 
        //    //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //    //    }
        //    //);

        //    return Json(card);
        //}

        //public ActionResult ShowStoreInfo(SearchModel item)
        //{
        //    //搜尋方法
        //    string[] time = null;
        //    string startTime = "";
        //    string endTime = "";
        //    if (item.time != "營業時間")
        //    {  
        //    //轉換時間格式為 2022/9/5 06:00 - 2022/9/5 12:00
        //    time = item.time.Split('-');
        //    startTime = DateTime.Today.ToString()+" "+time[0];
        //    endTime = DateTime.Today.ToString() + " " + time[1];
        //    }

        //    string FoodClass = "";
        //    if(item.FoodClass != "食物類別")
        //    {
        //        FoodClass = item.FoodClass;
        //    }
        //    string location = "";
        //    if(item.location != "地點")
        //    {
        //        location = item.location;
        //    }
        //    string KeyWord = "";
        //    if(item.KeyWord != "")
        //    {
        //        KeyWord = item.KeyWord;
        //    }

        //    trytryEntities t = new trytryEntities();
        //    if(time == null || FoodClass == "" || location == "")
        //    {
        //        //outMsg.err = "請完整填入搜尋資訊!";
        //    }
        //    else if (KeyWord == "")
        //    {
        //        //讀取資料庫 抓取Business的資料並left join store的資料，並進行複合搜尋(因為字串原因，故轉int去比較)
        //        var Search = from x in t.Store_Business
        //                     join o in t.Store on x.StoreID equals o.StoreID into ps from o in ps.DefaultIfEmpty()
        //                     where (x.Address_City == location && o.Store_Class == FoodClass && (Convert.ToInt32(x.Punch_Start.Replace("/", "").Replace(" ", "").Replace(":", "")) > Convert.ToInt32(startTime.Replace("/", "").Replace(" ", "").Replace(":", "")) && Convert.ToInt32(x.Punch_End.Replace("/", "").Replace(" ", "").Replace(":", "")) < Convert.ToInt32(startTime.Replace("/", "").Replace(" ", "").Replace(":", ""))))
        //                     select new { x, o.Store_Name ,o.Store_Class};
        //    }
        //    else
        //    {   //延續上方，增添關鍵字搜尋內容
        //        var Search = from x in t.Store_Business
        //                     join o in t.Store on x.StoreID equals o.StoreID into ps
        //                     from o in ps.DefaultIfEmpty()
        //                     where (x.Address_City == location && o.Store_Class == FoodClass && (Convert.ToInt32(x.Punch_Start.Replace("/", "").Replace(" ", "").Replace(":", "")) > Convert.ToInt32(startTime.Replace("/", "").Replace(" ", "").Replace(":", "")) && Convert.ToInt32(x.Punch_End.Replace("/", "").Replace(" ", "").Replace(":", "")) < Convert.ToInt32(startTime.Replace("/", "").Replace(" ", "").Replace(":", ""))))
        //                     select new { x, o.Store_Name, o.Store_Class };
        //    }

        //    return View();
        //}


    }
}