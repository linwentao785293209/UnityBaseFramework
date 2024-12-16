using UnityEngine.Events;

namespace BaseFramework
{
    /// <summary>
    /// 有参事件信息
    /// </summary>
    /// <typeparam name="T">参数类型</typeparam>
    internal class EventInfo<T> : IEventInfo
    {
        /// <summary>
        /// 事件委托
        /// </summary>
        public UnityAction<T> Actions;

        public EventInfo(UnityAction<T> action)
        {
            Actions += action;
        }
    }

    /// <summary>
    /// 无参事件信息
    /// </summary>
    internal class EventInfo : IEventInfo
    {
        /// <summary>
        /// 事件委托
        /// </summary>
        public UnityAction Actions;

        public EventInfo(UnityAction action)
        {
            Actions += action;
        }
    }
}