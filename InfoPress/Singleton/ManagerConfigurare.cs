namespace InfoPress.Singleton
{
    public class ManagerConfigurare
    {
        private static ManagerConfigurare instance;

        private ManagerConfigurare()
        {
        }

        public static ManagerConfigurare GetInstance()
        {
            if (instance == null)
            {
                instance = new ManagerConfigurare();
            }

            return instance;
        }

        public string NumeSite { get; set; } = "InfoPress";
    }
}