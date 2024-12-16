namespace BaseFramework
{
    // JsonUtility需要加特性
    [System.Serializable]
    public class TestJsonItemClass
    {
        public int id = 1;
        public int num = 10;

        public TestJsonItemClass()
        {
        }

        public TestJsonItemClass(int id, int num)
        {
            this.id = id;
            this.num = num;
        }
    }
}