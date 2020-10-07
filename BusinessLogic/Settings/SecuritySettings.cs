namespace BusinessLogic.Settings
{
    public class SecuritySettings
    {
        public SecuritySettings()
        {
        }

        public int ExpirationMinutes { get; set; }

        public string Issuer { get; set; }

        public string Key { get; set; }
    }
}
