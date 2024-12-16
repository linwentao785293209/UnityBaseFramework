using BaseFramework;
using UnityEngine;

namespace BaseFrameworkTest
{
    public class JsonTest : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TestJsonClass testJsonClass1 = new TestJsonClass();
                testJsonClass1.intValue = 666;
                testJsonClass1.intStringDict.Add("6666666", "6666666");

                // 使用 JsonUtility 存储
                JsonDataManager.Instance.SetJsonType(EJsonType.JsonUtility);
                JsonDataManager.Instance.Save<TestJsonClass>("myJsonTestClass_JsonUtility", testJsonClass1);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                TestJsonClass testJsonClass1 = new TestJsonClass();
                testJsonClass1.intValue = 999;
                testJsonClass1.intStringDict.Add("99999999", "99999999");

                // 使用 LitJson 存储
                JsonDataManager.Instance.SetJsonType(EJsonType.LitJson);
                JsonDataManager.Instance.Save<TestJsonClass>("myJsonTestClass_LitJson", testJsonClass1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                TestJsonClass testJsonClass1 = new TestJsonClass();
                testJsonClass1.intValue = 888;
                testJsonClass1.intStringDict.Add("88888888", "88888888");

                // 使用 Newtonsoft.Json 存储
                JsonDataManager.Instance.SetJsonType(EJsonType.NewtonsoftJson);
                JsonDataManager.Instance.Save<TestJsonClass>("myJsonTestClass_NewtonsoftJson", testJsonClass1);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                // 使用 JsonUtility 加载
                JsonDataManager.Instance.SetJsonType(EJsonType.JsonUtility);
                TestJsonClass testJsonClass2TestJsonUtility =
                    JsonDataManager.Instance.Load<TestJsonClass>("myJsonTestClass_JsonUtility");

                if (testJsonClass2TestJsonUtility != null)
                {
                    // 打印所有属性的值
                    PrintJsonTestClassValues(testJsonClass2TestJsonUtility, "JsonUtility");
                }
                else
                {
                    Log.LogError("加载 JsonTestClass (JsonUtility) 失败.");
                }
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                // 使用 LitJson 加载
                JsonDataManager.Instance.SetJsonType(EJsonType.LitJson);
                TestJsonClass testJsonClass2LitTestJson =
                    JsonDataManager.Instance.Load<TestJsonClass>("myJsonTestClass_LitJson");

                if (testJsonClass2LitTestJson != null)
                {
                    // 打印所有属性的值
                    PrintJsonTestClassValues(testJsonClass2LitTestJson, "LitJson");
                }
                else
                {
                    Log.LogError("加载 JsonTestClass (LitJson) 失败.");
                }
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                // 使用 Newtonsoft.Json 加载
                JsonDataManager.Instance.SetJsonType(EJsonType.NewtonsoftJson);
                TestJsonClass testJsonClass2NewtonsoftTestJson =
                    JsonDataManager.Instance.Load<TestJsonClass>("myJsonTestClass_NewtonsoftJson");

                if (testJsonClass2NewtonsoftTestJson != null)
                {
                    // 打印所有属性的值
                    PrintJsonTestClassValues(testJsonClass2NewtonsoftTestJson, "NewtonsoftJson");
                }
                else
                {
                    Log.LogError("加载 JsonTestClass (NewtonsoftJson) 失败.");
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Log.LogDebug($"{JsonDataManager.Instance.PersistentDataPath}");
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                JsonDataManager.Instance.Delete("myJsonTestClass_JsonUtility");
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                JsonDataManager.Instance.Delete("myJsonTestClass_LitJson");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                JsonDataManager.Instance.Delete("myJsonTestClass_NewtonsoftJson");
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                JsonDataManager.Instance.Clear();
            }
        }

        // 打印 JsonTestClass 中所有属性的值
        private void PrintJsonTestClassValues(TestJsonClass testJsonClass, string jsonType)
        {
            Log.LogDebug($"Printing values for JsonTestClass using {jsonType}:");

            // 打印各个类型的值
            Log.LogDebug($"sbyteValue: {testJsonClass.sbyteValue}");
            Log.LogDebug($"intValue: {testJsonClass.intValue}");
            Log.LogDebug($"shortValue: {testJsonClass.shortValue}");
            Log.LogDebug($"longValue: {testJsonClass.longValue}");
            Log.LogDebug($"byteValue: {testJsonClass.byteValue}");
            Log.LogDebug($"uintValue: {testJsonClass.uintValue}");
            Log.LogDebug($"ushortValue: {testJsonClass.ushortValue}");
            Log.LogDebug($"ulongValue: {testJsonClass.ulongValue}");
            Log.LogDebug($"floatValue: {testJsonClass.floatValue}");
            Log.LogDebug($"doubleValue: {testJsonClass.doubleValue}");
            Log.LogDebug($"decimalValue: {testJsonClass.decimalValue}");
            Log.LogDebug($"boolValue: {testJsonClass.boolValue}");
            testJsonClass.LogBool2();
            Log.LogDebug($"charValue: {testJsonClass.charValue}");
            Log.LogDebug($"stringValue: {testJsonClass.stringValue}");
            Log.LogDebug($"testItemClass: {testJsonClass.itemClass}");

            // 打印数组
            Log.LogDebug("intArray:");
            foreach (var item in testJsonClass.intArray)
            {
                Log.LogDebug($"  {item}");
            }

            // 打印数组
            Log.LogDebug("JsonTestItemClassArray:");
            foreach (var item in testJsonClass.JsonTestItemClassArray)
            {
                Log.LogDebug($"  {item}");
            }

            // 打印列表
            Log.LogDebug("intList:");
            foreach (var item in testJsonClass.intList)
            {
                Log.LogDebug($"  {item}");
            }

            // 打印字典
            Log.LogDebug("intStringDict:");
            foreach (var item in testJsonClass.intStringDict)
            {
                Log.LogDebug($"  Key: {item.Key}, Value: {item.Value}");
            }

            // 打印列表
            Log.LogDebug("testItemList:");
            foreach (var item in testJsonClass.testItemList)
            {
                Log.LogDebug($"  {item}");
            }

            // 打印字典
            Log.LogDebug("testItemDict:");
            foreach (var item in testJsonClass.testItemDict)
            {
                Log.LogDebug($"  Key: {item.Key}, Value: {item.Value}");
            }
        }
    }
}