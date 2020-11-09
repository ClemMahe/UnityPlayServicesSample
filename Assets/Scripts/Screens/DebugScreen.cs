using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpaceScavengersSocial;

public class DebugScreen : MonoBehaviour
{
    public ISocialServices socialServices;
    
    public Button playConnect, playDisconnect, getSavedState, increaseShipLevel, saveGameState;

    void Start()
    {
        playConnect.onClick.AddListener(connectPlayServices);
        playDisconnect.onClick.AddListener(disconnectPlayServices);
        initPlaygamesPlatform();
    }

    void initPlaygamesPlatform(){
        socialServices = FactorySocial.getSocialServices();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void connectPlayServices(){
        Debug.Log("connectPlayServices button called");
        socialServices.connectUser();
    }

    public void disconnectPlayServices(){
    }
}
