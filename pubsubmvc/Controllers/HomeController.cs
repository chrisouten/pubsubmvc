using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.Redis;
using System.Configuration;

namespace pubsubmvc.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "Welcome to ASP.NET MVC!";
			var redisUrl = ConfigurationManager.AppSettings.Get("REDISTOGO_URL");
			var connectionUri = new Uri(redisUrl);
			using (var redis = new RedisClient(connectionUri))
			{
				redis.Set("hello", "world");
				var value = redis.Get<string>("hello");
				ViewBag.Message = value;
			}
			
			return View();
		}

		public ActionResult About()
		{
			return View();
		}
	}
}
