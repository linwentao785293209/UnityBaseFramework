namespace BaseFramework
{
    /// <summary>
    /// Error级日志配置
    /// </summary>
    internal class LogErrorConfig : ILogConfig
    {
        public ELogLevel LogLevel => ELogLevel.Error;

        public bool IsLogEnabled => true;
    }
}