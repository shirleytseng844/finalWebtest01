//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace webtest01.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Message_Board
    {
        public int MessageID { get; set; }
        public int StoreID { get; set; }
        public int CustomerID { get; set; }
        public string Text_Content { get; set; }
        public Nullable<int> Star_Rating { get; set; }
        public string Picture { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Store Store { get; set; }
    }
}
