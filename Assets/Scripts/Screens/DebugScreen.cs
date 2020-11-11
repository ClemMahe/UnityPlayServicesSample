using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpaceScavengersSocial;

public class DebugScreen : MonoBehaviour
{
    public ISocialServices socialServices;
    
    public Button btnPlayConnect, btnPlayDisconnect, btnIncreaseShipLevel;

    void Start()
    {
        btnPlayConnect.onClick.AddListener(connectPlayServices);
        btnPlayDisconnect.onClick.AddListener(disconnectPlayServices);
        btnIncreaseShipLevel.onClick.AddListener(increaseShipLevel);
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
        socialServices.connectUser((isConnected)=>{
            if(isConnected){
                updateButtonsState(true);
                //TODO save state locally
                //TODO updateUI
            }
        });
    }

    public void disconnectPlayServices(){
        Debug.Log("disconnectPlayServices button called");
        socialServices.disconnectUser();
        updateButtonsState(false);
        //TODO remove user/state & everything
        //TODO updateUI
    }

    public void increaseShipLevel(){
        
    }

    public void updateButtonsState(bool isUserConnected){
        btnPlayConnect.interactable = !isUserConnected;
        btnPlayDisconnect.interactable = isUserConnected;
        btnIncreaseShipLevel.interactable = isUserConnected;
    }
}
