using SQLiteManager.DLA.Interfaces;
using SQLiteManager.DLA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SQLiteManager.DLA.Implementation
{
	class ProductDAL : IProductRepository
	{
		public async Task<int> AddAsync(Product entity)
		{
			var query = $"INSERT INTO products(code, name, bar_code, quantity, model, sort, color, size, weight, date_changes, id_price)" +
						$"VALUES (@code, @name, @bar_code, @quantity, @model, @sort, @color, @size, @weight, DATETIME('now'), @id_price);";

			using (var connection = DBConnection.CreateConnection())
			{
				connection.Open();
				var result = await connection.ExecuteAsync(query, entity)
											 .ConfigureAwait(false);

				return result;
			}
		}

		public async Task<int> DeleteAsync(int id)
		{
			var query = $"DELETE FROM products " +
						$"WHERE id = @id;";

			using (var connection = DBConnection.CreateConnection())
			{
				var result = await connection.ExecuteAsync(query, new { id = id })
											 .ConfigureAwait(false);

				return result;
			}
		}

		public async Task<IReadOnlyList<Product>> GetAllAsync()
		{
			var query = $"SELECT * FROM products;";

			using (var connection = DBConnection.CreateConnection())
			{
				var result = await connection.QueryAsync<Product>(query)
											 .ConfigureAwait(false);

				return result.AsList<Product>();
			}
		}

		public async Task<Product> GetByIdAsync(int id)
		{
			var query = "SELECT * FROM products WHERE id = @Id;";

			using (var connection = DBConnection.CreateConnection())
			{
				connection.Open();
				var result = await connection.QuerySingleOrDefaultAsync<Product>(query, new { Id = id })
											 .ConfigureAwait(false);

				//if (result == null)
				//	throw new KeyNotFoundException($"user with id [{id}] could not be found.");

				return result;
			}
		}

		public async Task<int> UpdateAsync(Product entity)
		{
			var query = $"UPDATE products " +
						$"SET code = @code, name = @name, bar_code = @bar_code, quantity = @quantity, model = @model, sort = @sort, color = @color, " +
						$"	  size = @size, weight = @weight, DATETIME('now'), id_price = @id_price "+
						$"WHERE id = @id;";

			using (var connection = DBConnection.CreateConnection())
			{
				connection.Open();
				var result = await connection.ExecuteAsync(query, entity)
											 .ConfigureAwait(false);

				return result;
			}
		}
	}
}