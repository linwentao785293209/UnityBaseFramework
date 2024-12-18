using UnityEngine;

namespace BaseFramework
{
    public abstract class ResourceInfoBase
    {
        public int RefCount;
        public Coroutine Coroutine;

        public void IncrementRefCount()
        {
            ++RefCount;
        }

        public void DecrementRefCount()
        {
            if (RefCount <= 0)
            {
                Log.LogError("引用计数小于等于0了，请检查使用和卸载是否配对执行");
                return;
            }

            --RefCount;
        }

        public void ResetRefCount()
        {
            RefCount = 0;
        }
    }
}