using UnityEngine;

namespace BaseFramework
{
    /// <summary>
    /// 日志类，用于记录和输出日志信息
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 用于加锁的对象
        /// </summary>
        private static readonly object LockObj = new object();

        /// <summary>
        /// 日志配置对象
        /// </summary>
        private static ILogConfig _logConfig;

        /// <summary>
        /// 日志配置属性
        /// </summary>
        private static ILogConfig LogConfig
        {
            get
            {
                if (_logConfig == null)
                {
                    lock (LockObj)
                    {
                        if (_logConfig == null)
                        {
                        #if UNITY_EDITOR
                            _logConfig = new LogDebugConfig();
                        #else
                            _logConfig = new LogCloseConfig();
                        #endif
                        }
                    }
                }

                return _logConfig;
            }
            set
            {
                lock (LockObj)
                {
                    _logConfig = value;
                }
            }
        }

        /// <summary>
        /// 设置日志配置
        /// </summary>
        /// <typeparam name="T">日志配置类型</typeparam>
        public static void SetLogConfig<T>() where T : ILogConfig, new()
        {
            LogConfig = new T();
        }


        /// <summary>
        /// 输出调试日志方法
        /// </summary>
        /// <param name="messages">日志信息</param>
        public static void LogDebug(params object[] messages) => LogMessage(ELogLevel.Debug, messages);

        /// <summary>
        /// 输出信息日志方法
        /// </summary>
        /// <param name="messages">日志信息</param>
        public static void LogInfo(params object[] messages) => LogMessage(ELogLevel.Info, messages);

        /// <summary>
        /// 输出警告日志方法
        /// </summary>
        /// <param name="messages">日志信息</param>
        public static void LogWarning(params object[] messages) => LogMessage(ELogLevel.Warning, messages);

        /// <summary>
        /// 输出错误日志方法
        /// </summary>
        /// <param name="messages">日志信息</param>
        public static void LogError(params object[] messages) => LogMessage(ELogLevel.Error, messages);


        /// <summary>
        /// 输出日志信息方法
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <param name="messages">日志信息</param>
        private static void LogMessage(ELogLevel logLevel, params object[] messages)
        {
            // 如果日志未启用或者日志级别低于当前配置级别，则直接返回
            if (!LogConfig.IsLogEnabled || logLevel < LogConfig.LogLevel)
                return;

            // 根据日志级别选择相应的输出方法
            switch (logLevel)
            {
                case ELogLevel.Debug:
                    Debug.Log($"{FormatMessage(logLevel, messages)}");
                    break;
                case ELogLevel.Info:
                    Debug.Log($"{FormatMessage(logLevel, messages)}");
                    break;
                case ELogLevel.Warning:
                    Debug.LogWarning($"{FormatMessage(logLevel, messages)}");
                    break;
                case ELogLevel.Error:
                    Debug.LogError($"{FormatMessage(logLevel, messages)}");
                    break;
                default:
                    throw new System.ArgumentException($"Unsupported log level: {logLevel}");
            }
        }

        /// <summary>
        /// 格式化日志消息
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <param name="messages">日志信息</param>
        /// <returns>格式化后的日志消息</returns>
        private static string FormatMessage(ELogLevel logLevel, params object[] messages)
        {
            if (messages == null || messages.Length == 0)
            {
                return $"[{logLevel.ToString().ToUpper()}]";
            }

            return $"[{logLevel.ToString().ToUpper()}] {string.Join(" ", messages)}";
        }
    }
}