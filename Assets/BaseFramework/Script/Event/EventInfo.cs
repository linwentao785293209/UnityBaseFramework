using UnityEngine.Events;

namespace BaseFramework
{
    internal class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> Actions;

        public EventInfo(UnityAction<T> action)
        {
            Actions += action;
        }
    }


    internal class EventInfo : IEventInfo
    {
        public UnityAction Actions;

        public EventInfo(UnityAction action)
        {
            Actions += action;
        }
    }
}