using System;
using UnityEngine;

namespace BaseFramework
{
    public abstract class DataManagerBase<T> : CSharpSingleton<T>, IDataManager, IStreamingAssetsPath,
        IPersistentDataPath
        where T : DataManagerBase<T>
    {
        public string StreamingAssetsPath => $"{Application.streamingAssetsPath}/{Const.Data}/{DataString}/";
        public string PersistentDataPath => $"{Application.persistentDataPath}/{Const.Data}/{DataString}/";

        protected abstract string DataString { get; }
        protected abstract EDataType DataType { get; }
        protected abstract string DataExtension { get; }

        public void Save<TData>(string key, TData value)
        {
            DataValidator.ValidateKey(key);
            DataValidator.ValidateValue(value);
            OnSave(key, value);
        }

        public void Save(string key, object value)
        {
            DataValidator.ValidateKey(key);
            DataValidator.ValidateValue(value);
            OnSave(key, value);
        }

        public TData Load<TData>(string key)
        {
            DataValidator.ValidateKey(key);
            return OnLoad<TData>(key);
        }

        public object Load(string key, Type type)
        {
            DataValidator.ValidateKey(key);
            DataValidator.ValidateType(type);
            return OnLoad(key, type);
        }

        public bool Delete(string key)
        {
            DataValidator.ValidateKey(key);
            return OnDelete(key);
        }

        public void Clear()
        {
            OnClear();
        }

        protected abstract void OnSave<TData>(string key, TData value);

        protected abstract void OnSave(string key, object value);

        protected abstract TData OnLoad<TData>(string key);

        protected abstract object OnLoad(string key, Type type);

        protected abstract bool OnDelete(string key);

        protected abstract void OnClear();
    }
}