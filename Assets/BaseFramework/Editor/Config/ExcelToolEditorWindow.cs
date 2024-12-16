using UnityEditor;
using UnityEngine;
using System.IO;

namespace BaseFramework
{
    public class ExcelToolEditorWindow : EditorWindow
    {
        private string _excelFilesPath = "";
        private string _excelConfigClassPath = "";
        private string _excelConfigContainerPath = "";
        private string _excelConfigFilesPath = "";

        private const string ExcelFilesPathKey = "ExcelTool_ExcelFilesPath";
        private const string ExcelConfigClassPathKey = "ExcelTool_ExcelConfigClassPath";
        private const string ExcelConfigContainerPathKey = "ExcelTool_ExcelConfigContainerPath";
        private const string ExcelConfigFilesPathKey = "ExcelTool_ExcelConfigFilesPathKey";

        private const string ExcelToolEditorPath =
            Const.BaseFramework + "/" + Const.Config + "/" + ExcelToolEditorWindowTitle;

        private const string ExcelToolEditorWindowTitle = "Excel文件生成 配置类 配置容器类 二进制配置文件 工具";
        private const string Select = "选择";
        private const string ExcelFilesPath = "Excel文件路径";
        private const string ExcelConfigClassPath = "Excel配置类路径";
        private const string ExcelConfigContainerPath = "Excel配置容器类路径";
        private const string ExcelConfigFilesPath = "Excel二进制配置文件路径";


        [MenuItem(ExcelToolEditorPath)]
        public static void ShowWindow()
        {
            ExcelToolEditorWindow excelToolEditorWindow = EditorWindow.GetWindow<ExcelToolEditorWindow>();
            excelToolEditorWindow.titleContent = new GUIContent(ExcelToolEditorWindowTitle);
            excelToolEditorWindow.Show();
        }

        private void OnEnable()
        {
            _excelFilesPath = EditorPrefs.GetString(ExcelFilesPathKey, "");
            _excelConfigClassPath = EditorPrefs.GetString(ExcelConfigClassPathKey, "");
            _excelConfigContainerPath = EditorPrefs.GetString(ExcelConfigContainerPathKey, "");
            _excelConfigFilesPath = EditorPrefs.GetString(ExcelConfigFilesPathKey, "");
        }

        private void OnDisable()
        {
            EditorPrefs.SetString(ExcelFilesPathKey, _excelFilesPath);
            EditorPrefs.SetString(ExcelConfigClassPathKey, _excelConfigClassPath);
            EditorPrefs.SetString(ExcelConfigContainerPathKey, _excelConfigContainerPath);
            EditorPrefs.SetString(ExcelConfigFilesPathKey, _excelConfigFilesPath);
        }

        private void OnGUI()
        {
            // 标题
            GUIStyle centeredStyle = new GUIStyle(GUI.skin.label);
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            centeredStyle.fontSize = 16;
            GUILayout.Label(ExcelToolEditorWindowTitle, centeredStyle);

            EditorGUILayout.Space(30);

            // Excel文件路径
            _excelFilesPath = EditorGUILayout.TextField(ExcelFilesPath, _excelFilesPath);
            if (GUILayout.Button(Select + ExcelFilesPath))
            {
                string selectedPath = EditorUtility.OpenFolderPanel(Select + ExcelFilesPath, _excelFilesPath, "");
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    _excelFilesPath = selectedPath;
                }
            }

            EditorGUILayout.Space(15);

            // 数据类路径
            _excelConfigClassPath = EditorGUILayout.TextField(ExcelConfigClassPath, _excelConfigClassPath);
            if (GUILayout.Button(Select + ExcelConfigClassPath))
            {
                string selectedPath =
                    EditorUtility.OpenFolderPanel(Select + ExcelConfigClassPath, _excelConfigClassPath, "");
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    _excelConfigClassPath = selectedPath;
                }
            }

            EditorGUILayout.Space(15);

            // 容器类路径
            _excelConfigContainerPath = EditorGUILayout.TextField(ExcelConfigContainerPath, _excelConfigContainerPath);
            if (GUILayout.Button(Select + ExcelConfigContainerPath))
            {
                string selectedPath =
                    EditorUtility.OpenFolderPanel(Select + ExcelConfigContainerPath, _excelConfigContainerPath, "");
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    _excelConfigContainerPath = selectedPath;
                }
            }

            EditorGUILayout.Space(15);

            // 配置文件路径
            _excelConfigFilesPath = EditorGUILayout.TextField(ExcelConfigFilesPath, _excelConfigFilesPath);
            if (GUILayout.Button(Select + ExcelConfigFilesPath))
            {
                string selectedPath =
                    EditorUtility.OpenFolderPanel(Select + ExcelConfigFilesPath, _excelConfigFilesPath, "");
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    _excelConfigFilesPath = selectedPath;
                }
            }

            EditorGUILayout.Space(15);

            // 生成按钮
            if (GUILayout.Button("生成 配置类 配置容器类 二进制配置文件"))
            {
                if (string.IsNullOrEmpty(_excelFilesPath)
                    || string.IsNullOrEmpty(_excelConfigClassPath)
                    || string.IsNullOrEmpty(_excelConfigContainerPath)
                    || string.IsNullOrEmpty(_excelConfigFilesPath))
                {
                    EditorUtility.DisplayDialog("错误", "请先选择所有路径后再生成。", "确定");
                    return;
                }

                ExcelTool.GenerateExcelAssets(_excelFilesPath, _excelConfigClassPath, _excelConfigContainerPath,
                    _excelConfigFilesPath);
            }
        }
    }
}