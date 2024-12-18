namespace BaseFramework
{
    internal class LogInfoConfig : ILogConfig
    {
        public ELogLevel LogLevel => ELogLevel.Info;

        public bool IsLogEnabled => true;
    }
}