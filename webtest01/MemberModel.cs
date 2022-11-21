using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webtest01.Models
{
    
	//一般會員
	public class DoRegisterIn
	{
		public string UserAccount { get; set; }
		public string UserPwd { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string county { get; set; }
		public string district { get; set; }
		public string address { get; set; }
	}

	public class DoRegisterOut
	{
		public string ErrMsg { get; set; }
		public string ResultMsg { get; set; }
	}
	//店家會員
	public class DoRegisterStoreIn
	{
		public string CarName { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Pwd { get; set; }
		public string county { get; set; }
		public string district { get; set; }
		public string address { get; set; }
	}

	public class DoRegisterStoreOut
	{
		public string ErrMsg { get; set; }
		public string ResultMsg { get; set; }
	}


	public class DoLoginIn
    {
		public string UserAccount { get; set; }
		public string UserPwd { get; set; }
	}
	public class DoLoginOut
    {
		public string ErrMsg { get; set; }
		public string ResultMsg { get; set; }
	}
}
	public class DoLoginStoreIn
{
	public string StoreAccount { get; set; }
	public string StorePwd { get; set; }
}
	public class DoLoginStoreOut
{
	public string ErrMsg { get; set; }
	public string ResultMsg { get; set; }
}