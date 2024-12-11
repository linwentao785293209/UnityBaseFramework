using BaseFramework;
using UnityEngine;

namespace BaseFrameworkTest
{
    public class MonoBehaviourTest : MonoBehaviour
    {
        private void Start()
        {
            MonoBehaviourManager.Instance.AddUpdateListener(OnCheckInput);
        }

        private void OnDestroy()
        {
            MonoBehaviourManager.Instance?.RemoveUpdateListener(OnCheckInput);
        }

        private void OnCheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                MonoBehaviourManager.Instance.AddUpdateListener(OnUpdate1);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                MonoBehaviourManager.Instance.AddFixedUpdateListener(OnFixedUpdate1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                MonoBehaviourManager.Instance.AddLateUpdateListener(OnLateUpdate1);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                MonoBehaviourManager.Instance.AddUpdateListener(OnUpdate2);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                MonoBehaviourManager.Instance.AddFixedUpdateListener(OnFixedUpdate2);
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                MonoBehaviourManager.Instance.AddLateUpdateListener(OnLateUpdate2);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                MonoBehaviourManager.Instance?.RemoveUpdateListener(OnUpdate1);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                MonoBehaviourManager.Instance?.RemoveFixedUpdateListener(OnFixedUpdate1);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                MonoBehaviourManager.Instance?.RemoveLateUpdateListener(OnLateUpdate1);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                MonoBehaviourManager.Instance?.RemoveUpdateListener(OnUpdate2);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                MonoBehaviourManager.Instance?.RemoveFixedUpdateListener(OnFixedUpdate2);
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                MonoBehaviourManager.Instance?.RemoveLateUpdateListener(OnLateUpdate2);
            }
        }

        private void OnUpdate1()
        {
            Log.LogDebug("每帧更新1");
        }

        private void OnFixedUpdate1()
        {
            Log.LogDebug("固定帧更新1");
        }

        private void OnLateUpdate1()
        {
            Log.LogDebug("晚期更新1");
        }

        private void OnUpdate2()
        {
            Log.LogDebug("每帧更新2");
        }

        private void OnFixedUpdate2()
        {
            Log.LogDebug("固定帧更新2");
        }

        private void OnLateUpdate2()
        {
            Log.LogDebug("晚期更新2");
        }
    }
}