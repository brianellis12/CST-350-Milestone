using NLog;

namespace Activity_2_RegisterAndLoginApp.Utility
{
    public class GameLogger : ILoggers
    {
        private static GameLogger instance;
        private static Logger logger;

        public static GameLogger GetInstance()
        {
            if (instance == null)
            {
                instance = new GameLogger();
            }
            return instance;
        }

        private Logger GetLogger()
        {
            if (GameLogger.logger == null)
            {
                GameLogger.logger = LogManager.GetLogger("GameRule");
            }
            return logger;
        }
        public void Debug(string message)
        {
            GetLogger().Debug(message);
        }

        public void Error(string message)
        {
            GetLogger().Error(message);
        }

        public void Info(string message)
        {
            GetLogger().Info(message);
        }

        public void Warning(string message)
        {
            GetLogger().Warn(message);
        }
    }
}
