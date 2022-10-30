using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteManager.DLA.Implementation
{
	public static class DBConnection
	{
		public static string DbFile
		{
			//get { return Environment.CurrentDirectory + ConfigurationManager.AppSettings.Get("DataBaseFile"); }
			get { return Environment.CurrentDirectory + Path.DirectorySeparatorChar + ConfigurationManager.AppSettings.Get("DataBaseFile"); }
		}

		public static SQLiteConnection CreateConnection()
		{
			return new SQLiteConnection("Data Source=" + DbFile +
										"");
		}
	}
}
