using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace webtest01.Models
{
    public class StoreInfo
    {
        public class StoreCarInfoEditIn
        {
            public string CarName { get; set; }
            public string county { get; set; }
            public string district { get; set; }
            public string address { get; set; }

            public string Introduce { get; set; }

        }
        public class StoreCarInfoEditOut
        {
            public string ErrMsg { get; set; }
            public string ResultMsg { get; set; }
        }




        public class StoreBossInfoEditOut
        {
            public string ErrMsg { get; set; }
            public string ResultMsg { get; set; }
            public string relogin { get; set; }
        }

        public class ClickStoreCardIn
        {
            public string storeID { get; set; }
        }
        public class ClickStoreCardOut
        {
            public int storeID { get; set; }
            public int CalendarID { get; set; }
            public string Store_Name { get; set; }
            public string Introduce { get; set; }
            public string Store_Class { get; set; }
            public string Address_City { get; set; }
            public string Address_Local { get; set; }
            public string Phone { get; set; }
            public string longitude { get; set; }
            public string latitude { get; set; }
            public string Picture { get; set; }
        }



        public class ProductInfoOut
        {
            public int ProductID { get; set; }
            public string Product_Name { get; set; }
            public int product_Price { get; set; }
        }
        public class MessageCommentCardOut
        {
            public int CustomerID { get; set; }
            public string Text_Content { get; set; }
            public int Star_Rating { get; set; }
            public string Picture { get; set; }
            public string CustomerName { get; set; }

        }


        public static void sendmail()
        {
            string to = "jane@contoso.com";
            string from = "ben@contoso.com";
            string subject = "Using the new SMTP client.";
            string body = @"Using this new feature, you can send an email message from an application very easily.";
            MailMessage message = new MailMessage(
                from,
                to,
                subject,
                body
                );

            SmtpClient smtpClient = new SmtpClient(); //偵測伺服器的
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Host ="" ;//指定SMTP服務器
            smtpClient.Credentials = new System.Net.NetworkCredential("name", "pass");//用戶名和密碼
            MailMessage mail = new MailMessage();//發件人，發件人名
            mail.From = new MailAddress("發送箱地址", "別名");//收件人
            mail.To.Add("接收郵箱");
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Subject = "主題";
            mail.Body = "內容";
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;//設置為HTML格式
            mail.Priority = MailPriority.High;//優先級
            //string MessageBody = "請進行郵箱驗證來完成您註冊的最後一步,點擊下面的鏈接激活您的帳號：<br><a target=‘_blank‘ rel=‘nofollow‘ style=‘color: #0041D3; text-decoration: underline‘ href=‘http://www.****.net/regeditOK.aspx‘>激活</a>"; //郵件內容 （一般是一個網址鏈接，生成隨機數加驗證id參數，點擊去網站驗證。）
            try
            {
                smtpClient.Send(mail);
            }
            catch (Exception)
            {

            }


        }
    
    }
}