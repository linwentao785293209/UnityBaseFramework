using System;

namespace BaseFramework
{
    /// <summary>
    /// 数据验证器，提供通用的参数验证方法。
    /// </summary>
    public static class DataValidator
    {
        public static void ValidateKey(string key)
        {
            if (!string.IsNullOrEmpty(key)) return;
            Log.LogError("Key不能为空");
            throw new ArgumentException("Key不能为空");
        }

        public static void ValidateValue(object value)
        {
            if (value != null) return;
            Log.LogError("Value不能为空");
            throw new ArgumentException("Value不能为空");
        }

        public static void ValidateType(Type type)
        {
            if (type != null) return;
            Log.LogError("Type不能为空");
            throw new ArgumentException("Type不能为空");
        }
    }
}