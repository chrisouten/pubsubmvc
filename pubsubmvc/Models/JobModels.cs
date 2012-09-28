using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using DataAnnotationsExtensions;

namespace pubsubmvc.Models
{

	public class JobModel
	{
		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "SKU")]
		public string SKU { get; set; }
		
		[Required]
		[Integer]
		[Min(1, ErrorMessage="Must be greater than 0")]
		[Display(Name = "Bigness")]
		public string Bigness { get; set; }
	}

}
