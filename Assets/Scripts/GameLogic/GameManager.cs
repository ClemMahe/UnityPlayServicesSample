using System;
using UnityEngine;
using SpaceScavengersSocial;

public class GameManager
{
    private static GameManager gameManagerInstance;
    private static ISocialServices socialServices;
    private PlayerData playerData;

    private GameManager(){
        socialServices = FactorySocial.getSocialServices();
        //Load from disk & cloud to compare 
        playerData = PlayerData.LoadFromDisk();
    }

    public static GameManager getInstance(){
        if(gameManagerInstance==null){
            gameManagerInstance = new GameManager();
        }
        return gameManagerInstance;
    }

    public void LoadCloudSave(){
        if(isUserConnected()){ //Already handled inside Social methods, but game can implement its own way
            socialServices.loadGame((ESocialCloudState loadState, byte[] genericGameSave) =>{
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
        if(isUserConnected()){
            socialServices.saveGame(playerData,(ESocialCloudState saveState)=>{
                if(saveState!=ESocialCloudState.ESocialCloudState_Completed){
                    //Handle failure cases
                }
            });
        }
    }

    public void increaseShipLevel(){
        //Save
        playerData.playerLevel = playerData.playerLevel+1;
        playerData.SaveToDisk();
        SaveCloud();
    }
    public int getShipLevel(){
        return playerData.playerLevel;
    }
    public void connectUser(SocialCallbackAuthentication successResult){
        socialServices.connectUser(successResult);
    }
    public void disconnectUser(){
        socialServices.disconnectUser();
        //TODO remove user/state & everything
    }
    public bool isUserConnected(){
        return socialServices.isUserConnected();
    }
    
}
