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

    public static class MyAuthorizationPolicies
    {
        public static readonly string SimpleValuePolicy = "simpleValuePolicy";
    }
}
