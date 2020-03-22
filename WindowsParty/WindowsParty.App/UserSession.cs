namespace WindowsParty.App
{
    public static class UserSession
    {
        public static string BearerToken { get; private set; }

        public static void SetBearerToken(string value)
        {
            BearerToken = value;
        }

        public static void ClearSession()
        {
            BearerToken = null;
        }
    }
}
