using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpaceScavengersSocial;

public class DebugScreen : MonoBehaviour
{
    public GameManager gameManager;
    
    public Button btnPlayConnect, btnPlayDisconnect, btnIncreaseShipLevel;
    public Text textPlayer;

    void Start()
    {
        //Listeners
        btnPlayConnect.onClick.AddListener(connectPlayServices);
        btnPlayDisconnect.onClick.AddListener(disconnectPlayServices);
        btnIncreaseShipLevel.onClick.AddListener(increaseShipLevel);
        gameManager = GameManager.getInstance();
        Init();
    }

    void Init(){      
        updateButtonsState(gameManager.isUserConnected());
        //need to establish a strategy regarding cloud/local sync
        gameManager.LoadCloudSave();
    }

    // Update is called once per frame
    void Update()
    {
        int playerLevel = gameManager.getShipLevel();
        string textToUpdate = "Player level : "+playerLevel+"\n";
        if(gameManager.isUserConnected()){
            textToUpdate+= "CloudState connected";
        }else{
            textToUpdate+= "CloudState disconnected";
        }
        //Update text
        textPlayer.text = textToUpdate;
    }

    public void connectPlayServices(){
        Debug.Log("connectPlayServices button called");
        gameManager.connectUser((isConnected)=>{
            if(isConnected){
                updateButtonsState(true);
                gameManager.LoadCloudSave();
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
