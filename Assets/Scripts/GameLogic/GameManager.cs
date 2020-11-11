using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceScavengersSocial;

public class GameManager
{
    private static GameManager gameManagerInstance;
    private static ISocialServices socialServices;

    private GameManager(){
        socialServices = FactorySocial.getSocialServices();
    }

    public static GameManager getInstance(){
        if(gameManagerInstance==null){
            gameManagerInstance = new GameManager();
        }
        return gameManagerInstance;
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
    public void increaseShipLevel(){
    }


}
