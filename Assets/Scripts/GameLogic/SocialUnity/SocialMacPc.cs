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

        public void ConnectUser(SocialCallbackAuthentication successResultCallback){
            Debug.Log("SocialMacPc, Connection : No social network configured for PC/Mac");
            successResultCallback.Invoke(true); //Debug purpose -> true
        }

        public void DisconnectUser(){
            Debug.Log("SocialMacPc, Disconnection : No social network configured for PC/Mac");
        }

        public bool IsUserConnected(){
            return true ; //Debug purpose -> true
        }

        public void SaveGame(ISaveGame objectToSave, SocialCallbackSaveGame saveDelegate){
            saveDelegate.Invoke(ESocialCloudState.ESocialCloudState_NotSupportedByPlatform);
        }

        public void LoadGame(SocialCallbackLoadGame loadDelegate){
            loadDelegate.Invoke(ESocialCloudState.ESocialCloudState_NotSupportedByPlatform, null);
        }
    }
}