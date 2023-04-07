namespace Milestone.Models
{
	public class GameModelDTO
	{
		public int id { get; set; }
		public int userId { get; set; }
		public DateTime date { get; set; }

		public string gameData { get; set; }

		public GameModelDTO(int id, int userId, DateTime date, string gameData)
		{
			this.id = id;
			this.userId = userId;
			this.date = date;
			this.gameData = gameData;
		}
	}
}
