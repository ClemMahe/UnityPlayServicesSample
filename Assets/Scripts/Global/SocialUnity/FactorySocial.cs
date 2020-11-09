namespace SpaceScavengersSocial
{
    public static class FactorySocial
    {
        public static ISocialServices getSocialServices(){
            #if UNITY_ANDROID
                return new SocialAndroid();
            #else 
                return new SocialMacPc();
            #endif
        }
    }
}