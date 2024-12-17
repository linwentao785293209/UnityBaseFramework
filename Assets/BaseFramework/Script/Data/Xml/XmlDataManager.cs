using System;
using System.Collections.Concurrent;
using System.IO;
using System.Xml.Serialization;


namespace BaseFramework
{
    /// <summary>
    /// Xml 数据管理器
    /// </summary>
    public class XmlDataManager : DataManagerBase<XmlDataManager>
    {
        protected override string DataString => Const.Xml;
        protected override EDataType DataType => EDataType.Xml;
        protected override string DataExtension => "xml";
        private readonly ConcurrentDictionary<Type, XmlSerializer> _xmlSerializerCache =
            new ConcurrentDictionary<Type, XmlSerializer>();

        private XmlDataManager()
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
                using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    var xmlSerializer = GetSerializer(value.GetType());
                    xmlSerializer.Serialize(fileStream, value);
                }
            }
            catch (Exception e)
            {
                Log.LogError($"保存 XML 数据失败，错误信息：{e.Message}\n{e.StackTrace}");
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
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var xmlSerializer = GetSerializer(type);
                return xmlSerializer.Deserialize(fileStream);
            }
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
                    Log.LogError($"删除 XML 文件失败，错误信息：{e.Message}\n{e.StackTrace}");
                    return false;
                }
            }
            else
            {
                Log.LogWarning($"文件 {key} 不存在，无法删除。");
                return false;
            }
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
                    Log.LogError($"清理 XML 文件失败，错误信息：{e.Message}\n{e.StackTrace}");
                }
            }

            Log.LogInfo($"已清理 {files.Length} 个 XML 文件。");
        }

        private XmlSerializer GetSerializer(Type type)
        {
            return _xmlSerializerCache.GetOrAdd(type, t => new XmlSerializer(t));
        }
    }
}