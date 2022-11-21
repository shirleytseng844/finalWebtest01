using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webtest01.Models;
using System.IO;
using System.Web.Configuration;

namespace webtest01.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        //商品展列
        public ActionResult Product_Customer(int ?id)
        {
            if(id != null)
            {
                trytryEntities t = new trytryEntities();
                var product = from x in t.Store_Products
                              where x.StoreID == id
                              select x;
                return View(product);
            }

            return RedirectToAction("Index","Home");
        }

        public ActionResult Product_Store()
        {

            //Session["Store"] = 10;
            if ( Session["Store"] == null)
            {   
                return RedirectToAction("Login","Member");
            }
            else
            {
                int StoreID = Convert.ToInt32(Session["Store"]);
                trytryEntities t = new trytryEntities();
                var product = from p in t.Store_Products
                              where p.StoreID == StoreID
                              select p;
                ////轉換型態
                //var list = product.Select(p => new ProductInfoOut()
                //{
                //    ProductID = p.ProductID,
                //    Product_Name = p.Product_Name,
                //    product_Price = (int)p.Product_Price
                //}).ToList();                
                //return View(list);

                return View(product);
            };
        }
        //新增產品(頁面伴有檔案上傳功能)
        public ActionResult addNewProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addNewProduct(string Product_name,string Product_price, HttpPostedFileBase file)
        {
            //Session["Store"] = 10;
            //上傳檔案部分
            string fullFilePath = "";
            string newFileName = "";
            string fileSavedPath = "";
            if (file != null)
            {
                if (Request.Files["file"].ContentLength > 0)
                {


                    string extension = Path.GetExtension(file.FileName);
                    
                    if (extension == ".jpg" || extension == ".png")
                    {
                        fileSavedPath = WebConfigurationManager.AppSettings["ProductUploadPath"];
                        newFileName = Session["Store"] + string.Concat(DateTime.Now.ToString("yyyy-MM-ddHH-mm-ss"), extension.ToLower());
                        fullFilePath = Path.Combine(Server.MapPath(fileSavedPath), newFileName);
                        Request.Files["file"].SaveAs(fullFilePath);
                        
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
            int ID = Convert.ToInt32(Session["Store"]);
            fileSavedPath = fileSavedPath.Replace("~", "");
            trytryEntities t = new trytryEntities();
            Store_Products product = new Store_Products()
            {
                StoreID = ID,
                Product_Name = Product_name,
                Product_Price = Convert.ToInt32(Product_price),
                Product_Picture = fileSavedPath + "/" + newFileName

                //Product_Image = fullFilePath 待資料庫修正後加入，且上方需,
            };
            t.Store_Products.Add(product);
            t.SaveChanges();


            return addNewProduct();
        }


        //修改
        public ActionResult ReviseProuduct(int id)
        {
            if(id != 0)
            {
                trytryEntities t = new trytryEntities();
                var x = from product in t.Store_Products
                        where product.ProductID == id
                        select product;
                return View(x);

            }

            return View();
        }
        [HttpPost]
        public ActionResult ReviseProduct(int id,string Product_name, string Product_price, HttpPostedFileBase file)
        {
            string fullFilePath = "";
            string newFileName = "";
            string fileSavedPath = "";
            if (file != null)
            {
                if (Request.Files["file"].ContentLength > 0)
                {
                    string extension = Path.GetExtension(file.FileName);

                    if (extension == ".jpg" || extension == ".png")
                    {
                        fileSavedPath = WebConfigurationManager.AppSettings["ProductUploadPath"];
                        newFileName = Session["Store"] + string.Concat(DateTime.Now.ToString("yyyy-MM-ddHH-mm-ss"), extension.ToLower());
                        fullFilePath = Path.Combine(Server.MapPath(fileSavedPath), newFileName);
                        Request.Files["file"].SaveAs(fullFilePath);
                        ViewBag.path = fullFilePath;
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
            int ID = Convert.ToInt32(Session["Store"]);
            fileSavedPath = fileSavedPath.Replace("~", "");
            trytryEntities t = new trytryEntities();
            var pro = from x in t.Store_Products
                      where x.ProductID == id
                      select x;
            if (newFileName == ""| fileSavedPath == "")
            {
                foreach(Store_Products newpro in pro)
                {
                    newpro.Product_Name = Product_name;
                    newpro.Product_Price = Convert.ToInt32(Product_price);
                }
                t.SaveChanges();
            }
            else
            {
                foreach (Store_Products newpro in pro)
                {
                    newpro.Product_Name = Product_name;
                    newpro.Product_Price = Convert.ToInt32(Product_price);
                    newpro.Product_Picture = fileSavedPath + "/" + newFileName;
                }
                t.SaveChanges();
            }
            return Redirect("~/Product/Product_Store");
        }

        //刪除
        public ActionResult DeleteProuduct(int id)
        {
            trytryEntities t = new trytryEntities();
            var pro = from product in t.Store_Products
                      where product.ProductID == id
                      select product;
            
            foreach(var detail in pro)
            {
                t.Store_Products.Remove(detail);
            }
            t.SaveChanges();


            return Redirect("~/Product/Product_Store");
        }
    }
}