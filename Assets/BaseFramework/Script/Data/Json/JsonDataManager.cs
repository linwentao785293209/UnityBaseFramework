using System;
using System.IO;
using UnityEngine;
using LitJson;
using Newtonsoft.Json;

namespace BaseFramework
{
    /// <summary>
    /// JSON 数据管理器
    /// </summary>
    public class JsonDataManager : BaseDataManager<JsonDataManager>
    {
        private EJsonType _jsonType = EJsonType.NewtonsoftJson;
        protected override string DataString => Const.Json;
        protected override EDataType DataType => EDataType.Json;
        protected override string DataExtension => "json";

        private JsonDataManager()
        {
        }

        public void SetJsonType(EJsonType jsonType)
        {
            _jsonType = jsonType;
        }

        protected override void OnSave<TData>(string key, TData value)
        {
            OnSave(key, (object)value);
        }

        protected override void OnSave(string key, object value)
        {
            string path = Path.Combine(PersistentDataPath, $"{key}.{DataExtension}");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            string jsonStr = Serialize(value);
            try
            {
                File.WriteAllText(path, jsonStr);
            }
            catch (Exception e)
            {
                Log.LogError($"保存 JSON 数据失败，错误信息：{e.Message}\n{e.StackTrace}");
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
            string jsonStr = File.ReadAllText(path);
            return Deserialize(jsonStr, type);
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
                    Log.LogError($"删除 JSON 文件失败，错误信息：{e.Message}\n{e.StackTrace}");
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
                    Log.LogError($"清理 JSON 文件失败，错误信息：{e.Message}\n{e.StackTrace}");
                }
            }

            Log.LogInfo($"已清理 {files.Length} 个 JSON 文件。");
        }

        private string Serialize(object value)
        {
            return _jsonType switch
            {
                EJsonType.JsonUtility => JsonUtility.ToJson(value),
                EJsonType.LitJson => JsonMapper.ToJson(value),
                EJsonType.NewtonsoftJson => JsonConvert.SerializeObject(value),
                _ => throw new NotSupportedException($"不支持的 JSON 类型: {_jsonType}")
            };
        }

        private object Deserialize(string jsonStr, Type type)
        {
            return _jsonType switch
            {
                EJsonType.JsonUtility => JsonUtility.FromJson(jsonStr, type),
                EJsonType.LitJson => JsonMapper.ToObject(jsonStr, type),
                EJsonType.NewtonsoftJson => JsonConvert.DeserializeObject(jsonStr, type),
                _ => throw new NotSupportedException($"不支持的 JSON 类型: {_jsonType}")
            };
        }
    }
}