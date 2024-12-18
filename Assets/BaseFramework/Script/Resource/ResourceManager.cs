using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BaseFramework
{
    /// <summary>
    /// 管理游戏资源的加载、卸载以及引用计数的核心类。
    /// </summary>
    public class ResourceManager : CSharpSingleton<ResourceManager>
    {
        /// <summary>
        /// 嵌套字典，外层以资源路径为Key，内层以资源类型为Key
        /// </summary>
        private readonly Dictionary<string, Dictionary<Type, ResourceInfoBase>> _resourceInfoDictionary =
            new Dictionary<string, Dictionary<Type, ResourceInfoBase>>();

        private ResourceManager()
        {
        }

        /// <summary>
        /// 尝试获取资源信息。
        /// </summary>
        private bool TryGetResourceInfo<T>(string path, out ResourceInfo<T> resourceInfo) where T : UnityEngine.Object
        {
            resourceInfo = null;
            if (_resourceInfoDictionary.TryGetValue(path, out var typeDictionary) &&
                typeDictionary.TryGetValue(typeof(T), out var baseInfo) &&
                baseInfo is ResourceInfo<T> typedInfo)
            {
                resourceInfo = typedInfo;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 同步加载资源。
        /// </summary>
        public T LoadSync<T>(string path) where T : UnityEngine.Object
        {
            // 没有加载过直接同步加载
            if (!TryGetResourceInfo(path, out ResourceInfo<T> resourceInfo))
            {
                T asset = Resources.Load<T>(path);
                if (asset == null)
                {
                    Log.LogInfo($"同步加载 路径 {path} 资源类型为 {typeof(T)} 的资源失败。");
                    return null;
                }

                resourceInfo = new ResourceInfo<T> { Asset = asset };
                resourceInfo.IncrementRefCount();

                if (!_resourceInfoDictionary.ContainsKey(path))
                {
                    _resourceInfoDictionary[path] = new Dictionary<Type, ResourceInfoBase>();
                }

                _resourceInfoDictionary[path][typeof(T)] = resourceInfo;

                Log.LogInfo($"同步加载 路径 {path} 资源类型为 {typeof(T)} 的资源成功。");

                return asset;
            }

            resourceInfo.IncrementRefCount();

            // 如果资源正在异步加载，立即加载同步资源并完成回调。
            if (resourceInfo.IsLoading)
            {
                Log.LogInfo($"路径 {path} 资源类型为 {typeof(T)} 的资源尝试过异步加载 取消异步加载改成同步加载。");
                MonoBehaviourManager.Instance.StopCoroutine(resourceInfo.Coroutine);
                resourceInfo.Coroutine = null;

                T asset = Resources.Load<T>(path);
                if (asset == null)
                {
                    Log.LogInfo($"同步加载 路径 {path} 资源类型为 {typeof(T)} 的资源失败。");
                    resourceInfo.CallBack?.Invoke(false, null);
                    return null;
                }

                Log.LogInfo($"同步加载 路径 {path} 资源类型为 {typeof(T)} 的资源成功。");
                resourceInfo.Asset = asset;

                Log.LogInfo($"同步加载成功后 尝试执行异步加载 路径 {path} 资源类型为 {typeof(T)} 的资源时添加的回调。");
                resourceInfo.CallBack?.Invoke(true, resourceInfo.Asset);
                resourceInfo.CallBack = null;

                return asset;
            }

            // 加载过直接返回
            Log.LogInfo($"加载过 路径 {path} 资源类型为 {typeof(T)} 的资源 直接返回。");
            return resourceInfo.Asset;
        }

        /// <summary>
        /// 异步加载资源。
        /// </summary>
        public void LoadAsync<T>(string path, UnityAction<bool, T> callBack) where T : UnityEngine.Object
        {
            // 没有加载过直接异步加载
            if (!TryGetResourceInfo(path, out ResourceInfo<T> resourceInfo))
            {
                resourceInfo = new ResourceInfo<T>();
                resourceInfo.IncrementRefCount();
                resourceInfo.CallBack += callBack;

                if (!_resourceInfoDictionary.ContainsKey(path))
                {
                    _resourceInfoDictionary[path] = new Dictionary<Type, ResourceInfoBase>();
                }

                _resourceInfoDictionary[path][typeof(T)] = resourceInfo;

                Log.LogInfo($"开始异步加载 路径 {path} 资源类型为 {typeof(T)} 的资源。");
                resourceInfo.Coroutine = MonoBehaviourManager.Instance.StartCoroutine(LoadAsyncCoroutine<T>(path));
            }
            // 加载过的话分情况处理
            else
            {
                resourceInfo.IncrementRefCount();

                // 资源正在加载中，挂接回调
                if (resourceInfo.IsLoading)
                {
                    Log.LogInfo($"正在异步加载 路径 {path} 资源类型为 {typeof(T)} 的资源，添加异步加载回调。");
                    resourceInfo.CallBack += callBack;
                }
                // 资源已加载完成，直接回调
                else
                {
                    Log.LogInfo($"加载过 路径 {path} 资源类型为 {typeof(T)} 的资源 直接返回。");
                    callBack?.Invoke(true, resourceInfo.Asset);
                }
            }
        }

        /// <summary>
        /// 异步加载协程。
        /// </summary>
        private IEnumerator LoadAsyncCoroutine<T>(string path) where T : UnityEngine.Object
        {
            var resourceRequest = Resources.LoadAsync<T>(path);
            yield return resourceRequest;


            if (!TryGetResourceInfo(path, out ResourceInfo<T> resourceInfo))
            {
                Log.LogError($"路径 {path} 获取资源信息失败。可能在异步加载过程中被卸载。");
                yield break;
            }

            if (resourceRequest.asset == null)
            {
                Log.LogError($"异步加载 路径 {path} 资源类型为 {typeof(T)} 的资源失败。");
                resourceInfo.DecrementRefCount();
                _resourceInfoDictionary[path].Remove(typeof(T));
                if (_resourceInfoDictionary[path].Count == 0)
                {
                    _resourceInfoDictionary.Remove(path);
                }

                resourceInfo.Coroutine = null;
                resourceInfo.CallBack?.Invoke(false, null);
                resourceInfo.CallBack = null;
                yield break;
            }

            Log.LogInfo($"异步加载 路径 {path} 资源类型为 {typeof(T)} 的资源成功，执行异步加载回调。");
            resourceInfo.Coroutine = null;
            resourceInfo.Asset = resourceRequest.asset as T;
            resourceInfo.CallBack?.Invoke(true, resourceInfo.Asset);
            resourceInfo.CallBack = null;
        }

        /// <summary>
        /// 卸载资源。
        /// </summary>
        public void Unload<T>(string path, UnityAction<bool, T> removeCallback = null) where T : UnityEngine.Object
        {
            if (!TryGetResourceInfo(path, out ResourceInfo<T> resourceInfo))
            {
                Log.LogError($"路径 {path} 获取资源信息失败。");
                return;
            }

            resourceInfo.DecrementRefCount();

            // 引用计数为0
            if (resourceInfo.RefCount == 0)
            {
                Log.LogInfo($"路径 {path} 资源类型为 {typeof(T)} 的资源引用计数为0，移除资源信息。");
                _resourceInfoDictionary[path].Remove(typeof(T));
                if (_resourceInfoDictionary[path].Count == 0)
                {
                    _resourceInfoDictionary.Remove(path);
                }

                // 异步加载中，停止协程，执行失败回调
                if (resourceInfo.IsLoading)
                {
                    Log.LogInfo($"路径 {path} 资源类型为 {typeof(T)} 的资源异步加载中，停止协程，执行失败回调。");
                    MonoBehaviourManager.Instance.StopCoroutine(resourceInfo.Coroutine);
                    resourceInfo.Coroutine = null;
                    resourceInfo.CallBack?.Invoke(false, null);
                }
                // 已经加载完，直接卸载
                else
                {
                    Log.LogInfo($"路径 {path} 资源类型为 {typeof(T)} 的资源已经加载完，直接卸载。");
                    Resources.UnloadAsset(resourceInfo.Asset);
                }
            }
            // 引用计数不为0
            else
            {
                Log.LogInfo($"路径 {path} 资源类型为 {typeof(T)} 的资源引用计数为{resourceInfo.RefCount}，仍然有资源依赖。");
                // 异步加载中，移除指定回调
                if (resourceInfo.IsLoading && removeCallback != null)
                {
                    Log.LogInfo($"路径 {path} 资源类型为 {typeof(T)} 的资源异步加载中，移除指定回调。");
                    resourceInfo.CallBack -= removeCallback;
                }
            }
        }


        /// <summary>
        /// 获取指定资源的引用计数。
        /// </summary>
        /// <typeparam name="T">资源类型。</typeparam>
        /// <param name="path">资源路径。</param>
        /// <returns>资源的引用计数。</returns>
        public int GetRefCount<T>(string path) where T : UnityEngine.Object
        {
            if (!TryGetResourceInfo(path, out ResourceInfo<T> resourceInfo))
            {
                Log.LogError($"路径 {path} 获取资源信息失败。");
                return -1;
            }

            return resourceInfo.RefCount;
        }
    }
}