using SQLiteManager.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTS_Test.DataManager
{
	public class EntityUpdateEventArgs : EventArgs
	{
		public int index;
		public string fieldName;
		public string value;
		public Product product;
	}
}
