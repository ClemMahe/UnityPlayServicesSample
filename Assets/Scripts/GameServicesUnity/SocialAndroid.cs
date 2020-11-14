using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

#if UNITY_ANDROID
    using GooglePlayGames;
    using GooglePlayGames.BasicApi;
    using GooglePlayGames.BasicApi.SavedGame;
#endif

namespace SpaceScavengersSocial
{
    public  class SocialAndroid : ISocialServices
    {
        public const string CLOUD_SAVE_FILENAME = "cloudsave-android";

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
                //LeaderBoard
            #endif
        }

        public void LeaderboardSetDefaultKeyForUI(string identifierDefaultLeaderboard){
            #if UNITY_ANDROID
                ((PlayGamesPlatform) Social.Active).SetDefaultLeaderboardForUI(identifierDefaultLeaderboard);
            #endif
        }

        public void ConnectUser(SocialCallbackAuthentication successResultCallback, bool silent){
            #if UNITY_ANDROID
                if(!silent){
                    PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) =>{
                        if(result==SignInStatus.Success){
                            successResultCallback.Invoke(true);
                        }
                    });
                }else{
                    //Silent login
                    PlayGamesPlatform.Instance.Authenticate ((bool success) => {
                        successResultCallback.Invoke(success);
                    }, true);
                }
            #else
                throw new Exception("Platform not valid");
            #endif  
        }

        public void DisconnectUser(){
            #if UNITY_ANDROID
                ((GooglePlayGames.PlayGamesPlatform) Social.Active).SignOut();
            #endif
        }

        public bool IsUserConnected(){
            return Social.localUser.authenticated;
        }

        public void SaveGame(ISaveGame objectToSave, SocialCallbackSaveGame saveDelegate){
            if(IsUserConnected()){
                #if UNITY_ANDROID
                    //TODO Implement Async. StartCoroutine?UniTask?
                    ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(CLOUD_SAVE_FILENAME, 
                        DataSource.ReadCacheOrNetwork, 
                        ConflictResolutionStrategy.UseLongestPlaytime, 
                        (SavedGameRequestStatus s, ISavedGameMetadata m)=> SaveGameProcessing(s,m,objectToSave,saveDelegate));
                #else
                    saveDelegate.Invoke(ESocialCloudState.ESocialCloudState_Failure_CannotSaveGame);
                #endif
            }else{
                saveDelegate.Invoke(ESocialCloudState.ESocialCloudState_NotAuthenticated);
            }
        }
        public void LoadGame(SocialCallbackLoadGame loadDelegate){
            if(IsUserConnected()){
                //TODO Implement Async. StartCoroutine?UniTask?
                #if UNITY_ANDROID
                    ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(CLOUD_SAVE_FILENAME,
                        DataSource.ReadCacheOrNetwork,
                        ConflictResolutionStrategy.UseLongestPlaytime,
                        (SavedGameRequestStatus s, ISavedGameMetadata m)=> LoadGameProcessing(s,m,loadDelegate));
                #else
                    saveDelegate.Invoke(ESocialCloudState.ESocialCloudState_Failure_CannotLoadGame);
                #endif
            }else{
                loadDelegate.Invoke(ESocialCloudState.ESocialCloudState_NotAuthenticated,null);
            }
        }

        private void SaveGameProcessing(SavedGameRequestStatus status, ISavedGameMetadata metaData, 
            ISaveGame game, SocialCallbackSaveGame saveResult){
            #if UNITY_ANDROID
                if (status == SavedGameRequestStatus.Success){
                    byte[] data = game.ObjectToBytes();
                    SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
                    SavedGameMetadataUpdate updatedMetadata = builder.Build();
                    ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(metaData, updatedMetadata, data,
                        //Lamda Result
                        (SavedGameRequestStatus statusCommit, ISavedGameMetadata metaDataCommit)=>{
                            if(statusCommit == SavedGameRequestStatus.Success){
                                saveResult.Invoke(ESocialCloudState.ESocialCloudState_Completed);
                            }else{
                                saveResult.Invoke(ESocialCloudState.ESocialCloudState_Failure_CannotSaveGame);
                            }
                        });
                }else{
                    saveResult.Invoke(ESocialCloudState.ESocialCloudState_Failure_CannotOpenSavedGame);
                }
            #else
                saveResult.Invoke(ESocialCloudState.ESocialCloudState_Failure_CannotOpenSavedGame);
            #endif
        }

        private void LoadGameProcessing(SavedGameRequestStatus status, ISavedGameMetadata metaData, 
           SocialCallbackLoadGame loadResult){
            #if UNITY_ANDROID
                if (status == SavedGameRequestStatus.Success){
                    ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(metaData, 
                        //Lamda Result
                        (SavedGameRequestStatus statusReading, byte[] bytes)=>{
                            if(statusReading == SavedGameRequestStatus.Success){
                                loadResult.Invoke(ESocialCloudState.ESocialCloudState_Completed, bytes);
                            }else{
                                loadResult.Invoke(ESocialCloudState.ESocialCloudState_Failure_CannotLoadGame, null);
                            }
                        }
                    );
                }else{
                    loadResult.Invoke(ESocialCloudState.ESocialCloudState_Failure_CannotOpenSavedGame, null);
                }
            #else 
                loadResult.Invoke(ESocialCloudState.ESocialCloudState_Failure_CannotOpenSavedGame, null);
            #endif
        }

        public void LeaderboardReportScoreForKey(string leaderboardKey, long value){
            if(IsUserConnected()){
                Social.ReportScore(value, leaderboardKey, (bool success) => {
                    //For we don't handle failures scenarios
                });
            }
        }
        public void LeaderboardShowDefaultUI(){
            if (IsUserConnected()){
                Social.ShowLeaderboardUI();
            }
        }
    }
}