namespace MultiIdentityTest.Helpers
{
    public static class MyHelpers
    {
        public static string ProviderNameToChallengeScheme(string provider) => provider switch
        {
            "google" => MyChallengeSchemes.ContentIntegration,
            "kleos" => MyChallengeSchemes.Kleos,
            _ => MyChallengeSchemes.Kleos
        };
    }
}
