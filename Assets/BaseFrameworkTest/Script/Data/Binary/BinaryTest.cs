using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using BaseFramework;
using UnityEngine;

namespace BaseFrameworkTest
{
    public class BinaryTest : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TestBinaryClass testBinaryClass1 = new TestBinaryClass();
                testBinaryClass1.intValue = 666;
                testBinaryClass1.intStringDict.Add("6666666", "6666666");

                testBinaryClass1.stringValue = "i am shuaige!";

                // 使用 BinaryDataManager 存储
                BinaryDataManager.Instance.Save("myBinaryTestClass", testBinaryClass1);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                // 使用 BinaryDataManager 加载
                TestBinaryClass testBinaryClass2 =
                    BinaryDataManager.Instance.Load<TestBinaryClass>("myBinaryTestClass");

                if (testBinaryClass2 != null)
                {
                    // 打印所有属性的值
                    PrintBinaryTestClassValues(testBinaryClass2);
                }
                else
                {
                    Log.LogError("加载 BinaryTestClass 失败.");
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Log.LogDebug($"{BinaryDataManager.Instance.PersistentDataPath}");
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                BinaryDataManager.Instance.Delete("myBinaryTestClass");
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                BinaryDataManager.Instance.Clear();
            }
        }

        // 打印 BinaryTestClass 中所有属性的值
        private void PrintBinaryTestClassValues(TestBinaryClass testBinaryClass)
        {
            Log.LogDebug("Printing values for BinaryTestClass:");

            // 打印各个类型的值
            Log.LogDebug($"sbyteValue: {testBinaryClass.sbyteValue}");
            Log.LogDebug($"intValue: {testBinaryClass.intValue}");
            Log.LogDebug($"shortValue: {testBinaryClass.shortValue}");
            Log.LogDebug($"longValue: {testBinaryClass.longValue}");
            Log.LogDebug($"byteValue: {testBinaryClass.byteValue}");
            Log.LogDebug($"uintValue: {testBinaryClass.uintValue}");
            Log.LogDebug($"ushortValue: {testBinaryClass.ushortValue}");
            Log.LogDebug($"ulongValue: {testBinaryClass.ulongValue}");
            Log.LogDebug($"floatValue: {testBinaryClass.floatValue}");
            Log.LogDebug($"doubleValue: {testBinaryClass.doubleValue}");
            Log.LogDebug($"decimalValue: {testBinaryClass.decimalValue}");
            Log.LogDebug($"boolValue: {testBinaryClass.boolValue}");
            testBinaryClass.LogBool2();
            Log.LogDebug($"charValue: {testBinaryClass.charValue}");
            Log.LogDebug($"stringValue: {testBinaryClass.stringValue}");
            Log.LogDebug($"testItemClass: {testBinaryClass.itemClass}");

            // 打印数组
            Log.LogDebug("intArray:");
            foreach (var item in testBinaryClass.intArray)
            {
                Log.LogDebug($"  {item}");
            }

            // 打印数组
            Log.LogDebug("BinaryTestItemClassArray:");
            foreach (var item in testBinaryClass.BinaryTestItemClassArray)
            {
                Log.LogDebug($"  {item}");
            }

            // 打印列表
            Log.LogDebug("intList:");
            foreach (var item in testBinaryClass.intList)
            {
                Log.LogDebug($"  {item}");
            }

            // 打印字典
            Log.LogDebug("intStringDict:");
            foreach (var item in testBinaryClass.intStringDict)
            {
                Log.LogDebug($"  Key: {item.Key}, Value: {item.Value}");
            }

            // 打印列表
            Log.LogDebug("testItemList:");
            foreach (var item in testBinaryClass.testItemList)
            {
                Log.LogDebug($"  {item}");
            }

            // 打印字典
            Log.LogDebug("testItemDict:");
            foreach (var item in testBinaryClass.testItemDict)
            {
                Log.LogDebug($"  Key: {item.Key}, Value: {item.Value}");
            }
        }
    }
}