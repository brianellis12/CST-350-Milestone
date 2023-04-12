
using Activity_2_RegisterAndLoginApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Milestone.Models
{
	public class GameDAO
	{
		string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Milestone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

		public List<GameModel> getGames()
		{
			List<GameModel> games = new List<GameModel>();

			string sqlStatment = "SELECT * FROM dbo.Games";

			using(SqlConnection connection = new SqlConnection(connectionstring))
			{
				SqlCommand command = new SqlCommand(sqlStatment, connection);
				try
				{
					connection.Open();
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						games.Add(new GameModel((int)reader[0], (int)reader[1], (DateTime)reader[2], (string)reader[3]));
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
			string sqlStatment = "SELECT * FROM dbo.Games WHERE Id = @Id";

			using (SqlConnection connection = new SqlConnection(connectionstring))
			{
				SqlCommand command = new SqlCommand(sqlStatment, connection);
				command.Parameters.AddWithValue("@Id", id);
				
				try
				{
					connection.Open();
					SqlDataReader reader = command.ExecuteReader();
					
					while (reader.Read())
					{
						gameModel = new GameModel((int)reader[0], (int)reader[1], (DateTime)reader[2], (string)reader[3]);
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
			using (SqlConnection connection = new SqlConnection((string)connectionstring))
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

		public bool saveGame(GameModel game)
		{
			using (SqlConnection connection = new SqlConnection(connectionstring))
			{
				string query = "INSERT INTO dbo.Games(userId, date, gameData) VALUES (@UserId, @Date, @GameData)";

				SqlCommand myCommand = new SqlCommand(query, connection);
				myCommand.Parameters.AddWithValue("@UserId", game.userId);
				myCommand.Parameters.AddWithValue("@Date", game.date);
				myCommand.Parameters.AddWithValue("@GameData", game.gameData);

				try
				{
					connection.Open();
					SqlDataReader reader = myCommand.ExecuteReader();
				}
				catch (Exception ex) { Console.WriteLine(ex.Message); };

				return true;
			}
		}
	}
}
