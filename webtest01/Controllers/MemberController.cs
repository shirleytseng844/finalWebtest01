using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using webtest01.Models;

namespace webtest01.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisterIdentity()
        {
            return View();
        }
        //一般會員註冊
        public ActionResult DoRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoRegister(FormCollection formCollection)
        {
            DoRegisterOut outModel = new DoRegisterOut();
            DoRegisterIn list = new DoRegisterIn()
            {
                UserAccount = formCollection.Get("Email"),
                UserPwd = formCollection.Get("Pwd"),
                Name = formCollection.Get("Name"),
                Phone = formCollection.Get("Phone"),
                county = formCollection.Get("county"),
                district = formCollection.Get("district"),
                address = formCollection.Get("address"),
            };
            
            if (string.IsNullOrEmpty(list.UserAccount) || string.IsNullOrEmpty(list.UserPwd) || string.IsNullOrEmpty(list.Name) || string.IsNullOrEmpty(list.Phone) || string.IsNullOrEmpty(list.county))
            {
                    outModel.ErrMsg = "請完整輸入資料";
            }
            else
            {
                trytryEntities t = new trytryEntities();
                var y = from customer in t.Customer
                        where customer.Email_Account == list.UserAccount
                        select customer;
                if (y.Any())
                {
                    outModel.ErrMsg = "帳號已存在";
                }
                else
                {
                    //記得加上判斷，不然誤按enter會爆炸

                    trytryEntities db = new trytryEntities();
                    Customer customerInfo = new Customer()
                    {
                        Name = list.Name,
                        Phone = list.Phone,
                        Email_Account = list.UserAccount,
                        Password = list.UserPwd,
                        Address_City = list.county,
                        Address_Local = list.district,
                        Address_Road = list.address,
                        Created_At = DateTime.Now
                    };
                    try
                    {
                        db.Customer.Add(customerInfo);
                        db.SaveChanges();
                        outModel.ResultMsg = "使用者註冊完成";
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            return Json(outModel);
        }

        //店家註冊
        public ActionResult DoRegisterStore()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoRegisterStore(FormCollection formCollection)
        {
            DoRegisterStoreOut outModel = new DoRegisterStoreOut();
            DoRegisterStoreIn list = new DoRegisterStoreIn() {
            CarName = formCollection.Get("CarName"),
            Name = formCollection.Get("Name"),
            Phone = formCollection.Get("Phone"),
            Email = formCollection.Get("Email"),
            Pwd = formCollection.Get("Pwd"),
            county = formCollection.Get("county"),
            district = formCollection.Get("district"),
            address = formCollection.Get("address"),
            //Store_class = formCollection.Get("Store_class")
            //Introduce = formCollection.Get("Introduce")
            };

            string fullFilePath = "";
            string newFileName = "";
            string fileSavedPath = "";

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase httpPostedFileBase = Request.Files[0];
                if(string.IsNullOrEmpty(list.Name)|| string.IsNullOrEmpty(list.CarName) || string.IsNullOrEmpty(list.county) || string.IsNullOrEmpty(list.Phone) || string.IsNullOrEmpty(list.Email) || string.IsNullOrEmpty(list.Pwd))
                {
                    outModel.ErrMsg = "請完整輸入資料";
                }
                else
                {
                    trytryEntities t = new trytryEntities();
                    var x = from store in t.Store
                            where store.Email_Account == list.Email
                            select store;
                    if (x.Any())
                    {
                        outModel.ErrMsg = "帳號已存在";
                    }
                    else
                    {
                        if (httpPostedFileBase != null)
                        {
                            if (Request.Files["imageFile"].ContentLength > 0)
                            {
                                string extension = Path.GetExtension(httpPostedFileBase.FileName);

                                if (extension == ".jpg" || extension == ".png")
                                {
                                    fileSavedPath = WebConfigurationManager.AppSettings["StoreUploadPath"];
                                    newFileName = list.Email + string.Concat(DateTime.Now.ToString("yyyy-MM-ddHH-mm-ss"), extension.ToLower());
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
                        trytryEntities db = new trytryEntities();
                        Store storeInfo = new Store()
                        {
                            Store_Name = list.CarName,
                            //Store_Class = list.Store_class,
                            //Introduce = list.Introduce,
                            Address_Area = list.district,
                            Address_City = list.county,
                            Address_Local = list.address,
                            Owner_Name = list.Name,
                            Phone = list.Phone,
                            Email_Account = list.Email,
                            Password = list.Pwd,
                            Creation_At = DateTime.Now,
                            Picture = fileSavedPath + "/" + newFileName

                        };
                        try
                        {
                            db.Store.Add(storeInfo);
                            db.SaveChanges();
                            outModel.ResultMsg = "店家註冊完成";
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(list.Name) || string.IsNullOrEmpty(list.CarName) || string.IsNullOrEmpty(list.county) || string.IsNullOrEmpty(list.Phone) || string.IsNullOrEmpty(list.Email) || string.IsNullOrEmpty(list.Pwd))
                {
                    outModel.ErrMsg = "請完整輸入資料";
                }
                else
                {
                    trytryEntities t = new trytryEntities();
                    var x = from store in t.Store
                            where store.Email_Account == list.Email
                            select store;
                    if (x.Any())
                    {
                        outModel.ErrMsg = "帳號已存在";
                    }
                    else
                    {
                        //記得加上判斷，不然誤按enter會爆炸
                       
                        trytryEntities db = new trytryEntities();
                        Store storeInfo = new Store()
                        {
                            Store_Name = list.CarName,
                            //Store_Class = list.Store_class,
                            //Introduce = list.Introduce,
                            Address_Area = list.district,
                            Address_City = list.county,
                            Address_Local = list.address,
                            Owner_Name = list.Name,
                            Phone = list.Phone,
                            Email_Account = list.Email,
                            Password = list.Pwd,
                            Creation_At = DateTime.Now,
                            Picture = fileSavedPath
                        };
                        try
                        {
                            db.Store.Add(storeInfo);
                            db.SaveChanges();
                            outModel.ResultMsg = "店家註冊完成";
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
            }
            return Json(outModel);
        }
        //登入(一般會員及店家)
        public ActionResult Login()
        {   
            if(Session["Store"] == null && Session["Customer"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        public ActionResult DoLogin(string Login_Email, string Login_Pwd)
        {
            
            DoLoginOut outModel = new DoLoginOut();
            if (string.IsNullOrEmpty(Login_Email) || string.IsNullOrEmpty(Login_Pwd))
            {
                outModel.ErrMsg = "請填入帳號/密碼!";
            }
            else
            {
                trytryEntities t = new trytryEntities();
                var Account = from x in t.Customer
                              where x.Email_Account == Login_Email
                              select x;
                if (!Account.Any())
                {
                    outModel.ErrMsg = "帳號不存在/密碼錯誤!";
                }
                else if (Account.FirstOrDefault().Password != Login_Pwd)
                {
                    outModel.ErrMsg = "密碼錯誤!";
                }
                else
                {
                    outModel.ResultMsg = Account.FirstOrDefault().Name + "歡迎回來!";
                    Session["Customer"] = Account.FirstOrDefault();
                }
            }
            return Json(outModel);
        }
        public ActionResult DoLoginStore(string Login_StoreEmail,string Login_StorePwd)
        {

            DoLoginStoreOut outModel = new DoLoginStoreOut();
            if (Login_StoreEmail == "" || Login_StorePwd == "")
            {
                outModel.ErrMsg = "請填入帳號/密碼!";
            }
            else
            {
                trytryEntities t = new trytryEntities();
                var Account = from x in t.Store
                              where x.Email_Account == Login_StoreEmail
                              select x;
                if (!Account.Any())
                {
                    outModel.ErrMsg = "帳號不存在/密碼錯誤!";
                }
                else if (Account.FirstOrDefault().Password != Login_StorePwd)
                {
                    outModel.ErrMsg = "密碼錯誤!";
                }
                else
                {
                    outModel.ResultMsg = Account.FirstOrDefault().Store_Name + "歡迎回來!";
                    Session["Store"] = Account.FirstOrDefault().StoreID;
                }
            }


            return Json(outModel);
        }
    }
}