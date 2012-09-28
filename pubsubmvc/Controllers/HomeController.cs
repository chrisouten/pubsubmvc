using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.Redis;
using System.Configuration;
using pubsubmvc.utils;
using System.Threading;
using pubsubmvc.Models;
using System.Web.Script.Serialization;

namespace pubsubmvc.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "Welcome to ASP.NET MVC!";
			var enviroment = ConfigurationManager.AppSettings.Get("Environment");
			RedisClient rc = RedisHelper.GetRedisClient();
			using (var redis = rc)
			{
				redis.Set("hello", "testing 123");
				var value = redis.Get<string>("hello");
				ViewBag.Message = value;
			}
			return View();
		}

		public ActionResult About()
		{
			RedisClient rc = RedisHelper.GetRedisClient();
			new Thread(() =>
				{
					int x = 0;
					for (int i = 0; i < 20; i++)
					{
						int blah = rc.PublishMessage("bigjob", x.ToString());
						Thread.Sleep(1000);
						x++;
					}

				}).Start();
			return View();
		}

		public ActionResult Jobs()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Jobs(JobModel model)
		{
			if (ModelState.IsValid)
			{
				RedisClient rc = RedisHelper.GetRedisClient();
				JavaScriptSerializer s = new JavaScriptSerializer();
				BigJob job = RedisHelper.CreateBigJob(User.Identity.Name, int.Parse(model.Bigness), model.SKU);
				new Thread(() =>
					{
						int x = 0;
						for (int i = 0; i < job.Total; i++)
						{
							job.Progress++;
							rc.PublishMessage("bigjob", s.Serialize(job));
							Thread.Sleep(1000);
						}
					}).Start();
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}
	}
}
