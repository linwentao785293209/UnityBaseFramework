using BaseFramework;

namespace BaseFrameworkTest
{
    public abstract class TestAbstractCSharpSingleton : CSharpSingleton<TestAbstractCSharpSingleton>
    {
        private string _name;

        private TestAbstractCSharpSingleton()
        {
            _name = nameof(TestAbstractCSharpSingleton);
        }

        public void TestMethod()
        {
            Log.LogDebug($"C#抽象类单例测试方法 类名是: {_name}");
        }
    }
}