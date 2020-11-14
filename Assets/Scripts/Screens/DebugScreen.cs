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
        btnPlayConnect.onClick.AddListener(ConnectPlayServices);
        btnPlayDisconnect.onClick.AddListener(DisconnectPlayServices);
        btnIncreaseShipLevel.onClick.AddListener(IncreaseShipLevel);
        gameManager = GameManager.GetInstance();
        Init();
    }

    void Init(){      
        UpdateButtonsState(gameManager.IsUserConnected());
        gameManager.LoadCloudSave();
    }

    // Update is called once per frame
    void Update()
    {
        int playerLevel = gameManager.GetShipLevel();
        string textToUpdate = "Player level : "+playerLevel+"\n";
        if(gameManager.IsUserConnected()){
            textToUpdate+= "CloudState connected";
        }else{
            textToUpdate+= "CloudState disconnected";
        }
        //Update text
        textPlayer.text = textToUpdate;
    }

    public void ConnectPlayServices(){
        Debug.Log("connectPlayServices button called");
        gameManager.ConnectUser((isConnected)=>{
            if(isConnected){
                UpdateButtonsState(true);
                gameManager.LoadCloudSave();
            }
        });
    }

    public void DisconnectPlayServices(){
        Debug.Log("disconnectPlayServices button called");
        gameManager.DisconnectUser();
        //UpdateUI
        UpdateButtonsState(false);
    }

    public void IncreaseShipLevel(){
        gameManager.IncreaseShipLevel();
    }

    public void UpdateButtonsState(bool isUserConnected){
        btnPlayConnect.interactable = !isUserConnected;
        btnPlayDisconnect.interactable = isUserConnected;
    }
    
}
