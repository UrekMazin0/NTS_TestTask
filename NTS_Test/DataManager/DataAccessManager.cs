using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLiteManager.DAL.Implementation;
using SQLiteManager.DAL.Model;

namespace NTS_Test.DataManager
{
	class DataAccessManager
	{
		private ProductDAL _productDAL;
		private PriceDAL _priceDAL;

		private List<Product> _currentProducts;

		public event EventHandler<DataUpdateEventArgs> dataUpdate;

		public List<Product> Products
		{
			get { return _currentProducts; }
		}

		public DataAccessManager()
		{
			_productDAL = new ProductDAL();
			_priceDAL = new PriceDAL();
			_currentProducts = new List<Product>();
		}

		public void FilterProducts()
		{
			// code name barcode price
			// int  str  str     decimal
			var query = $"SELECT * FROM products";
		}

		public void GetByLimit(int limit)
		{
			_currentProducts = (List<Product>)_productDAL.GetByLimitAsync(limit).Result;

			var args = new DataUpdateEventArgs { products = _currentProducts };

			EventHandler<DataUpdateEventArgs> handler = dataUpdate;
			if (handler != null)
				handler(this, args);
		}

		public async void FilterUpdateEventHandler(object sender, FilterUpdateEventArgs e)
		{
			var result = await _productDAL.GetByFilterAsync(name: e.name,
															code: e.code,
															bar_code: e.bar_code,
															price: e.price);

			_currentProducts = (List<Product>)result;

			var args = new DataUpdateEventArgs { products = _currentProducts };

			EventHandler<DataUpdateEventArgs> handler = dataUpdate;
			if (handler != null)
				handler(this, args);
		}

		public async void EntityUpdateEventHandler(object sender, EntityUpdateEventArgs e)
		{
			var product = e.product;

			switch (e.fieldName)
			{
				case "name":
					product.name = e.value;
					break;
				case "code":
					product.code = Int32.Parse(e.value);
					break;
				case "barcode":
					product.bar_code = e.value;
					break;
				case "quantity":
					product.quantity = Decimal.Parse(e.value);
					break;
				case "sort":
					product.sort = e.value;
					break;
				case "color":
					product.color = e.value;
					break;
				case "size":
					product.size = e.value;
					break;
				case "weight":
					product.weight = Decimal.Parse(e.value);
					break;
				default:
					break;
			}

			Trace.WriteLine(product);
			
			var result = await _productDAL.UpdateAsync(product);
			
			Trace.WriteLine($"ROWS: {result}");
		}
	}
}
