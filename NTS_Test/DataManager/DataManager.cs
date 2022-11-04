using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLiteManager.DAL.Implementation;
using SQLiteManager.DAL.Model;

namespace NTS_Test.DataManager
{
	class DataManager
	{
		private ProductDAL _productDAL;
		private PriceDAL _priceDAL;

		private List<Product> _products;
		public List<Product> Products
		{
			get { return _products; }
		}

		public	DataManager()
		{
			_productDAL = new ProductDAL();
			_priceDAL   = new PriceDAL();
			_products   = new List<Product>();
		}

		public void FilterProducts()
		{
			// code name barcode price
			// int  str  str     decimal
			var query = $"SELECT * FROM products";
		}
	}
}
