using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpaceScavengersSocial;

public class DebugScreen : MonoBehaviour
{
    public GameManager gameManager;
    
    public Button btnPlayConnect, btnPlayDisconnect, btnIncreaseShipLevel;

    void Start()
    {
        //Listeners
        btnPlayConnect.onClick.AddListener(connectPlayServices);
        btnPlayDisconnect.onClick.AddListener(disconnectPlayServices);
        btnIncreaseShipLevel.onClick.AddListener(increaseShipLevel);
        gameManager = GameManager.getInstance();
        InitSocial();
    }

    void InitSocial(){
        if(gameManager.isUserConnected()){
            updateButtonsState(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void connectPlayServices(){
        Debug.Log("connectPlayServices button called");
        gameManager.connectUser((isConnected)=>{
            if(isConnected){
                updateButtonsState(true);
                //TODO save state locally
                //TODO updateUI
            }
        });
    }

    public void disconnectPlayServices(){
        Debug.Log("disconnectPlayServices button called");
        gameManager.disconnectUser();
        //UpdateUI
        updateButtonsState(false);
    }

    public void increaseShipLevel(){
        gameManager.increaseShipLevel();
    }

    public void updateButtonsState(bool isUserConnected){
        btnPlayConnect.interactable = !isUserConnected;
        btnPlayDisconnect.interactable = isUserConnected;
    }
}
