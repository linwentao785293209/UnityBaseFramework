namespace BaseFramework
{
    /// <summary>
    /// Debug级日志配置
    /// </summary>
    internal class LogDebugConfig : ILogConfig
    {
        public ELogLevel LogLevel => ELogLevel.Debug;

        public bool IsLogEnabled => true;
    }
}