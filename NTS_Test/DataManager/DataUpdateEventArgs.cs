using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLiteManager.DAL.Model;

namespace NTS_Test.DataManager
{
	public class DataUpdateEventArgs
	{
		public List<Product> products { get; set; }
	}
}
