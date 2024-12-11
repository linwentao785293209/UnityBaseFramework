using BaseFramework;


namespace BaseFrameworkTest
{
    public sealed class TestCSharpSingleton : CSharpSingleton<TestCSharpSingleton>
    {
        private string _name;

        private TestCSharpSingleton()
        {
            _name = nameof(TestCSharpSingleton);
        }

        public void TestMethod()
        {
            Log.LogDebug($"C#单例测试方法 类名是: {_name}");
        }
    }
}