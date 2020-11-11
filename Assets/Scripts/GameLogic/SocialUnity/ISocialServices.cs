namespace SpaceScavengersSocial
{
    public delegate void SocialCallbackAuthentication(bool successResult);
   
    public interface ISocialServices
    {
        void connectUser(SocialCallbackAuthentication successResult);
        void disconnectUser();
    }
}