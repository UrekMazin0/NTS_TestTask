using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteManager.DLA.Model
{
	class Price
	{
		public int     id	 { get; set; }
		public decimal price { get; set; }

		public override string ToString()
		{
			return $"{id} : {price}";
		}
	}
}
