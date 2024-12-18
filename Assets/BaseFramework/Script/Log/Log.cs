using UnityEngine;

namespace BaseFramework
{
    public static class Log
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

            // 获取日志级别对应的颜色
            string color = GetLogLevelColor(logLevel);
            string formattedMessage = $"<color={color}>{FormatMessage(logLevel, messages)}</color>";

            // 根据日志级别选择相应的输出方法
            switch (logLevel)
            {
                case ELogLevel.Debug:
                case ELogLevel.Info:
                    Debug.Log(formattedMessage);
                    break;
                case ELogLevel.Warning:
                    Debug.LogWarning(formattedMessage);
                    break;
                case ELogLevel.Error:
                    Debug.LogError(formattedMessage);
                    break;
                default:
                    throw new System.ArgumentException($"Unsupported log level: {logLevel}");
            }
        }

        /// <summary>
        /// 获取日志级别对应的颜色
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <returns>颜色字符串</returns>
        private static string GetLogLevelColor(ELogLevel logLevel)
        {
            switch (logLevel)
            {
                case ELogLevel.Debug: return "white"; // Debug 级别显示白色
                case ELogLevel.Info: return "green"; // Info 级别显示绿色
                case ELogLevel.Warning: return "yellow"; // Warning 级别显示黄色
                case ELogLevel.Error: return "red"; // Error 级别显示红色
                default: return "white"; // 默认白色
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