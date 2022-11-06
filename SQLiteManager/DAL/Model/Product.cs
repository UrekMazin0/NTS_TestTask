using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteManager.DAL.Model
{
	public class Product
	{
		public int      id       { get; set; }
		public int      id_price { get; set; }
		public int		code     { get; set; }
		public string   name     { get; set; }
		public string   bar_code { get; set; }
		public decimal  quantity { get; set; }
		public string   model    { get; set; }
		public string   sort     { get; set; }
		public string   color    { get; set; }
		public string   size     { get; set; }
		public decimal  weight   { get; set; }
		public DateTime date_changes { get; set; }

		public Price Price { get; set; }

		public override string ToString()
		{
			return $"{id} : {id_price} : {code} : {name} : {bar_code} : {quantity} : " +
				   $"{model} : {sort} : {color} : {size} : {weight} : {date_changes} : {Price.price}";
		}
	}
}
