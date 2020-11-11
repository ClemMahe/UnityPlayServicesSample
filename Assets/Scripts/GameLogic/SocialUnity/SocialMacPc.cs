using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace SpaceScavengersSocial
{
    public class SocialMacPc : ISocialServices
    {
        public SocialMacPc(){
        }

        public void connectUser(SocialCallbackAuthentication successResultCallback){
            Debug.Log("SocialMacPc, Connection : No social network configured for PC/Mac");
            successResultCallback.Invoke(true); //Debug purpose -> true
        }

        public void disconnectUser(){
            Debug.Log("SocialMacPc, Disconnection : No social network configured for PC/Mac");
        }

        public bool isUserConnected(){
            return true ; //Debug purpose -> true
        }
    }
}