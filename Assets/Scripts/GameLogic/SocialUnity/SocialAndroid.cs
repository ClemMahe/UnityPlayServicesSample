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
            ((PlayGamesPlatform) Social.Active).SetDefaultLeaderboardForUI(identifierDefaultLeaderboard);
        }

        public void ConnectUser(SocialCallbackAuthentication successResultCallback){
            #if UNITY_ANDROID
                Social.Active.localUser.Authenticate((bool successResult) =>
                {
                    successResultCallback.Invoke(successResult);
                });
            #else
                throw new Exception("Platform not valid");
            #endif  
        }

        public void DisconnectUser(){
            ((GooglePlayGames.PlayGamesPlatform) Social.Active).SignOut();
        }

        public bool IsUserConnected(){
            return Social.Active.localUser.authenticated;
        }

        public void SaveGame(ISaveGame objectToSave, SocialCallbackSaveGame saveDelegate){
            if(IsUserConnected()){
                //TODO Implement Async. StartCoroutine?UniTask?
                ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(CLOUD_SAVE_FILENAME, 
                    DataSource.ReadCacheOrNetwork, 
                    ConflictResolutionStrategy.UseLongestPlaytime, 
                    (SavedGameRequestStatus s, ISavedGameMetadata m)=> SaveGameProcessing(s,m,objectToSave,saveDelegate));
            }else{
                saveDelegate.Invoke(ESocialCloudState.ESocialCloudState_NotAuthenticated);
            }
        }
        public void LoadGame(SocialCallbackLoadGame loadDelegate){
            if(IsUserConnected()){
                //TODO Implement Async. StartCoroutine?UniTask?
                ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(CLOUD_SAVE_FILENAME,
                    DataSource.ReadCacheOrNetwork,
                    ConflictResolutionStrategy.UseLongestPlaytime,
                    (SavedGameRequestStatus s, ISavedGameMetadata m)=> LoadGameProcessing(s,m,loadDelegate));
            }else{
                loadDelegate.Invoke(ESocialCloudState.ESocialCloudState_NotAuthenticated,null);
            }
        }

        private void SaveGameProcessing(SavedGameRequestStatus status, ISavedGameMetadata metaData, 
            ISaveGame game, SocialCallbackSaveGame saveResult){
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
        }

        private void LoadGameProcessing(SavedGameRequestStatus status, ISavedGameMetadata metaData, 
           SocialCallbackLoadGame loadResult){
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