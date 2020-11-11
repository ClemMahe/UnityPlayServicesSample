using System.Collections;
using System.Collections.Generic;
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

    public void increaseShipLevel(){
        //Save
        playerData.SaveToDisk();
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
