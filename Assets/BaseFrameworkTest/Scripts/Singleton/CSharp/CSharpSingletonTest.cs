using System;
using UnityEngine;

namespace BaseFrameworkTest
{
    public class CSharpSingletonTest : MonoBehaviour
    {
        void Update()
        {
            // 测试方法 正常调用
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TestCSharpSingleton.Instance.TestMethod();
            }

            // Activator反射实例化 报错
            if (Input.GetKeyDown(KeyCode.W))
            {
                TestCSharpSingleton testCSharpSingleton =
                    Activator.CreateInstance(typeof(TestCSharpSingleton), true) as
                        TestCSharpSingleton;

                testCSharpSingleton.TestMethod();
            }

            // Constructor反射实例化 报错
            if (Input.GetKeyDown(KeyCode.E))
            {
                var type = typeof(TestCSharpSingleton);

                var constructorInfo = type.GetConstructor(
                    System.Reflection.BindingFlags.Instance
                    | System.Reflection.BindingFlags.NonPublic
                    | System.Reflection.BindingFlags.Public,
                    null,
                    Type.EmptyTypes,
                    null
                );

                TestCSharpSingleton testCSharpSingleton =
                    (TestCSharpSingleton)constructorInfo.Invoke(null);

                testCSharpSingleton.TestMethod();
            }

            // 抽象类尝试获取单例 报错
            if (Input.GetKeyDown(KeyCode.R))
            {
                TestAbstractCSharpSingleton.Instance.TestMethod();
            }
        }
    }
}