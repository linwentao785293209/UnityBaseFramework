using System.Collections;
using System.Collections.Generic;
using BaseFramework;
using UnityEngine;

namespace BaseFrameworkTest
{
    public class EventTest : MonoBehaviour
    {
        void Start()
        {
            // 添加无参事件监听
            EventManager.Instance.AddEventListener(ETestEventType.TestEventType1, TestEventType11);
            EventManager.Instance.AddEventListener(ETestEventType.TestEventType1, TestEventType12);

            // 添加带参事件监听
            EventManager.Instance.AddEventListener<float>(ETestEventType.TestEventType2, TestEventType21);
            EventManager.Instance.AddEventListener<float>(ETestEventType.TestEventType2, TestEventType22);

            // 添加多个类型的事件监听
            EventManager.Instance.AddEventListener<int>(ETestEventType.TestEventType3, TestEventType3);
            EventManager.Instance.AddEventListener<string>(ETestEventType.TestEventType4, TestEventType4);
        }

        void Update()
        {
            // 测试触发无参事件
            if (Input.GetKeyDown(KeyCode.Q))
            {
                EventManager.Instance.TriggerEvent(ETestEventType.TestEventType1);
            }

            // 测试触发带 float 参数事件
            if (Input.GetKeyDown(KeyCode.W))
            {
                EventManager.Instance.TriggerEvent(ETestEventType.TestEventType2, Random.Range(0f, 100000f));
            }

            // 测试触发带 int 参数事件
            if (Input.GetKeyDown(KeyCode.E))
            {
                EventManager.Instance.TriggerEvent(ETestEventType.TestEventType3, Random.Range(1, 100));
            }

            // 测试触发带 string 参数事件
            if (Input.GetKeyDown(KeyCode.R))
            {
                EventManager.Instance.TriggerEvent(ETestEventType.TestEventType4, "Hello EventManager!");
            }

            // 移除无参事件监听
            if (Input.GetKeyDown(KeyCode.A))
            {
                EventManager.Instance.RemoveEventListener(ETestEventType.TestEventType1, TestEventType11);
            }

            // 移除带参事件监听
            if (Input.GetKeyDown(KeyCode.S))
            {
                EventManager.Instance.RemoveEventListener<float>(ETestEventType.TestEventType2, TestEventType22);
            }

            // 清空指定事件类型监听
            if (Input.GetKeyDown(KeyCode.D))
            {
                EventManager.Instance.Clear(ETestEventType.TestEventType3);
                Log.LogDebug("事件 TestEventType3 的监听已清空");
            }

            // 清空所有事件监听
            if (Input.GetKeyDown(KeyCode.F))
            {
                EventManager.Instance.Clear();
                Log.LogDebug("所有事件已清空");
            }
        }

        void TestEventType11()
        {
            Log.LogDebug("TestEventType11: 无参事件触发");
        }

        void TestEventType12()
        {
            Log.LogDebug("TestEventType12: 无参事件触发");
        }

        void TestEventType21(float damage)
        {
            Log.LogDebug($"TestEventType21: 带参事件触发，造成伤害 {damage}");
        }

        void TestEventType22(float damage)
        {
            Log.LogDebug($"TestEventType22: 带参事件触发，额外伤害 {damage}");
        }

        void TestEventType3(int value)
        {
            Log.LogDebug($"TestEventType3: 带 int 参数事件触发，数值 {value}");
        }

        void TestEventType4(string message)
        {
            Log.LogDebug($"TestEventType4: 带 string 参数事件触发，消息 {message}");
        }
    }
}