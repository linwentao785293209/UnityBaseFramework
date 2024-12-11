using System;
using System.Linq;
using BaseFramework;
using UnityEngine;

namespace BaseFrameworkTest
{
    public class PlayerPrefsTest : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TestPlayerPrefsClass testPlayerPrefsClass1 = new TestPlayerPrefsClass();
                PlayerPrefsDataManager.Instance.Save<TestPlayerPrefsClass>("myPlayerPrefsTestClass", testPlayerPrefsClass1);
                Log.LogDebug("Saved PlayerPrefsTestClass: " + testPlayerPrefsClass1);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                TestPlayerPrefsClass testPlayerPrefsClass2 = PlayerPrefsDataManager.Instance.Load<TestPlayerPrefsClass>("myPlayerPrefsTestClass");
                if (testPlayerPrefsClass2 != null)
                {
                    Log.LogDebug("Loaded PlayerPrefsTestClass:");
                    Log.LogDebug("sbyteValue: " + testPlayerPrefsClass2.sbyteValue);
                    Log.LogDebug("intValue: " + testPlayerPrefsClass2.intValue);
                    Log.LogDebug("shortValue: " + testPlayerPrefsClass2.shortValue);
                    Log.LogDebug("longValue: " + testPlayerPrefsClass2.longValue);
                    Log.LogDebug("byteValue: " + testPlayerPrefsClass2.byteValue);
                    Log.LogDebug("uintValue: " + testPlayerPrefsClass2.uintValue);
                    Log.LogDebug("ushortValue: " + testPlayerPrefsClass2.ushortValue);
                    Log.LogDebug("ulongValue: " + testPlayerPrefsClass2.ulongValue);
                    Log.LogDebug("floatValue: " + testPlayerPrefsClass2.floatValue);
                    Log.LogDebug("doubleValue: " + testPlayerPrefsClass2.doubleValue);
                    Log.LogDebug("decimalValue: " + testPlayerPrefsClass2.decimalValue);
                    Log.LogDebug("boolValue: " + testPlayerPrefsClass2.boolValue);
                    Log.LogDebug("charValue: " + testPlayerPrefsClass2.charValue);
                    Log.LogDebug("stringValue: " + testPlayerPrefsClass2.stringValue);
                    Log.LogDebug("testItemClass: " + testPlayerPrefsClass2.ItemClass);
                    Log.LogDebug("intArray: " + string.Join(", ", testPlayerPrefsClass2.intArray));
                    // 遍历数组并打印
                    foreach (var item in testPlayerPrefsClass2.playerPrefsTestItemClassArray)
                    {
                        Log.LogDebug("playerPrefsTestItemClassArray item: id = " + item.id + ", num = " + item.num);
                    }
                    Log.LogDebug("intList: " + string.Join(", ", testPlayerPrefsClass2.intList));
                    Log.LogDebug("intStringDict: " + string.Join(", ", testPlayerPrefsClass2.intStringDict.Select(kv => kv.Key + ": " + kv.Value)));
                    
                    
                    // 遍历并打印列表
                    foreach (var item in testPlayerPrefsClass2.testItemList)
                    {
                        Log.LogDebug("testItemList item: id = " + item.id + ", num = " + item.num);
                    }
                    
                    // 遍历并打印字典
                    foreach (var kv in testPlayerPrefsClass2.testItemDict)
                    {
                        Log.LogDebug($"testItemDict key = {kv.Key}, value: id = {kv.Value.id}, num = {kv.Value.num}");
                    }
                }
                else
                {
                    Log.LogDebug("Failed to load PlayerPrefsTestClass.");
                }
            }
        }
    }
}
