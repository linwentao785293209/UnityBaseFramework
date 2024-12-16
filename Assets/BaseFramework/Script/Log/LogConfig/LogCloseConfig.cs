namespace BaseFramework
{
    /// <summary>
    /// 关闭日志配置
    /// </summary>
    public class LogCloseConfig : ILogConfig
    {
        public ELogLevel LogLevel => ELogLevel.Error;
        public bool IsLogEnabled => false;
    }
}