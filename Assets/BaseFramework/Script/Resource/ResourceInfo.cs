using UnityEngine.Events;

namespace BaseFramework
{
    /// <summary>
    /// 泛型资源信息类，存储具体资源对象及回调
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    public class ResourceInfo<T> : ResourceInfoBase
    {
        public T Asset;
        public UnityAction<bool, T> CallBack;
        public bool IsLoading => Asset == null && Coroutine != null;
    }
}