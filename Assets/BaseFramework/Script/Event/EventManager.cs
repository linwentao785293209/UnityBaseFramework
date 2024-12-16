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
        /// �����޲����¼�
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        public void TriggerEvent(System.Enum eventType)
        {
            if (_eventInfoDictionary.TryGetValue(eventType, out var eventInfo))
            {
                (eventInfo as EventInfo)?.Actions?.Invoke();
            }
            else
            {
                Log.LogWarning($"�¼� {eventType} ��δע��");
            }
        }

        /// <summary>
        /// �����������¼�
        /// </summary>
        /// <typeparam name="T">�¼���������</typeparam>
        /// <param name="eventType">�¼�����</param>
        /// <param name="info">�¼�����</param>
        public void TriggerEvent<T>(System.Enum eventType, T info)
        {
            if (_eventInfoDictionary.TryGetValue(eventType, out var eventInfo))
            {
                (eventInfo as EventInfo<T>)?.Actions?.Invoke(info);
            }
            else
            {
                Log.LogWarning($"�¼� {eventType} ��δע��");
            }
        }

        /// <summary>
        /// ����޲����¼�����
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="action">�¼��ص�</param>
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
                    Log.LogError($"�¼� {eventType} ����ת������");
                }
            }
            else
            {
                _eventInfoDictionary.Add(eventType, new EventInfo(action));
            }
        }

        /// <summary>
        /// ��Ӵ������¼�����
        /// </summary>
        /// <typeparam name="T">�¼���������</typeparam>
        /// <param name="eventType">�¼�����</param>
        /// <param name="action">�¼��ص�</param>
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
                    Log.LogError($"�¼� {eventType} ����ת������");
                }
            }
            else
            {
                _eventInfoDictionary.Add(eventType, new EventInfo<T>(action));
            }
        }


        /// <summary>
        /// �Ƴ��޲����¼�����
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="action">�¼��ص�</param>
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
                    Log.LogError($"�¼� {eventType} ����ת������");
                }
            }
        }

        /// <summary>
        /// �Ƴ��������¼�����
        /// </summary>
        /// <typeparam name="T">�¼���������</typeparam>
        /// <param name="eventType">�¼�����</param>
        /// <param name="action">�¼��ص�</param>
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
                    Log.LogError($"�¼� {eventType} ����ת������");
                }
            }
        }


        /// <summary>
        /// ��������¼�����
        /// </summary>
        public void Clear()
        {
            _eventInfoDictionary.Clear();
        }

        /// <summary>
        /// ���ָ���¼�����
        /// </summary>
        /// <param name="eventName">�¼�����</param>
        public void Clear(System.Enum eventName)
        {
            if (_eventInfoDictionary.ContainsKey(eventName))
            {
                _eventInfoDictionary.Remove(eventName);
            }
        }
    }
}