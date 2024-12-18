namespace BaseFramework
{
    internal class LogErrorConfig : ILogConfig
    {
        public ELogLevel LogLevel => ELogLevel.Error;

        public bool IsLogEnabled => true;
    }
}