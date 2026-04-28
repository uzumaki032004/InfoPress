namespace InfoPress.Singleton
{
    public sealed class ManagerConfigurare
    {
        private static ManagerConfigurare _instance = null;
        private static readonly object _lock = new object();

        private ManagerConfigurare()
        {
            // Inițializare setări implicite
            NumeSite = "InfoPress";
            Versiune = "1.0.0";
        }

        public static ManagerConfigurare GetInstance()
        {
            // Thread-safe Singleton using double-check locking
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ManagerConfigurare();
                    }
                }
            }
            return _instance;
        }

        public string NumeSite { get; set; }
        public string Versiune { get; set; }
    }
}