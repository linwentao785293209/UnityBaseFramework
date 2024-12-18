using UnityEngine.Events;

namespace BaseFramework
{
    public class ResourceInfo<T> : ResourceInfoBase
    {
        public T Asset;
        public UnityAction<bool, T> CallBack;
        public bool IsLoading => Asset == null && Coroutine != null;
    }
}