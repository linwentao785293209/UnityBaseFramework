using BaseFramework;
using UnityEngine;


namespace BaseFrameworkTest
{
    public class XmlTest : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TestXmlClass testXmlClass1 = new TestXmlClass();
                testXmlClass1.intValue = 250;
                XmlDataManager.Instance.Save<TestXmlClass>("myXmlTestClass",
                    testXmlClass1);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                TestXmlClass testXmlClass2 =
                    XmlDataManager.Instance.Load<TestXmlClass>("myXmlTestClass");

                if (testXmlClass2 != null)
                {
                    // 打印各个类型的值
                    Log.LogDebug($"sbyteValue: {testXmlClass2.sbyteValue}");
                    Log.LogDebug($"intValue: {testXmlClass2.intValue}");
                    Log.LogDebug($"shortValue: {testXmlClass2.shortValue}");
                    Log.LogDebug($"longValue: {testXmlClass2.longValue}");
                    Log.LogDebug($"byteValue: {testXmlClass2.byteValue}");
                    Log.LogDebug($"uintValue: {testXmlClass2.uintValue}");
                    Log.LogDebug($"ushortValue: {testXmlClass2.ushortValue}");
                    Log.LogDebug($"ulongValue: {testXmlClass2.ulongValue}");
                    Log.LogDebug($"floatValue: {testXmlClass2.floatValue}");
                    Log.LogDebug($"doubleValue: {testXmlClass2.doubleValue}");
                    Log.LogDebug($"decimalValue: {testXmlClass2.decimalValue}");
                    Log.LogDebug($"boolValue: {testXmlClass2.boolValue}");
                    Log.LogDebug($"charValue: {testXmlClass2.charValue}");
                    Log.LogDebug($"stringValue: {testXmlClass2.stringValue}");
                    Log.LogDebug($"testItemClass: {testXmlClass2.ItemClass}");

                    // 打印数组
                    Log.LogDebug("intArray:");
                    foreach (var item in testXmlClass2.intArray)
                    {
                        Log.LogDebug($"  {item}");
                    }

                    // 打印数组
                    Log.LogDebug("XmlTestItemClassArray:");
                    foreach (var item in testXmlClass2.XmlTestItemClassArray)
                    {
                        Log.LogDebug($"  {item}");
                    }

                    // 打印列表
                    Log.LogDebug("intList:");
                    foreach (var item in testXmlClass2.intList)
                    {
                        Log.LogDebug($"  {item}");
                    }

                    // 打印字典
                    Log.LogDebug("intStringDict:");
                    foreach (var item in testXmlClass2.intStringDict)
                    {
                        Log.LogDebug($"  Key: {item.Key}, Value: {item.Value}");
                    }

                    // 打印列表
                    Log.LogDebug("testItemList:");
                    foreach (var item in testXmlClass2.testItemList)
                    {
                        Log.LogDebug("testItemList item: id = " + item.id + ", num = " + item.num);
                    }

                    // 打印字典
                    Log.LogDebug("testItemDict:");
                    foreach (var item in testXmlClass2.testItemDict)
                    {
                        Log.LogDebug($"  Key: {item.Key}, Value: {item.Value} id：{item.Value.id} num：{item.Value.num}");
                    }
                }
                else
                {
                    Log.LogError("加载 XmlTestClass 失败.");
                }

                Log.LogDebug($"{XmlDataManager.Instance.PersistentDataPath}");
            }
        }
    }
}