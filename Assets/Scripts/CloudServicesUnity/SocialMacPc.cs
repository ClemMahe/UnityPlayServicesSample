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

        public void ConnectUser(SocialCallbackAuthentication successResultCallback, bool silent){
            Debug.Log("SocialMacPc, Connection : Not implemented so far on PC/Mac");
            successResultCallback.Invoke(false);
        }

        public void DisconnectUser(){
            Debug.Log("SocialMacPc, DisconnectUser : Not implemented so far on PC/Mac");
        }

        public bool IsUserConnected(){
            Debug.Log("SocialMacPc, IsUserConnected : Not implemented so far on PC/Mac");
            return false ;
        }

        public void SaveGame(ISaveGame objectToSave, SocialCallbackSaveGame saveDelegate){
            Debug.Log("SocialMacPc, SaveGame : Not implemented so far on PC/Mac");
            saveDelegate.Invoke(ESocialCloudState.ESocialCloudState_NotSupportedByPlatform);
        }

        public void LoadGame(SocialCallbackLoadGame loadDelegate){
            Debug.Log("SocialMacPc, LoadGame : Not implemented so far on PC/Mac");
            loadDelegate.Invoke(ESocialCloudState.ESocialCloudState_NotSupportedByPlatform, null);
        }

        public void LeaderboardSetDefaultKeyForUI(string identifierDefaultLeaderboard){
            Debug.Log("SocialMacPc, LeaderboardSetDefaultKeyForUI : Not implemented so far on PC/Mac");
        }

        public void LeaderboardReportScoreForKey(string leaderboardKey, long value){
            Debug.Log("SocialMacPc, LeaderboardReportScoreForKey : Not implemented so far on PC/Mac");
        } 
        public void LeaderboardShowDefaultUI(){
            Debug.Log("SocialMacPc, LeaderboardShowDefaultUI : Not implemented so far on PC/Mac");
        }
    }
}