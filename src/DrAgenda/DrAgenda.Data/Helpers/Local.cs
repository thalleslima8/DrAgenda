namespace DrAgenda.Data.Helpers
{
    public class Local
    {
        public static void Initialize(ILocalData localData)
        {
            Data = localData;
        }

        public static object Obj = new object();

        public static ILocalData Data { get; private set; }
    }
}
