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
            // 同步加载预制体HeadTest
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

            // 同步加载图片资源HeadTest
            if (Input.GetKeyDown(KeyCode.W))
            {
                Log.LogDebug("同步加载 HeadTest 图片资源");
                var headTestTexture = ResourceManager.Instance.LoadSync<Texture>("BaseFrameworkTest/HeadTest");
                rawImage1.texture = headTestTexture;
                Log.LogDebug("同步加载 HeadTest 图片资源成功");
            }

            // 异步加载图片资源HeadTest
            if (Input.GetKeyDown(KeyCode.E))
            {
                Log.LogDebug("异步加载 HeadTest 图片资源");
                ResourceManager.Instance.LoadAsync<Texture>("BaseFrameworkTest/HeadTest",
                    ((isSuccess, folderTestTexture) =>
                    {
                        if (isSuccess)
                        {
                            rawImage2.texture = folderTestTexture;
                            Log.LogDebug("异步加载 HeadTest 图片资源成功");
                        }
                        else
                        {
                            Log.LogWarning("异步加载 HeadTest 资源失败");
                        }
                    }));
            }

            // 卸载图片资源HeadTest
            if (Input.GetKeyDown(KeyCode.R))
            {
                Log.LogDebug("卸载 HeadTest 图片资源");
                ResourceManager.Instance.Unload<Texture>("BaseFrameworkTest/HeadTest");
            }

            // 异步加载预制体FolderTest时同步加载
            if (Input.GetKeyDown(KeyCode.A))
            {
                Log.LogDebug("异步加载预制体 FolderTest 预制体资源时同步加载");
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
                var folderTestGameObject =
                    ResourceManager.Instance.LoadSync<GameObject>("BaseFrameworkTest/FolderTest");
                if (folderTestGameObject != null)
                {
                    var obj = Instantiate(folderTestGameObject, GetRandomPosition(), Quaternion.identity);
                    obj.name = $"FolderTest_同步_{Time.time}";
                    loadedPrefabs.Add(obj);
                    Log.LogDebug($"同步加载成功，实例化 {obj.name}");
                }
            }

            // 异步加载图片资源FolderTest
            if (Input.GetKeyDown(KeyCode.S))
            {
                Log.LogDebug("异步加载 FolderTest 图片资源");
                ResourceManager.Instance.LoadAsync<Texture>("BaseFrameworkTest/FolderTest",
                    LoadFolderTestTextureCallback1);
                ResourceManager.Instance.LoadAsync<Texture>("BaseFrameworkTest/FolderTest",
                    LoadFolderTestTextureCallback2);
                // ResourceManager.Instance.Unload<Texture>("BaseFrameworkTest/FolderTest");
                ResourceManager.Instance.Unload<Texture>("BaseFrameworkTest/FolderTest",
                    LoadFolderTestTextureCallback1);
            }
        }

        private void LoadFolderTestTextureCallback1(bool isSuccess, Texture folderTestTexture)
        {
            if (isSuccess)
            {
                rawImage3.texture = folderTestTexture;
                Log.LogDebug("异步加载 FolderTest1 图片资源成功");
            }
            else
            {
                Log.LogWarning("异步加载 FolderTest1 资源失败");
            }
        }

        private void LoadFolderTestTextureCallback2(bool isSuccess, Texture folderTestTexture)
        {
            if (isSuccess)
            {
                rawImage3.texture = folderTestTexture;
                Log.LogDebug("异步加载 FolderTest2 图片资源成功");
            }
            else
            {
                Log.LogWarning("异步加载 FolderTest2 资源失败");
            }
        }

        private Vector3 GetRandomPosition()
        {
            return new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f)
            );
        }

        private void DestroyLoadedPrefabs()
        {
            foreach (var obj in loadedPrefabs)
            {
                Destroy(obj);
            }

            loadedPrefabs.Clear();
            Log.LogDebug("所有实例化的预制体已销毁");
        }

        void OnResourceCleared()
        {
            Log.LogDebug("资源管理器缓存已清理完成");
        }
    }
}