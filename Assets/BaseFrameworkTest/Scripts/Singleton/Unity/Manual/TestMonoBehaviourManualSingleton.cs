﻿using BaseFramework;
using UnityEngine;

namespace BaseFrameworkTest
{
    [DisallowMultipleComponent]
    public sealed class TestMonoBehaviourManualSingleton : MonoBehaviourManualSingleton<TestMonoBehaviourManualSingleton>
    {
        private string _name;

        protected override void Awake()
        {
            base.Awake();
            _name = nameof(TestCSharpSingleton);
        }

        public void TestMethod()
        {
            Log.LogDebug($"MonoBehaviour手动单例基类测试方法 类名是: {_name}");
        }
    }
}