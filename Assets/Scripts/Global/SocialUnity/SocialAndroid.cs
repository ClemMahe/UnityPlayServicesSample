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
            public PlayGamesPlatform playGamesPlatform;
            public PlayGamesClientConfiguration config;
        #endif


        public SocialAndroid(){   
            #if UNITY_ANDROID
                config = new PlayGamesClientConfiguration.Builder()
                    .EnableSavedGames()
                    .RequestEmail()
                    .Build();
                PlayGamesPlatform.InitializeInstance(config);
                PlayGamesPlatform.DebugLogEnabled = true;
                playGamesPlatform = PlayGamesPlatform.Activate();
            #endif
        }

        public  void connectUser(){
            #if UNITY_ANDROID
                Social.Active.localUser.Authenticate((bool success) =>
                {
                    if (success){ 
                        Debug.Log("Authenticate success"); 
                    }else{
                        Debug.Log("Authenticate failure");
                    }
                });
            #else
                throw new Exception("Platform not valid");
            #endif  
        }

        public void disconnectUser(){
            ((GooglePlayGames.PlayGamesPlatform) Social.Active).SignOut();
        }
    }
}