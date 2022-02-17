namespace MultiIdentityTest.Helpers
{
    public static class MyAuthenticationSchemes
    {
        public static readonly string RegularScheme = "AuthSchemeFuffaUno";
        public static readonly string CiScheme = "AuthSchemeFuffaDue";
    }

    public static class MyChallengeSchemes
    {
        public static readonly string ContentIntegration = "contentIntegration";
        public static readonly string Kleos = "kleosIdentityServer";
    }
}
