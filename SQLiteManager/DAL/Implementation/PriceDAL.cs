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
	public class PriceDAL : IPriceRepository
	{
		public async Task<int> AddAsync(Price entity)
		{
			var query = $"INSERT INTO prices (price) " +
						$"VALUES (@price);";

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
			var query = $"DELETE FROM prices " +
						$"WHERE id = @id;";

			using (var connection = DBConnection.CreateConnection())
			{
				var result = await connection.ExecuteAsync(query, new { id = id })
										     .ConfigureAwait(false);
				return result;
			}
		}

		public async Task<IReadOnlyList<Price>> GetAllAsync()
		{
			var query = $"SELECT * FROM prices";
			
			using (var connection = DBConnection.CreateConnection())
			{
				var result = await connection.QueryAsync<Price>(query)
											 .ConfigureAwait(false);

				return result.AsList<Price>();
			}
		}

		public async Task<Price> GetByIdAsync(int id)
		{
			var query = "SELECT * FROM prices WHERE id = @Id;";

			using (var connection = DBConnection.CreateConnection())
			{
				connection.Open();
				var result = await connection.QuerySingleOrDefaultAsync<Price>(query, new { Id = id })
											 .ConfigureAwait(false);

				//if (result == null)
				//	throw new KeyNotFoundException($"user with id [{id}] could not be found.");

				return result;
			}
		}

		public async Task<int> UpdateAsync(Price entity)
		{
			var query = $"UPDATE prices " +
						$"SET price = @price "+
						$"WHERE id = @id";

			using (var connection = DBConnection.CreateConnection())
			{
				connection.Open();
				var result = await connection.ExecuteAsync(query, entity)
											 .ConfigureAwait(false);

				return result;
			}
		}

		public async Task<bool> Exist(Price entity)
		{
			var query = "SELECT count(1) FROM prices WHERE price=@price";
			
			using(var connection = DBConnection.CreateConnection())
			{
				connection.Open();
				var exist = await connection.ExecuteScalarAsync<bool>(query, entity).ConfigureAwait(false);
				
				return exist;
			}
		}

		public async Task<int> GetIdCreateIfNotExistAsync(Price entity)
		{
			using(var connection = DBConnection.CreateConnection())
			{
				connection.Open();

				var exist = await connection.ExecuteScalarAsync<bool>("SELECT count(1) FROM prices WHERE price=@price", entity)
											.ConfigureAwait(false);

				if(!exist)
				{
					_ = await connection.ExecuteAsync("INSERT INTO prices (price) VALUES (@price);", entity)
										.ConfigureAwait(false);

					
				}

				var result = await connection.QuerySingleOrDefaultAsync<Price>("SELECT id FROM prices WHERE price=@price", entity)
											 .ConfigureAwait(false);

				return result.id;

			}
		}

		public async Task<int> GetIdIfExistAsync(Price entity)
		{
			var exist = await Exist(entity);
			if (!exist)
				return -1;

			var query = "SELECT id FROM prices WHERE price=@price";
			using (var connection = DBConnection.CreateConnection())
			{
				connection.Open();
				var result = await connection.QuerySingleOrDefaultAsync<Price>(query, entity);

				return result.id;
			}
		}
	}
}
