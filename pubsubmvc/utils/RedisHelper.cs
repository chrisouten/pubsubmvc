using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Redis;
using System.Configuration;

namespace pubsubmvc.utils
{
	public static class RedisHelper
	{
		public static RedisClient GetRedisClient() {
			var enviroment = ConfigurationManager.AppSettings.Get("Environment");
			if (enviroment == "Debug")
			{
				return new RedisClient();
			}
			else
			{
				var redisUrl = ConfigurationManager.AppSettings.Get("REDISTOGO_URL");
				var connectionUri = new Uri(redisUrl);
				return new RedisClient(connectionUri);
			}
		}

		public static RedisUser RegisterUser(string username, string pword) {
			RedisClient rc = GetRedisClient();
			if (rc.Exists(String.Format("user-{0}", username)) == 0)
			{
				rc.Set<RedisUser>(String.Format("user-{0}", username), new RedisUser(username, pword));
				return rc.Get<RedisUser>(String.Format("user-{0}", username));
			}
			else
			{
				return null;
			}
		}

		public static RedisUser LoginUser(string username, string pword)
		{
			RedisClient rc = GetRedisClient();
			return rc.Get<RedisUser>(String.Format("user-{0}", username));
		}

		public static BigJob CreateBigJob(string ru, int total, string sku) {
			return new BigJob(ru, total, sku);
		}

	};

	public class RedisUser
	{
		public string UserName { get; set; }
		public string Password { get; set; }

		public RedisUser(string username, string password) {
			this.UserName = username;
			this.Password = password;
		}
	}

	public class BigJob
	{

		public string User { get; set; }
		public int Progress { get; set; }
		public int Total { get; set; }
		public string SKU { get; set; }

		public BigJob(string user, int total, string SKU)
		{
			this.User = user;
			this.Progress = 0;
			this.Total = total;
			this.SKU = SKU;
		}
	}
}