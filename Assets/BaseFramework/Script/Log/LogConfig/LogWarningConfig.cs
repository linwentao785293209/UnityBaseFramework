namespace BaseFramework
{
    /// <summary>
    /// Warning级日志配置
    /// </summary>
    internal class LogWarningConfig : ILogConfig
    {
        public ELogLevel LogLevel => ELogLevel.Warning;

        public bool IsLogEnabled => true;
    }
}