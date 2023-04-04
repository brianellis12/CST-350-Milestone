
using Activity_2_RegisterAndLoginApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Milestone.Models
{
	public class GameDAO
	{
		string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Milestone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

		public List<GameModel> getGames()
		{
			List<GameModel> games = new List<GameModel>();

			string sqlStatment = "SELECT * FROM dbo.Games";

			using(SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(sqlStatment, connection);
				try
				{
					connection.Open();
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						games.Add(new GameModel((int)reader[0], (DateTime)reader[1], (string)reader[2]));
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}

			return games;
		}

		public GameModel getOneGame(int id)
		{
			GameModel gameModel = null;
			string sqlStatment = "SELECT * FROM dbo.Games";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(sqlStatment, connection);
				try
				{
					connection.Open();
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						gameModel = new GameModel((int)reader[0], (DateTime)reader[1], (string)reader[2]);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
			return gameModel;

		}

		public bool deleteGame(int id)
		{
			string sqlStatment = "SELECT * FROM dbo.Games";

			using (SqlConnection connection = new SqlConnection((string)connectionString))
			{
				string query = "DELETE FROM dbo.Games WHERE Id = @Id";

				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Id", id);

				try
				{
					connection.Open();
					command.ExecuteScalar();
				}
				catch (Exception ex) { Console.WriteLine(ex.Message); }
			}
			return true;
		}
	}
}
