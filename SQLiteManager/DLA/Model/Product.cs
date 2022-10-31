using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteManager.DLA.Model
{
	class Product
	{
		public int      id       { get; set; }
		public int      price_id { get; set; }
		public int		code     { get; set; }
		public string   name     { get; set; }
		public string   bar_code { get; set; }
		public decimal  quantity { get; set; }
		public string   model    { get; set; }
		public string   sort     { get; set; }
		public string   color    { get; set; }
		public string   size     { get; set; }
		public string   weight   { get; set; }
		public DateTime datetime { get; set; }

		public override string ToString()
		{
			return $"{id} : {price_id} : {code} : {name} : {bar_code} : {quantity} : " +
				   $"{model} : {sort} : {color} : {size} : {weight} : {datetime}";
		}
	}
}
