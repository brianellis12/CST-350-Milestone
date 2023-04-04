namespace Activity_2_RegisterAndLoginApp.Models
{
	public class GameModel
	{
		public int userId { get; set; }
		public DateTime date { get; set; }

		public string gameData { get; set; }

		public GameModel(int userId, DateTime date, string gameData)
		{
			this.userId = userId;
			this.date = date;
			this.gameData = gameData;
		}
	}
}
