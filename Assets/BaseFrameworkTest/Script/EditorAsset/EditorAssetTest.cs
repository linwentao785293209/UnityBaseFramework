using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using BaseFramework;

namespace BaseFrameworkTest
{
    public class EditorAssetTest : MonoBehaviour
    {
        public RawImage rawImage1;
        public RawImage rawImage2;
        public RawImage rawImage3;
        public Transform prefabParent;

        private Sprite folderTestSprite;
        private Sprite headTestSprite;
        private Texture headTestTexture;
        private GameObject cubeTestGameObject;

        void Start()
        {
            // 测试加载资源
            LoadEditorAsset();
        }

        void Update()
        {
            // 按键切换资源显示和测试
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SetRawImageSprite(rawImage1, folderTestSprite);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                SetRawImageSprite(rawImage2, headTestSprite);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                SetRawImageTexture(rawImage3, headTestTexture);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (cubeTestGameObject != null)
                {
                    Instantiate(cubeTestGameObject, prefabParent);
                }
            }
        }

        private void LoadEditorAsset()
        {
            // 加载图集资源
            EditorAssetManager.Instance.Load<SpriteAtlas>("Assets/Resources/BaseFrameworkTest/AtlasTest",
                (success, atlas) =>
                {
                    if (success && atlas != null)
                    {
                        folderTestSprite = atlas.GetSprite("FolderTest");
                        headTestSprite = atlas.GetSprite("HeadTest");
                        Debug.Log("图集资源加载成功！");
                    }
                    else
                    {
                        Debug.LogError("图集资源加载失败！");
                    }
                });

            // 加载单张图片资源
            headTestTexture = EditorAssetManager.Instance.Load<Texture>("Assets/Resources/BaseFrameworkTest/HeadTest");
            if (headTestTexture != null)
            {
                Debug.Log("图片资源加载成功！");
            }
            else
            {
                Debug.LogError("图片资源加载失败！");
            }


            // 加载 GameObject 资源
            EditorAssetManager.Instance.Load<GameObject>("Assets/Resources/BaseFrameworkTest/CubeTest",
                (success, prefab) =>
                {
                    if (success && prefab != null)
                    {
                        cubeTestGameObject = prefab;
                        Debug.Log("Prefab 资源加载成功！");
                    }
                    else
                    {
                        Debug.LogError("Prefab 资源加载失败！");
                    }
                });
        }

        private void SetRawImageSprite(RawImage rawImage, Sprite sprite)
        {
            if (rawImage == null || sprite == null)
            {
                Debug.LogError("RawImage 或 Sprite 为空，无法设置纹理！");
                return;
            }

            rawImage.texture = sprite.texture;
        }

        private void SetRawImageTexture(RawImage rawImage, Texture texture)
        {
            if (rawImage == null || texture == null)
            {
                Debug.LogError("RawImage 或 Texture2D 为空，无法设置纹理！");
                return;
            }

            rawImage.texture = texture;
        }
    }
}