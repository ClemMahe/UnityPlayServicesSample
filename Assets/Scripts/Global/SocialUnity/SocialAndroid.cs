using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

#if UNITY_ANDROID
    using GooglePlayGames;
    using GooglePlayGames.BasicApi;
#endif

namespace SpaceScavengersSocial
{
    public  class SocialAndroid : ISocialServices
    {

//SocialAndroid variables
        #if UNITY_ANDROID
            public PlayGamesClientConfiguration config;
        #endif


        public SocialAndroid(){   
            #if UNITY_ANDROID
                config = new PlayGamesClientConfiguration.Builder()
                // enables saving game progress.
                .EnableSavedGames()
                // requests the email address of the player be available.
                // Will bring up a prompt for consent.
                .RequestEmail()
                // requests a server auth code be generated so it can be passed to an
                //  associated back end server application and exchanged for an OAuth token.
                .RequestServerAuthCode(false)
                // requests an ID token be generated.  This OAuth token can be used to
                //  identify the player to other services such as Firebase.
                .RequestIdToken()
                .Build();
                PlayGamesPlatform.InitializeInstance(config);
                // recommended for debugging:
                PlayGamesPlatform.DebugLogEnabled = true;
                // Activate the Google Play Games platform
                PlayGamesPlatform.Activate();
            #endif
        }

        public  void connectUser(){
            #if UNITY_ANDROID
                Social.localUser.Authenticate((bool success) =>
                {
                    if (success){ 
                        Debug.Log("Authenticate success"); 
                    }else{
                        Debug.Log("Authenticate failure");
                    }
                });
            #else
                throw new Exception("");
            #endif  
        }
    }
}