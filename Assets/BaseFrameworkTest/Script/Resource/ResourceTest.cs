using System.Collections.Generic;
using BaseFramework;
using UnityEngine;
using UnityEngine.UI;

namespace BaseFrameworkTest
{
    public class ResourceTest : MonoBehaviour
    {
        public RawImage rawImage1;
        public RawImage rawImage2;
        public RawImage rawImage3;
        private List<GameObject> loadedPrefabs = new List<GameObject>();

        void Update()
        {
            // 同步加载预制体：HeadTest
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Log.LogDebug("同步加载 HeadTest 预制体资源");
                var headTestGameObject = ResourceManager.Instance.LoadSync<GameObject>("BaseFrameworkTest/HeadTest");
                if (headTestGameObject != null)
                {
                    var obj = Instantiate(headTestGameObject, GetRandomPosition(), Quaternion.identity);
                    obj.name = $"HeadTest_同步_{Time.time}";
                    loadedPrefabs.Add(obj);
                    Log.LogDebug($"同步加载成功，实例化 {obj.name}");
                }
            }

            // 同步加载图片资源：HeadTest
            if (Input.GetKeyDown(KeyCode.W))
            {
                Log.LogDebug("同步加载 HeadTest 图片资源");
                var headTestTexture = ResourceManager.Instance.LoadSync<Texture>("BaseFrameworkTest/HeadTest");
                rawImage1.texture = headTestTexture;
                Log.LogDebug("同步加载 HeadTest 图片资源成功");
            }

            // 异步加载预制体：FolderTest
            if (Input.GetKeyDown(KeyCode.E))
            {
                Log.LogDebug("异步加载 FolderTest 预制体资源");
                ResourceManager.Instance.LoadAsync<GameObject>("BaseFrameworkTest/FolderTest",
                    ((isSuccess, folderTestGameObject) =>
                    {
                        if (isSuccess)
                        {
                            var obj = Instantiate(folderTestGameObject, GetRandomPosition(), Quaternion.identity);
                            obj.name = $"FolderTest_异步_{Time.time}";
                            loadedPrefabs.Add(obj);
                            Log.LogDebug($"异步加载成功，实例化 {obj.name}");
                        }
                        else
                        {
                            Log.LogWarning("异步加载 FolderTest 资源失败");
                        }
                    }));
            }

            // 异步加载图片资源：FolderTest
            if (Input.GetKeyDown(KeyCode.R))
            {
                Log.LogDebug("异步加载 FolderTest 图片资源");
                var headTestTexture = ResourceManager.Instance.LoadSync<Texture>("BaseFrameworkTest/FolderTest");
                ResourceManager.Instance.LoadAsync<Texture>("BaseFrameworkTest/FolderTest",
                    ((isSuccess, folderTestTexture) =>
                    {
                        if (isSuccess)
                        {
                            rawImage2.texture = headTestTexture;
                            Log.LogDebug("异步加载 FolderTest 图片资源成功");
                        }
                        else
                        {
                            Log.LogWarning("异步加载 FolderTest 资源失败");
                        }
                    }));
            }
        }

        /// <summary>
        /// 获取随机位置：(-10, 10) 范围
        /// </summary>
        private Vector3 GetRandomPosition()
        {
            return new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f)
            );
        }

        /// <summary>
        /// 销毁所有实例化的预制体
        /// </summary>
        private void DestroyLoadedPrefabs()
        {
            foreach (var obj in loadedPrefabs)
            {
                Destroy(obj);
            }

            loadedPrefabs.Clear();
            Log.LogDebug("所有实例化的预制体已销毁");
        }

        /// <summary>
        /// 资源清理回调
        /// </summary>
        void OnResourceCleared()
        {
            Log.LogDebug("资源管理器缓存已清理完成");
        }
    }
}