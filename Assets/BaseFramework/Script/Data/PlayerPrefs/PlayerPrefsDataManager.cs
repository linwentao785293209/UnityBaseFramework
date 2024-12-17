﻿using System;
using UnityEngine;
using Newtonsoft.Json;


namespace BaseFramework
{
    /// <summary>
    /// PlayerPrefs 数据管理器
    /// </summary>
    public sealed class PlayerPrefsDataManager : DataManagerBase<PlayerPrefsDataManager>
    {
        protected override string DataString => Const.PlayerPrefs;
        protected override EDataType DataType => EDataType.PlayerPrefs;
        protected override string DataExtension => "";

        private PlayerPrefsDataManager()
        {
        }

        protected override void OnSave<TData>(string key, TData value)
        {
            SaveToPlayerPrefs(key, value);
        }

        protected override void OnSave(string key, object value)
        {
            SaveToPlayerPrefs(key, value);
        }

        private void SaveToPlayerPrefs<TData>(string key, TData value)
        {
            Type type = value.GetType();
            try
            {
                var jsonData = JsonConvert.SerializeObject(value);
                PlayerPrefs.SetString(key, jsonData);
                PlayerPrefs.Save();
            }
            catch (Exception ex)
            {
                Log.LogError($"存储时错误序列化 key：'{key}' 类型：'{type}' 异常信息：{ex.Message}");
            }
        }

        protected override TData OnLoad<TData>(string key)
        {
            return (TData)LoadFromPlayerPrefs(key, typeof(TData));
        }

        protected override object OnLoad(string key, Type type)
        {
            return LoadFromPlayerPrefs(key, type);
        }

        private object LoadFromPlayerPrefs(string key, Type type)
        {
            var jsonData = PlayerPrefs.GetString(key, string.Empty);
            if (!string.IsNullOrEmpty(jsonData))
            {
                try
                {
                    return JsonConvert.DeserializeObject(jsonData, type);
                }
                catch (JsonException ex)
                {
                    Log.LogError($"读取时错误反序列化 key：'{key}' 类型：'{type}' 异常信息：{ex.Message}");
                }
            }

            Log.LogWarning($"读取时错误反序列化，返回默认类型");
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        protected override bool OnDelete(string key)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                Log.LogWarning($"PlayerPrefs不存在键'{key}'，删除失败。");
                return false;
            }

            PlayerPrefs.DeleteKey(key);
            return true;
        }

        protected override void OnClear()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}