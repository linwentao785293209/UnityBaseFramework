using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BaseFramework
{
    /// <summary>
    /// 二进制数据管理器
    /// </summary>
    public class BinaryDataManager : DataManagerBase<BinaryDataManager>
    {
        protected override string DataString => Const.Binary;
        protected override EDataType DataType => EDataType.Binary;
        protected override string DataExtension => "tao";

        private BinaryDataManager()
        {
        }

        protected override void OnSave<TData>(string key, TData value)
        {
            OnSave(key, (object)value);
        }

        protected override void OnSave(string key, object value)
        {
            string path = Path.Combine(PersistentDataPath, $"{key}.{DataExtension}");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, value);
                }
            }
            catch (Exception e)
            {
                Log.LogError($"保存二进制数据失败，错误信息：{e.Message}\n{e.StackTrace}");
            }
        }

        protected override TData OnLoad<TData>(string key)
        {
            return (TData)OnLoad(key, typeof(TData));
        }

        protected override object OnLoad(string key, Type type)
        {
            string path = Path.Combine(PersistentDataPath, $"{key}.{DataExtension}");
            if (!File.Exists(path))
            {
                path = Path.Combine(StreamingAssetsPath, $"{key}.{DataExtension}");
                if (!File.Exists(path))
                {
                    Log.LogWarning($"文件 {key} 未找到, 返回默认实例！");
                    return Activator.CreateInstance(type);
                }
            }
            object value;
            try
            {
                using FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                value = binaryFormatter.Deserialize(fileStream);
            }
            catch (Exception e)
            {
                Log.LogError($"加载二进制数据失败，错误信息：{e.Message}\n{e.StackTrace}");
                return Activator.CreateInstance(type);
            }

            if (type.IsInstanceOfType(value))
            {
                return value;
            }
            Log.LogWarning($"Type {type} 不匹配, 返回默认实例！");
            return Activator.CreateInstance(type);
        }

        protected override bool OnDelete(string key)
        {
            string path = Path.Combine(PersistentDataPath, $"{key}.{DataExtension}");
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                    return true;
                }
                catch (Exception e)
                {
                    Log.LogError($"删除二进制文件失败，错误信息：{e.Message}\n{e.StackTrace}");
                    return false;
                }
            }
            Log.LogWarning($"文件 {key} 不存在，无法删除。");
            return false;
        }

        protected override void OnClear()
        {
            string[] files = Directory.GetFiles(PersistentDataPath, $"*.{DataExtension}");
            foreach (string file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception e)
                {
                    Log.LogError($"清理二进制文件失败，错误信息：{e.Message}\n{e.StackTrace}");
                }
            }

            Log.LogInfo($"已清理 {files.Length} 个二进制文件。");
        }
    }
}