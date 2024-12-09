namespace BaseFramework
{
    /// <summary>
    /// 日志配置接口
    /// </summary>
    public interface ILogConfig
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        ELogLevel LogLevel { get; }

        /// <summary>
        /// 是否启用日志
        /// </summary>
        bool IsLogEnabled { get; }
    }
}