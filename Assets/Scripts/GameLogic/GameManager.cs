using System;
using UnityEngine;
using SpaceScavengersSocial;

public class GameManager
{
    private static GameManager gameManagerInstance;
    private static ISocialServices socialServices;
    private PlayerData playerData;

    private GameManager(){
        socialServices = FactorySocial.GetSocialServices();
        //Load from disk & cloud to compare 
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
    }
    public int GetShipLevel(){
        return playerData.playerLevel;
    }
    public void ConnectUser(SocialCallbackAuthentication successResult){
        socialServices.ConnectUser(successResult);
    }
    public void DisconnectUser(){
        socialServices.DisconnectUser();
        //TODO remove user/state & everything
    }
    public bool IsUserConnected(){
        return socialServices.IsUserConnected();
    }
    
}
