using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace pubsubmvc.Models
{

	public class JobModel
	{
		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "SKU")]
		public string SKU { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Bigness")]
		public string NewPassword { get; set; }
	}

}
