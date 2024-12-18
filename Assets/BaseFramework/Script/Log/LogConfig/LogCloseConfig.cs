namespace BaseFramework
{
    public class LogCloseConfig : ILogConfig
    {
        public ELogLevel LogLevel => ELogLevel.Error;
        public bool IsLogEnabled => false;
    }
}