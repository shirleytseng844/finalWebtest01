using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webtest01.Models;
using static webtest01.Models.StoreInfo;

namespace webtest01.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult test()
        {
            
            return View();
        }


       
        
        [HttpPost]

        public ActionResult test(string jdata)
        {

            //先借一下
            StoreCarInfoEditOut message = new StoreCarInfoEditOut();
            
            Console.WriteLine(jdata);
            List<SearchStoreCard> store = JsonConvert.DeserializeObject<List<SearchStoreCard>>(jdata);

            if (!string.IsNullOrEmpty(jdata))
            {
                if (Session["search"] != null)
                {
                    Session["search"] = null;
                }
                //List<SearchStoreCard> store = JsonConvert.DeserializeObject<List<SearchStoreCard>>(jdata);
                Console.WriteLine(store);

                //Session["search"] = store;
                message.ResultMsg = "搜尋成功!";
            }

            //名稱一致下，前端的物件轉json字符串，可以利用上方語法轉成物件，若資料量多，尚不知如何轉換成集合，理想狀況是可以轉換成集合，然後用Model的方式帶到前端進行foreach動態新增
            message.ErrMsg = "搜尋失敗";

            return Json(message);
        }

        
        public ActionResult SearchStore(string jdata)
        {   
            SearchStoreCard storeCard = JsonConvert.DeserializeObject<SearchStoreCard>(jdata);
            //首頁按下搜尋按鈕後，串接劉嘉的API，返回的資料經由succsess的函式進行挑轉進這個controller並返回頁面
            return View();
        }
    }
}