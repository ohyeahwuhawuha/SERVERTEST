public class Singleton<T> where T : new ()
{
    private static T _instance;
    private static readonly object lockHelper = new object();

    public static T I
    {
        get
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new T();
                }
            }
            return _instance;
        }
    }
}