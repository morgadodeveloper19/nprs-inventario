using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace WebService1
{
	public class WebServiceResult
	{
		public bool Success { get;   set; }
        public  string ErrorMessage { get; set; }

		private int _id = 1;
		private object _result;
		
		public int id
		{
			get { return _id; }
			set { _id = value; }
		}

		public object result
		{
			get { return _result; }
			set { _result = value; }
		}
	}
}