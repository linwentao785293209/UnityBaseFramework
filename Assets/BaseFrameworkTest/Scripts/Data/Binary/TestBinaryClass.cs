using System;
using System.Collections.Generic;
using BaseFramework;
using UnityEngine.Serialization;

namespace BaseFrameworkTest
{
    // 用于二进制序列化，需要加特性
    [System.Serializable]
    public class TestBinaryClass
    {
        // 有符号类型
        public sbyte sbyteValue = -50;
        public int intValue = -123456789;
        public short shortValue = -30000;
        public long longValue = -98765432101234L;

        // 无符号类型
        public byte byteValue = 100;
        public uint uintValue = 4294967295;
        public ushort ushortValue = 60000;
        public ulong ulongValue = 18446744073709551615;

        // 浮点数
        public float floatValue = 3.14f;
        public double doubleValue = 1234567890.123456789;
        public decimal decimalValue = 1234567890123456789012345678.12345678m;

        // 特殊类型
        public bool boolValue = true;
        private bool boolValue2 = false;
        public char charValue = 'A';
        public string stringValue = "Hello, Binary!";

        // 自定义数据类型
        [FormerlySerializedAs("testItemClass")] public TestBinaryItemClass itemClass = new TestBinaryItemClass(123, 456);

        // 集合类型
        public int[] intArray = new[] { 1, 2, 3, 4, 5 };

        public TestBinaryItemClass[] BinaryTestItemClassArray = new[]
        {
            new TestBinaryItemClass(123, 456),
            new TestBinaryItemClass(789, 101112)
        };

        public List<int> intList = new List<int>() { 1, 2, 3, 4, 5 };

        //只支持字符串类型的字典
        public Dictionary<string, string> intStringDict = new Dictionary<string, string>()
        {
            { "1", "One" },
            { "2", "Two" },
            { "3", "Three" }
        };

        public List<TestBinaryItemClass> testItemList = new List<TestBinaryItemClass>()
        {
            new TestBinaryItemClass(111, 222),
            new TestBinaryItemClass(333, 444)
        };

        //只支持字符串类型的字典
        public Dictionary<string, TestBinaryItemClass> testItemDict = new Dictionary<string, TestBinaryItemClass>()
        {
            { "1001", new TestBinaryItemClass(999, 888) },
            { "1002", new TestBinaryItemClass(777, 666) }
        };

        public void LogBool2()
        {
            Log.LogDebug($"bool2Value: {boolValue2}");
        }
    }
}