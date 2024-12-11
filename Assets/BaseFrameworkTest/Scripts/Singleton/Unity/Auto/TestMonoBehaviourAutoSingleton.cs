using BaseFramework;

namespace BaseFrameworkTest
{
    public sealed class TestMonoBehaviourAutoSingleton : MonoBehaviourAutoSingleton<TestMonoBehaviourAutoSingleton>
    {
        private string _name;

        protected override void Awake()
        {
            base.Awake();
            _name = nameof(TestCSharpSingleton);
        }

        public void TestMethod()
        {
            Log.LogDebug($"MonoBehaviour自动单例基类测试方法 类名是: {_name}");
        }
    }
}