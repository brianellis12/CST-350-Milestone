using Milestone.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Activity_2_RegisterAndLoginApp.Services
{
	public class GameAPIClient
	{
		private readonly HttpClient _client;
		public GameAPIClient(HttpClient client)
		{
			_client = client;
			_client.BaseAddress = new Uri("https://localhost:7229/");
		}

		public async Task<List<GameModelDTO>> GetGamesAsync()
		{
			var response = await _client.GetAsync("api/GameAPI");
			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
			var games = JsonConvert.DeserializeObject<List<GameModelDTO>>(content);
			return games;
		}

		public async Task<GameModelDTO> GetOneGameAsync(int id)
		{
			var response = await _client.GetAsync($"api/GameAPI/showOneGame/{id}");
			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
			var game = JsonConvert.DeserializeObject<GameModelDTO>(content);
			return game;
		}

		public async Task<bool> DeleteGameAsync(int id)
		{
			var response = await _client.DeleteAsync($"api/GameAPI/deleteOneGame/{id}");
			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<bool>(content);
			return result;
		}

		public async Task<bool> SaveGameAsync(GameModelDTO game)
		{
			var json = JsonConvert.SerializeObject(game);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/GameAPI/saveGame", content);
			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadAsStringAsync();
			return bool.Parse(result);
		}
	}
}
