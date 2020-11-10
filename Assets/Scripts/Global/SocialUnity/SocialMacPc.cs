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

        public  void connectUser(){
            Debug.Log("Connect user desktop");
        }

        public void disconnectUser(){
            Debug.Log("Disconnect user desktop");
        }
    }
}