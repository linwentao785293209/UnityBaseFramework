using System.Collections.Generic;
using BaseFramework;

namespace BaseFrameworkTest
{
    public class TestXmlClass
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
        public char charValue = 'A';
        public string stringValue = "Hello, Xml!";

        // 自定义数据类型
        public TestXmlItemClass ItemClass = new TestXmlItemClass(123, 456);

        // 集合类型
        public int[] intArray = new[] { 1, 2, 3, 4, 5 };

        public TestXmlItemClass[] XmlTestItemClassArray = new[]
        {
            new TestXmlItemClass(123, 456),
            new TestXmlItemClass(789, 101112)
        };

        public List<int> intList = new List<int>() { 1, 2, 3, 4, 5 };

        public XmlSerializeDictionary<int, string> intStringDict = new XmlSerializeDictionary<int, string>()
        {
            { 1, "One" },
            { 2, "Two" },
            { 3, "Three" }
        };

        public List<TestXmlItemClass> testItemList = new List<TestXmlItemClass>()
        {
            new TestXmlItemClass(111, 222),
            new TestXmlItemClass(333, 444)
        };

        public XmlSerializeDictionary<int, TestXmlItemClass> testItemDict =
            new XmlSerializeDictionary<int, TestXmlItemClass>()
            {
                { 1001, new TestXmlItemClass(999, 888) },
                { 1002, new TestXmlItemClass(777, 666) }
            };
    }
}