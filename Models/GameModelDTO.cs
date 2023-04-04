namespace Milestone.Models
{
	public class GameModelDTO
	{
		public int userId { get; set; }
		public DateTime date { get; set; }

		public string gameData { get; set; }

		public GameModelDTO(int userId, DateTime date, string gameData)
		{
			this.userId = userId;
			this.date = date;
			this.gameData = gameData;
		}
	}
}
