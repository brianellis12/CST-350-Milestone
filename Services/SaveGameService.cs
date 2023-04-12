using Activity_2_RegisterAndLoginApp.Models;
using Milestone.Models;
using Milestone.Services;
using System;

namespace Activity_2_RegisterAndLoginApp.Services
{
	public class SaveGameService
	{

		public GameAPIClient GetApiClient()
		{
			var client = new HttpClient();
			var apiClient = new GameAPIClient(client);
			return apiClient;
		}

		SecurityDAO securityDAO = new SecurityDAO();


		public List<GameModel> getUserGames(string username)
		{
			var apiClient = GetApiClient();
			List<GameModelDTO> gamesDTO = apiClient.GetGamesAsync().Result;
			List<GameModel> games = gamesDTO.Select(g =>
			{
				return new GameModel(g.id, g.userId, g.date, g.gameData);
			}).ToList();
			return games;
		}

		public async Task saveGame(string username, Board gameboard)
		{
			String currentGameData = serializeData(gameboard);
			int userId = getUserId(username);

			GameModelDTO gameDTO = new GameModelDTO(0, userId, DateTime.Now, currentGameData);
			var apiClient = GetApiClient();
			await apiClient.SaveGameAsync(gameDTO);

		}

		public GameModel getGameById(int id)
		{
			var apiClient = GetApiClient();
			GameModelDTO gameDTO = apiClient.GetOneGameAsync(id).Result;
			var game = new GameModel(gameDTO.id, gameDTO.userId, gameDTO.date, gameDTO.gameData);
			return game;
		}

		public async Task deleteGame(int id)
		{
			var apiClient = GetApiClient();
			await apiClient.DeleteGameAsync(id);
		}

		public int getUserId(string username)
		{
			int userId = securityDAO.getUserIdByUsername(username);
			return userId;
		}

		public string serializeData(Board gameboard)
		{
			string data = "";

			for (int i = 0; i < gameboard.Size; i++)
			{
				for (int j = 0; j < gameboard.Size; j++)
				{
					if (gameboard.Grid[i, j].IsVisited == true)
					{
						data += "V0";
					}
					else
					{
						data += "V1";
					}
					if (gameboard.Grid[i, j].IsLive == true)
					{
						data += "L0";
					}
					else
					{
						data += "L1";
					}
					if (gameboard.Grid[i, j].IsFlagged == true)
					{
						data += "F0";
					}
					else
					{
						data += "F1";
					}

					data += "&";
				}
			}
			return data;
		}

		public Board deserialize(string data)
		{
			List<string> saveCells = data.Split('&').ToList();
			Board gameboard = new Board((int)Math.Sqrt(saveCells.Count - 1));

			for (int i = 0; i < gameboard.Size; i++)
			{
				for (int j = 0; j < gameboard.Size; j++)
				{
					string selectedCell = saveCells[j * (i + 1)];
					if (selectedCell.Contains("V0"))
					{
						gameboard.Grid[i, j].IsVisited = true;
					}
					else
					{
						gameboard.Grid[i, j].IsVisited = false;
					}
					if (selectedCell.Contains("L0"))
					{
						gameboard.Grid[i, j].IsLive = true;
					}
					else
					{
						gameboard.Grid[i, j].IsLive = false;
					}
					if (selectedCell.Contains("F0"))
					{
						gameboard.Grid[i, j].IsFlagged = true;
					}
					else
					{
						gameboard.Grid[i, j].IsFlagged = false;
					}
				}
			}
			return gameboard;
		}
	}
}
