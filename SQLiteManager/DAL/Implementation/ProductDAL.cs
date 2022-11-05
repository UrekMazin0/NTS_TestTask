using SQLiteManager.DAL.Interfaces;
using SQLiteManager.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Diagnostics;

namespace SQLiteManager.DAL.Implementation
{
	public class ProductDAL : IProductRepository
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
						$"	  size = @size, weight = @weight, date_changes = DATETIME('now'), id_price = @id_price "+
						$"WHERE id = @id;";

			using (var connection = DBConnection.CreateConnection())
			{
				connection.Open();
				var result = await connection.ExecuteAsync(query, entity)
											 .ConfigureAwait(false);

				return result;
			}
		}

		public async Task<IReadOnlyList<Product>> GetByLimitAsync(int limit)
		{
			var query = $"SELECT * " +
						$"FROM products pro " +
						$"INNER JOIN prices pri " +
						$"ON pro.id_price = pri.id " +
						$"LIMIT {limit}";

			using (var connection = DBConnection.CreateConnection())
			{
				connection.Open();

				var result = await connection.QueryAsync<Product, Price, Product>(query, (product, price) =>
				{
					product.Price = price;
					return product;
				},
				splitOn: "id_price")
				.ConfigureAwait(false);

				return result.AsList<Product>();
			}
		}

		public async Task<IReadOnlyList<Product>> GetByFilterAsync(string name ="", int code=-1, string bar_code="", decimal price=-1)
		{
			var query = $"SELECT * " +
						$"FROM products pro " +
						$"INNER JOIN prices pri " +
						$"ON pro.id_price = pri.id ";

			// если хотя бы один из параметров пришёл заполненный, добавляем блок WHERE
			if (name != "" || code != -1 || bar_code != "" || price != -1)
				query += "WHERE ";


			bool first = true;

			if(name != "")
			{
				if (first)
				{
					first = false;
					query += $" pro.name = \"{name}\"";
				}
				else
				{
					query += $" AND pro.name = \"{name}\"";
				}
			}

			if (code != -1)
			{
				if (first)
				{
					first = false;
					query += $"pro.code = \"{code}\"";
				}
				else
				{
					query += $" AND pro.code = \"{code}\"";
				}
			}

			if (bar_code != "")
			{
				if (first)
				{
					first = false;
					query += $"pro.bar_code = \"{bar_code}\"";
				}
				else
				{
					query += $" AND pro.bar_code = \"{bar_code}\"";
				}
			}

			if (price != -1)
			{
				if (first)
				{
					first = false;
					query += $"pri.price = \"{price}\"";
				}
				else
				{
					query += $"AND pri.price = \"{price}\"";
				}
			}

			query += ";";

			Trace.WriteLine(query);

			using (var connection = DBConnection.CreateConnection())
			{
				connection.Open();

				var result = await connection.QueryAsync<Product, Price, Product>(query, (product, pri) =>
				{
					product.Price = pri;
					return product;
				},
				splitOn: "id_price");

				return result.AsList<Product>();
			}
		}
	}
}