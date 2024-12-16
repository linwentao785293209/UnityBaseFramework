using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BaseFramework
{
    public class EventManager : CSharpSingleton<EventManager>
    {
        private readonly Dictionary<System.Enum, IEventInfo> _eventInfoDictionary =
            new Dictionary<System.Enum, IEventInfo>();

        private EventManager()
        {
        }

        /// <summary>
        /// 触发无参数事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        public void TriggerEvent(System.Enum eventType)
        {
            if (_eventInfoDictionary.TryGetValue(eventType, out var eventInfo))
            {
                (eventInfo as EventInfo)?.Actions?.Invoke();
            }
            else
            {
                Log.LogWarning($"事件 {eventType} 尚未注册");
            }
        }

        /// <summary>
        /// 触发带参数事件
        /// </summary>
        /// <typeparam name="T">事件参数类型</typeparam>
        /// <param name="eventType">事件类型</param>
        /// <param name="info">事件参数</param>
        public void TriggerEvent<T>(System.Enum eventType, T info)
        {
            if (_eventInfoDictionary.TryGetValue(eventType, out var eventInfo))
            {
                (eventInfo as EventInfo<T>)?.Actions?.Invoke(info);
            }
            else
            {
                Log.LogWarning($"事件 {eventType} 尚未注册");
            }
        }

        /// <summary>
        /// 添加无参数事件监听
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="action">事件回调</param>
        public void AddEventListener(System.Enum eventType, UnityAction action)
        {
            if (_eventInfoDictionary.TryGetValue(eventType, out var eventInfo))
            {
                if (eventInfo is EventInfo info)
                {
                    info.Actions += action;
                }
                else
                {
                    Log.LogError($"事件 {eventType} 类型转换出错");
                }
            }
            else
            {
                _eventInfoDictionary.Add(eventType, new EventInfo(action));
            }
        }

        /// <summary>
        /// 添加带参数事件监听
        /// </summary>
        /// <typeparam name="T">事件参数类型</typeparam>
        /// <param name="eventType">事件类型</param>
        /// <param name="action">事件回调</param>
        public void AddEventListener<T>(System.Enum eventType, UnityAction<T> action)
        {
            if (_eventInfoDictionary.TryGetValue(eventType, out var eventInfo))
            {
                if (eventInfo is EventInfo<T> info)
                {
                    info.Actions += action;
                }
                else
                {
                    Log.LogError($"事件 {eventType} 类型转换出错");
                }
            }
            else
            {
                _eventInfoDictionary.Add(eventType, new EventInfo<T>(action));
            }
        }


        /// <summary>
        /// 移除无参数事件监听
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="action">事件回调</param>
        public void RemoveEventListener(System.Enum eventType, UnityAction action)
        {
            if (_eventInfoDictionary.TryGetValue(eventType, out var eventInfo))
            {
                if (eventInfo is EventInfo info)
                {
                    info.Actions -= action;
                }
                else
                {
                    Log.LogError($"事件 {eventType} 类型转换出错");
                }
            }
        }

        /// <summary>
        /// 移除带参数事件监听
        /// </summary>
        /// <typeparam name="T">事件参数类型</typeparam>
        /// <param name="eventType">事件类型</param>
        /// <param name="action">事件回调</param>
        public void RemoveEventListener<T>(System.Enum eventType, UnityAction<T> action)
        {
            if (_eventInfoDictionary.TryGetValue(eventType, out var eventInfo))
            {
                if (eventInfo is EventInfo<T> info)
                {
                    info.Actions -= action;
                }
                else
                {
                    Log.LogError($"事件 {eventType} 类型转换出错");
                }
            }
        }


        /// <summary>
        /// 清空所有事件监听
        /// </summary>
        public void Clear()
        {
            _eventInfoDictionary.Clear();
        }

        /// <summary>
        /// 清空指定事件监听
        /// </summary>
        /// <param name="eventName">事件类型</param>
        public void Clear(System.Enum eventName)
        {
            if (_eventInfoDictionary.ContainsKey(eventName))
            {
                _eventInfoDictionary.Remove(eventName);
            }
        }
    }
}