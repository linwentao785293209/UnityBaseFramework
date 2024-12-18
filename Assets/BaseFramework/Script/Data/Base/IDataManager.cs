using System;

namespace BaseFramework
{
    public interface IDataManager
    {
        void Save<TData>(string key, TData value);

        void Save(string key, object value);

        TData Load<TData>(string key);

        object Load(string key, Type type);

        public bool Delete(string key);

        public void Clear();
    }
}