using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace BaseFramework
{
    public static class ExcelTool
    {
        /// <summary>
        /// 数据内容开始的行号（从第4行开始处理实际数据）
        /// </summary>
        private const int DataBeginIndex = 4;

        /// <summary>
        /// 确保指定的目录存在，若不存在则创建
        /// </summary>
        /// <param name="path">需要检查的目录路径</param>
        private static void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 根据指定路径生成Excel相关的资源文件，包括数据类、容器类和二进制配置文件
        /// </summary>
        /// <param name="excelPath">Excel文件存放路径</param>
        /// <param name="excelDataClassPath">生成数据类文件的路径</param>
        /// <param name="excelDataContainerPath">生成容器类文件的路径</param>
        /// <param name="excelConfigPath">生成二进制配置文件的路径</param>
        public static void GenerateExcelAssets(string excelPath, string excelDataClassPath,
            string excelDataContainerPath, string excelConfigPath)
        {
            // 确保目标目录存在
            EnsureDirectory(excelPath);
            EnsureDirectory(excelDataClassPath);
            EnsureDirectory(excelDataContainerPath);
            EnsureDirectory(excelConfigPath);

            DirectoryInfo directoryInfo = new DirectoryInfo(excelPath);
            FileInfo[] fileInfoArray = directoryInfo.GetFiles();

            // 并行处理每个Excel文件
            Parallel.ForEach(fileInfoArray, fileInfo =>
            {
                if (fileInfo.Extension != ".xlsx" && fileInfo.Extension != ".xls")
                    return;

                try
                {
                    using (FileStream fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read))
                    {
                        IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
                        DataTableCollection tables = excelDataReader.AsDataSet().Tables;

                        // 遍历每个工作表，生成对应资源
                        foreach (DataTable dataTable in tables)
                        {
                            GenerateExcelDataClass(dataTable, excelDataClassPath);
                            GenerateExcelContainer(dataTable, excelDataContainerPath);
                            GenerateExcelBinary(dataTable, excelConfigPath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.LogError($"处理Excel文件失败: {fileInfo.FullName}\n{ex.Message}");
                }
            });

            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 生成Excel表对应的数据类文件
        /// </summary>
        /// <param name="dataTable">Excel工作表数据</param>
        /// <param name="path">生成文件的目标路径</param>
        private static void GenerateExcelDataClass(DataTable dataTable, string path)
        {
            // 获取字段名与字段类型
            DataRow rowName = GetVariableNameRow(dataTable);
            DataRow rowType = GetVariableTypeRow(dataTable);

            Dictionary<string, string> fields = new Dictionary<string, string>();
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                fields[rowName[i].ToString()] = rowType[i].ToString();
            }

            // 生成类模板并写入文件
            string classTemplate = GenerateClassTemplate(dataTable.TableName, fields);
            string filePath = Path.Combine(path, dataTable.TableName + ".cs");
            File.WriteAllText(filePath, classTemplate);

            Log.LogInfo($"生成数据类: {filePath}");
        }

        /// <summary>
        /// 生成Excel表对应的容器类文件
        /// </summary>
        /// <param name="dataTable">Excel工作表数据</param>
        /// <param name="path">生成文件的目标路径</param>
        private static void GenerateExcelContainer(DataTable dataTable, string path)
        {
            int keyIndex = GetKeyIndex(dataTable);
            DataRow rowType = GetVariableTypeRow(dataTable);

            // 定义容器类模板
            string containerTemplate = $"using System.Collections.Generic;\n\n" +
                                       $"public class {dataTable.TableName}Container\n" +
                                       "{\n" +
                                       $"    public Dictionary<{rowType[keyIndex]}, {dataTable.TableName}> Data = new Dictionary<{rowType[keyIndex]}, {dataTable.TableName}>();\n" +
                                       "}";

            string filePath = Path.Combine(path, dataTable.TableName + "Container.cs");
            File.WriteAllText(filePath, containerTemplate);

            Log.LogInfo($"生成容器类: {filePath}");
        }

        /// <summary>
        /// 生成Excel表对应的二进制配置文件
        /// </summary>
        /// <param name="dataTable">Excel工作表数据</param>
        /// <param name="path">生成文件的目标路径</param>
        private static void GenerateExcelBinary(DataTable dataTable, string path)
        {
            string filePath = Path.Combine(path, dataTable.TableName + ".tao");

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                // 写入记录数
                fileStream.Write(BitConverter.GetBytes(dataTable.Rows.Count - DataBeginIndex), 0, 4);

                // 写入主键名
                string keyName = GetVariableNameRow(dataTable)[GetKeyIndex(dataTable)].ToString();
                byte[] keyBytes = Encoding.UTF8.GetBytes(keyName);
                fileStream.Write(BitConverter.GetBytes(keyBytes.Length), 0, 4);
                fileStream.Write(keyBytes, 0, keyBytes.Length);

                // 写入数据
                DataRow rowType = GetVariableTypeRow(dataTable);
                for (int i = DataBeginIndex; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        WriteDataToBinary(fileStream, rowType[j].ToString(), row[j].ToString());
                    }
                }
            }

            Log.LogInfo($"生成二进制文件: {filePath}");
        }

        /// <summary>
        /// 将单个数据项写入二进制文件
        /// </summary>
        /// <param name="fileStream">目标文件流</param>
        /// <param name="type">数据类型</param>
        /// <param name="value">数据值</param>
        private static void WriteDataToBinary(FileStream fileStream, string type, string value)
        {
            switch (type)
            {
                case "int":
                    fileStream.Write(BitConverter.GetBytes(int.Parse(value)), 0, 4);
                    break;
                case "float":
                    fileStream.Write(BitConverter.GetBytes(float.Parse(value)), 0, 4);
                    break;
                case "bool":
                    fileStream.Write(BitConverter.GetBytes(bool.Parse(value)), 0, 1);
                    break;
                case "string":
                    byte[] bytes = Encoding.UTF8.GetBytes(value);
                    fileStream.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
                    fileStream.Write(bytes, 0, bytes.Length);
                    break;
            }
        }

        /// <summary>
        /// 获取字段名行（第一行）
        /// </summary>
        private static DataRow GetVariableNameRow(DataTable table) => table.Rows[0];

        /// <summary>
        /// 获取字段类型行（第二行）
        /// </summary>
        private static DataRow GetVariableTypeRow(DataTable table) => table.Rows[1];

        /// <summary>
        /// 获取主键的列索引
        /// </summary>
        private static int GetKeyIndex(DataTable table)
        {
            DataRow row = table.Rows[2];
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (row[i].ToString() == "key")
                    return i;
            }

            return 0;
        }

        /// <summary>
        /// 生成数据类的模板字符串
        /// </summary>
        private static string GenerateClassTemplate(string className, Dictionary<string, string> fields)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine($"public class {className}");
            sb.AppendLine("{");
            foreach (var field in fields)
            {
                sb.AppendLine($"    public {field.Value} {field.Key};");
            }

            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}