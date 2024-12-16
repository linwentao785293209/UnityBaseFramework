namespace BaseFrameworkTest
{
    // 用于二进制序列化，需要加特性
    [System.Serializable]
    public class TestBinaryItemClass
    {
        public int id = 1;
        public int num = 10;

        public TestBinaryItemClass()
        {
        }

        public TestBinaryItemClass(int id, int num)
        {
            this.id = id;
            this.num = num;
        }
    }
}