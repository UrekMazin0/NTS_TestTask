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
		static string dbFile;
		public static string DbFile
		{
			// мне совершенно не нравится указывать абсолютный путь к БД
			// но другого я ничего не придумал
			get 
			{
				if (dbFile == null)
					dbFile = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName +
							 Path.DirectorySeparatorChar + "database" + Path.DirectorySeparatorChar + "ntsDB.db";

				return dbFile;
			}
		}

		public static SQLiteConnection CreateConnection()
		{
			return new SQLiteConnection("Data Source=" + DbFile +
										"");
		}
	}
}
