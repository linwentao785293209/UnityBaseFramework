namespace BaseFramework
{
    /// <summary>
    /// Info级日志配置
    /// </summary>
    internal class LogInfoConfig : ILogConfig
    {
        public ELogLevel LogLevel => ELogLevel.Info;

        public bool IsLogEnabled => true;
    }
}