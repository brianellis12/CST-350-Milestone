using NLog;

namespace Activity_2_RegisterAndLoginApp.Utility
{
    public class LoginLogger : ILoggers
    {
        private static LoginLogger instance;
        private static Logger logger;

        public static LoginLogger GetInstance()
        {
            if (instance == null)
            {
                instance = new LoginLogger();
            }
            return instance;
        }

        private Logger GetLogger()
        {
            if (LoginLogger.logger == null)
            {
                LoginLogger.logger = LogManager.GetLogger("LoginRule");
            }
            return LoginLogger.logger;
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
