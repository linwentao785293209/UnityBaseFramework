namespace BaseFramework
{
    internal class LogWarningConfig : ILogConfig
    {
        public ELogLevel LogLevel => ELogLevel.Warning;

        public bool IsLogEnabled => true;
    }
}