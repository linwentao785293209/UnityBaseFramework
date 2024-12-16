using Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace BaseFramework
{
    public class ConfigManager : CSharpSingleton<ConfigManager>
    {
        /// <summary>
        /// 存储所有加载的 Excel 表数据
        /// </summary>
        private readonly Dictionary<string, object> _excelTableDictionary = new Dictionary<string, object>();

        private ConfigManager()
        {
        }

        /// <summary>
        /// 加载 Excel 表的二进制数据到内存中
        /// </summary>
        /// <typeparam name="T">容器类类型</typeparam>
        /// <typeparam name="K">配置类类型</typeparam>
        public void LoadExcelTable<T, K>(string path)
        {
            string filePath = Path.Combine(path, typeof(K).Name + ".tao");

            // 检查文件是否存在
            if (!File.Exists(filePath))
            {
                Debug.LogError($"文件不存在: {filePath}");
                return;
            }

            using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);

                int index = 0;

                // 读取记录数量
                int count = BitConverter.ToInt32(bytes, index);
                index += 4;

                // 读取主键名称
                int keyNameLength = BitConverter.ToInt32(bytes, index);
                index += 4;
                string keyName = Encoding.UTF8.GetString(bytes, index, keyNameLength);
                index += keyNameLength;

                // 创建容器对象
                Type containerType = typeof(T);
                object containerObj = Activator.CreateInstance(containerType);

                // 获取数据结构类信息
                Type classType = typeof(K);
                FieldInfo[] fieldInfos = classType.GetFields();

                // 读取每一行数据
                for (int i = 0; i < count; i++)
                {
                    object dataObj = Activator.CreateInstance(classType);

                    foreach (FieldInfo fieldInfo in fieldInfos)
                    {
                        if (fieldInfo.FieldType == typeof(int))
                        {
                            fieldInfo.SetValue(dataObj, BitConverter.ToInt32(bytes, index));
                            index += 4;
                        }
                        else if (fieldInfo.FieldType == typeof(float))
                        {
                            fieldInfo.SetValue(dataObj, BitConverter.ToSingle(bytes, index));
                            index += 4;
                        }
                        else if (fieldInfo.FieldType == typeof(bool))
                        {
                            fieldInfo.SetValue(dataObj, BitConverter.ToBoolean(bytes, index));
                            index += 1;
                        }
                        else if (fieldInfo.FieldType == typeof(string))
                        {
                            int length = BitConverter.ToInt32(bytes, index);
                            index += 4;
                            fieldInfo.SetValue(dataObj, Encoding.UTF8.GetString(bytes, index, length));
                            index += length;
                        }
                    }

                    // 将数据添加到容器
                    object dictionaryObj = containerType.GetField("dataClassDictionary")?.GetValue(containerObj);
                    MethodInfo addMethod = dictionaryObj?.GetType().GetMethod("Add");

                    object keyValue = classType.GetField(keyName)?.GetValue(dataObj);
                    addMethod?.Invoke(dictionaryObj, new[] { keyValue, dataObj });
                }

                // 将容器添加到管理器中
                _excelTableDictionary.Add(typeof(T).Name, containerObj);
            }
        }

        /// <summary>
        /// 获取指定表的信息
        /// </summary>
        /// <typeparam name="T">容器类类型</typeparam>
        /// <returns>返回容器对象</returns>
        public T GetExcelTable<T>() where T : class
        {
            string tableName = typeof(T).Name;

            // 检查是否存在指定表
            if (_excelTableDictionary.TryGetValue(tableName, out var table))
            {
                return table as T;
            }

            Debug.LogWarning($"未找到指定表: {tableName}");
            return null;
        }
    }
}