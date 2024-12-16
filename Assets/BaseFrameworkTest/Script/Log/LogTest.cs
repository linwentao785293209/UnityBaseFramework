using BaseFramework;
using UnityEngine;

namespace BaseFrameworkTest
{
    public class LogTest : MonoBehaviour
    {
        void Update()
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Log.LogDebug($"{CreateRandomStr()}", $"{CreateRandomStr()}", $"{CreateRandomStr()}");
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    Log.LogInfo($"{CreateRandomStr()}", $"{CreateRandomStr()}", $"{CreateRandomStr()}");
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    Log.LogWarning($"{CreateRandomStr()}", $"{CreateRandomStr()}", $"{CreateRandomStr()}");
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    Log.LogError($"{CreateRandomStr()}", $"{CreateRandomStr()}", $"{CreateRandomStr()}");
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    Log.SetLogConfig<LogDebugConfig>();
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    Log.SetLogConfig<LogInfoConfig>();
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    Log.SetLogConfig<LogWarningConfig>();
                }
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    Log.SetLogConfig<LogErrorConfig>();
                }
                else if (Input.GetKeyDown(KeyCode.G))
                {
                    Log.SetLogConfig<LogCloseConfig>();
                }
            }
        }

        string CreateRandomStr()
        {
            int randomNum = Random.Range(1, 20);
            string resultStr = "";
            for (int j = 0; j < randomNum; j++)
            {
                char randomChar = (char)Random.Range('A', 'Z');
                resultStr += randomChar;
            }

            return resultStr;
        }
    }
}