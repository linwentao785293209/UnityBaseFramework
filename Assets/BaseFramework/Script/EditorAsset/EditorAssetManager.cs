using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using UnityEngine.U2D;

namespace BaseFramework
{
    public class EditorAssetManager : CSharpSingleton<EditorAssetManager>
    {
        private static readonly Dictionary<Type, List<string>> TypeSuffixMap = new Dictionary<Type, List<string>>
        {
            { typeof(GameObject), new List<string> { ".prefab" } },
            { typeof(Material), new List<string> { ".mat" } },
            { typeof(Texture), new List<string> { ".png", ".jpg" } },
            { typeof(Sprite), new List<string> { ".png", ".jpg" } },
            { typeof(AudioClip), new List<string> { ".mp3", ".wav" } },
            { typeof(SpriteAtlas), new List<string> { ".spriteatlas", ".spriteatlasv2" } }
        };

        private EditorAssetManager()
        {
        }

        /// <summary>
        /// 加载资源（仅编辑器模式下可用）
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="path">资源路径（需以 "Assets/" 开头，不包含后缀）</param>
        /// <param name="callBack">回调，返回加载结果</param>
        /// <returns>加载的资源对象</returns>
        public T Load<T>(string path, UnityAction<bool, T> callBack = null) where T : UnityEngine.Object
        {
        #if UNITY_EDITOR
            // 修正路径（移除首尾空格）
            path = path.Trim();

            // 验证路径格式
            if (string.IsNullOrEmpty(path) || !path.StartsWith("Assets/"))
            {
                Log.LogError($"资源路径必须以 'Assets/' 开头！当前路径：'{path}'");
                callBack?.Invoke(false, null);
                return null;
            }

            // 获取类型对应的后缀列表
            if (!TypeSuffixMap.TryGetValue(typeof(T), out List<string> suffixList))
            {
                Log.LogWarning($"无法推断类型 {typeof(T).Name} 的资源后缀，请检查类型和路径！");
                callBack?.Invoke(false, null);
                return null;
            }

            // 遍历后缀列表，尝试加载资源
            foreach (string suffix in suffixList)
            {
                T asset = AssetDatabase.LoadAssetAtPath<T>(path + suffix.ToLower());
                if (asset != null)
                {
                    callBack?.Invoke(true, asset);
                    return asset;
                }
            }

            // 如果所有后缀都未找到资源
            Log.LogWarning($"编辑器资源中未找到 {path} 路径下的 {typeof(T).Name} 类型资源（尝试的后缀：{string.Join(", ", suffixList)}）。返回空！");
            callBack?.Invoke(false, null);
            return null;
        #else
            Log.LogError("非编辑器模式下不能用编辑器资源管理器加载资源！返回空！");
            return null;
        #endif
        }
    }
}