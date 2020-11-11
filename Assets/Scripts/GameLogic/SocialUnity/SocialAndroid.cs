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
        
        public void connectUser(SocialCallbackAuthentication successResultCallback){
            Debug.Log("SocialAndroid, Connection : Started"); 
            #if UNITY_ANDROID
                Social.Active.localUser.Authenticate((bool successResult) =>
                {
                    Debug.Log("SocialAndroid, ConnectionResult : "+successResult); 
                    successResultCallback.Invoke(successResult);
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