namespace BaseFramework
{
    internal class LogDebugConfig : ILogConfig
    {
        public ELogLevel LogLevel => ELogLevel.Debug;

        public bool IsLogEnabled => true;
    }
}