using System;
using UnityEngine;

namespace BaseFrameworkTest
{
    public class MonoBehaviourAutoSingletonTest : MonoBehaviour
    {
        void Update()
        {
            // 测试方法 正常调用
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TestMonoBehaviourAutoSingleton.Instance.TestMethod();
            }

            // 尝试动态添加 添加组件瞬间被删除 报错
            if (Input.GetKeyDown(KeyCode.W))
            {
                GameObject gameObj = new GameObject
                {
                    name = nameof(TestMonoBehaviourAutoSingleton) + " AddComponent"
                };

                TestMonoBehaviourAutoSingleton testMonoBehaviourAutoSingleton =
                    gameObj.AddComponent<TestMonoBehaviourAutoSingleton>();

                testMonoBehaviourAutoSingleton.TestMethod();
            }
        }
    }
}