using System;
using UnityEngine;
using UnityEngine.Advertisements;
using SpaceScavengersSocial;

public class GameManager
{
    public const String LEADERBOARD_ID_LEVEL = "CgkIwZWQ_-EDEAIQAQ";

    #if UNITY_ANDROID
        public const String UNITY_APP_ADS_ID = "3902011";
    #elif UNITY_IOS
        public const String UNITY_APP_ADS_ID = "3902010";
    #endif

    private static GameManager gameManagerInstance;
    private static ISocialServices socialServices;
    private static AdsManager adsManager;
    private PlayerData playerData;


    private GameManager(){
        socialServices = FactorySocial.GetSocialServices();
        socialServices.LeaderboardSetDefaultKeyForUI(LEADERBOARD_ID_LEVEL);
        playerData = PlayerData.LoadFromDisk();
    }

    public static GameManager GetInstance(){
        if(gameManagerInstance==null){
            gameManagerInstance = new GameManager();
        }
        return gameManagerInstance;
    }

    public void LoadCloudSave(){
        if(IsUserConnected()){ //Already handled inside Social methods, but game can implement its own way
            socialServices.LoadGame((ESocialCloudState loadState, byte[] genericGameSave) =>{
                if(loadState==ESocialCloudState.ESocialCloudState_Completed && genericGameSave.Length>0){
                    try{
                        PlayerData cloudPdata = PlayerData.BytesToObject(genericGameSave);
                        bool mergeNeeded = playerData.MergeLocalWithCloud(cloudPdata);
                        if(mergeNeeded){
                            SaveCloud();
                            //Leaderboard update
                            socialServices.LeaderboardReportScoreForKey(LEADERBOARD_ID_LEVEL,playerData.playerLevel);
                        }
                    }catch(Exception){
                        //Handle failure cases
                    }
                }
            });
        }
    }
    public void SaveCloud(){
        if(IsUserConnected()){
            socialServices.SaveGame(playerData,(ESocialCloudState saveState)=>{
                if(saveState!=ESocialCloudState.ESocialCloudState_Completed){
                    //Handle failure cases
                }
            });
        }
    }

    public void IncreaseShipLevel(){
        //Save
        playerData.playerLevel = playerData.playerLevel+1;
        playerData.SaveToDisk();
        SaveCloud();
        //Leaderboard update
        socialServices.LeaderboardReportScoreForKey(LEADERBOARD_ID_LEVEL,playerData.playerLevel);
    }
    public int GetShipLevel(){
        return playerData.playerLevel;
    }
    public void ConnectUser(SocialCallbackAuthentication successResult, bool silentMode){
        socialServices.ConnectUser(successResult, silentMode);
    }
    public void DisconnectUser(){
        socialServices.DisconnectUser();
        //TODO remove user/state & everything
    }
    public bool IsUserConnected(){
        return socialServices.IsUserConnected();
    }

    public void ShowLeaderBoard(){
        socialServices.LeaderboardShowDefaultUI();
    }

    public void InitAdsManager(IUnityAdsListener listener){
        adsManager = AdsManager.GetInstance(UNITY_APP_ADS_ID, listener);
    }
    public void ShowAd(){
        if(adsManager!=null){
            adsManager.ShowRewardedVideo();
        }else{
            throw new Exception("Ads Manager not initialized");
        }
    }
    
}
