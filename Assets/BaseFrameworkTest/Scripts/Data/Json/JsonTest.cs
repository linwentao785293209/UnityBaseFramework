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

                // ʹ�� JsonUtility �洢
                JsonDataManager.Instance.SetJsonType(EJsonType.JsonUtility);
                JsonDataManager.Instance.Save<TestJsonClass>("myJsonTestClass_JsonUtility", testJsonClass1);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                TestJsonClass testJsonClass1 = new TestJsonClass();
                testJsonClass1.intValue = 999;
                testJsonClass1.intStringDict.Add("99999999", "99999999");

                // ʹ�� LitJson �洢
                JsonDataManager.Instance.SetJsonType(EJsonType.LitJson);
                JsonDataManager.Instance.Save<TestJsonClass>("myJsonTestClass_LitJson", testJsonClass1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                TestJsonClass testJsonClass1 = new TestJsonClass();
                testJsonClass1.intValue = 888;
                testJsonClass1.intStringDict.Add("88888888", "88888888");

                // ʹ�� Newtonsoft.Json �洢
                JsonDataManager.Instance.SetJsonType(EJsonType.NewtonsoftJson);
                JsonDataManager.Instance.Save<TestJsonClass>("myJsonTestClass_NewtonsoftJson", testJsonClass1);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                // ʹ�� JsonUtility ����
                JsonDataManager.Instance.SetJsonType(EJsonType.JsonUtility);
                TestJsonClass testJsonClass2TestJsonUtility =
                    JsonDataManager.Instance.Load<TestJsonClass>("myJsonTestClass_JsonUtility");

                if (testJsonClass2TestJsonUtility != null)
                {
                    // ��ӡ�������Ե�ֵ
                    PrintJsonTestClassValues(testJsonClass2TestJsonUtility, "JsonUtility");
                }
                else
                {
                    Log.LogError("���� JsonTestClass (JsonUtility) ʧ��.");
                }
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                // ʹ�� LitJson ����
                JsonDataManager.Instance.SetJsonType(EJsonType.LitJson);
                TestJsonClass testJsonClass2LitTestJson =
                    JsonDataManager.Instance.Load<TestJsonClass>("myJsonTestClass_LitJson");

                if (testJsonClass2LitTestJson != null)
                {
                    // ��ӡ�������Ե�ֵ
                    PrintJsonTestClassValues(testJsonClass2LitTestJson, "LitJson");
                }
                else
                {
                    Log.LogError("���� JsonTestClass (LitJson) ʧ��.");
                }
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                // ʹ�� Newtonsoft.Json ����
                JsonDataManager.Instance.SetJsonType(EJsonType.NewtonsoftJson);
                TestJsonClass testJsonClass2NewtonsoftTestJson =
                    JsonDataManager.Instance.Load<TestJsonClass>("myJsonTestClass_NewtonsoftJson");

                if (testJsonClass2NewtonsoftTestJson != null)
                {
                    // ��ӡ�������Ե�ֵ
                    PrintJsonTestClassValues(testJsonClass2NewtonsoftTestJson, "NewtonsoftJson");
                }
                else
                {
                    Log.LogError("���� JsonTestClass (NewtonsoftJson) ʧ��.");
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

        // ��ӡ JsonTestClass ���������Ե�ֵ
        private void PrintJsonTestClassValues(TestJsonClass testJsonClass, string jsonType)
        {
            Log.LogDebug($"Printing values for JsonTestClass using {jsonType}:");

            // ��ӡ�������͵�ֵ
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

            // ��ӡ����
            Log.LogDebug("intArray:");
            foreach (var item in testJsonClass.intArray)
            {
                Log.LogDebug($"  {item}");
            }

            // ��ӡ����
            Log.LogDebug("JsonTestItemClassArray:");
            foreach (var item in testJsonClass.JsonTestItemClassArray)
            {
                Log.LogDebug($"  {item}");
            }

            // ��ӡ�б�
            Log.LogDebug("intList:");
            foreach (var item in testJsonClass.intList)
            {
                Log.LogDebug($"  {item}");
            }

            // ��ӡ�ֵ�
            Log.LogDebug("intStringDict:");
            foreach (var item in testJsonClass.intStringDict)
            {
                Log.LogDebug($"  Key: {item.Key}, Value: {item.Value}");
            }

            // ��ӡ�б�
            Log.LogDebug("testItemList:");
            foreach (var item in testJsonClass.testItemList)
            {
                Log.LogDebug($"  {item}");
            }

            // ��ӡ�ֵ�
            Log.LogDebug("testItemDict:");
            foreach (var item in testJsonClass.testItemDict)
            {
                Log.LogDebug($"  Key: {item.Key}, Value: {item.Value}");
            }
        }
    }
}