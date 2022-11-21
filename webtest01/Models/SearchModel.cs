using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webtest01.Models
{
    public class SearchModel
    {
        public string time { get; set; }
        public string FoodClass { get; set; }
        public string location { get; set; }
        public string KeyWord { get; set; }
    }




    public class SearchStoreCardList
    {
        public List<SearchStoreCard> SearchStoreCards { get; set; }
    }
    public class SearchStoreCard
    {
    public int StoreID { get; set; }
        public string Store_Name { get; set; }
        public string Store_Class { get; set; }
        public string Address_Area { get; set; }
        public string Address_City { get; set; }
        public string Address_Detail { get; set; }
        public string Address_Local { get; set; }
        public string Store_Address { get; set; }
        public int CalendarID { get;set;}
        public int ContentCount { get;set;}
        public int CustomerFavoriteStoreID { get;set;}
        public int CustomerID{ get; set; }
        public string Introduce { get; set; }
        public bool OnBusiness { get; set; }
        public string Picture { get; set; }
        public string Phone { get; set; }
        public string Punch_Start { get; set; }
        public string Punch_End { get; set; }
        public string Punch_Time { get; set; }
        public int StarRating { get; set; }

    public string Creation_At { get; set; }



    //public string Introduce { get; set; }


    //{"StoreID":1,"Store_Name":"Lighthouse Food Truck燈塔餐車","Store_Class":"美式","Address_Area":"北部","Address_City":"台北市/新北市","Address_Detail":"台北市大安區大安路","Picture":null,"Phone":"0952 766 427","Punch_Start":"2022/09/12 09:00","Punch_End":"2022/09/12 24:00","Punch_Time":"2022/09/12 09:00~24:00","StarRating":4.3,"ContentCount":3}

    ////store
    //public int StoreID { get; set; }
    //public string Store_Name { get; set; }
    //public string Store_Class { get; set; }
    //public string Introduce { get; set; }
    //public string Picture { get; set; }
    //public string Phone { get; set; }

    ////StoreBusiness
    //public int CalendarID { get; set; }
    //public string Address_Area { get; set; }
    //public string Address_City { get; set; }
    //public string Address_Local { get; set; }
    //public string Store_Address { get; set; }
    //public string Punch_Start { get; set; }
    //public string Punch_End { get; set; }
    //public Nullable<bool> OnBusiness { get; set; }

    ////Customer_Favorite
    //public int CustomerFavoriteStoreID { get; set; }

    ////Messenger_Board
    //public Nullable<double> StarRating { get; set; }
    //public int ContentCount { get; set; }

    ////合併顯示
    //public string Punch_Time { get; set; }
    //public string Address_Detail { get; set; }
}
}