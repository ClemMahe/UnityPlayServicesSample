using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpaceScavengersSocial;
using UnityEngine.Advertisements;

public class DebugScreen : MonoBehaviour, IUnityAdsListener
{
    public GameManager gameManager;
    
    public Button btnPlayConnect, btnPlayDisconnect, btnIncreaseShipLevel, btnLeaderboard, btnShowAd;
    public Text textPlayer;
    public string adState;

    void Start()
    {
        //Listeners
        btnPlayConnect.onClick.AddListener(ConnectPlayServicesBtnClicked);
        btnPlayDisconnect.onClick.AddListener(DisconnectPlayServices);
        btnIncreaseShipLevel.onClick.AddListener(IncreaseShipLevel);
        btnLeaderboard.onClick.AddListener(ShowLeaderboard);
        btnShowAd.onClick.AddListener(ShowAd);
        gameManager = GameManager.GetInstance();
        InitGameManager();
    }

    void InitGameManager(){    
        //PlayServices
        gameManager.ConnectUser((isConnected)=>{
            if(isConnected){
                UpdateButtonsState(true);
                gameManager.LoadCloudSave();
            }
        },true);
        //Ads manager
        gameManager.InitAdsManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager!=null){
            int playerLevel = gameManager.GetShipLevel();
            string textToUpdate = "Player level : "+playerLevel+"\n";
            if(gameManager.IsUserConnected()){
                textToUpdate+= "CloudState connected \n";
            }else{
                textToUpdate+= "CloudState disconnected \n";
            }
            if(adState!=null){
                textToUpdate += adState;
            }
            //Update text
            textPlayer.text = textToUpdate;
        }
    }

    public void ConnectPlayServicesBtnClicked(){
        Debug.Log("connectPlayServices button called");
        gameManager.ConnectUser((isConnected)=>{
            if(isConnected){
                UpdateButtonsState(true);
                gameManager.LoadCloudSave();
            }
        },false); //Not silently
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

    public void ShowLeaderboard(){
        gameManager.ShowLeaderBoard();
    }

    public void ShowAd(){
        gameManager.ShowAd();
    }

    public void UpdateButtonsState(bool isUserConnected){
        btnPlayConnect.interactable = !isUserConnected;
        btnPlayDisconnect.interactable = isUserConnected;
        btnLeaderboard.interactable = isUserConnected;
    }

    //Ads methods
    public void OnUnityAdsReady (string placementId) {
        btnShowAd.interactable=true;
    }
    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        if (showResult == ShowResult.Finished) {
            adState = "Ad Completed";
        } else if (showResult == ShowResult.Skipped) {
            adState = "Ad Skipped";
        } else if (showResult == ShowResult.Failed) {
            adState = "Ad Failed";
        }
    }
    public void OnUnityAdsDidError (string message) {
        adState = "AdError: "+message;
    }
    public void OnUnityAdsDidStart (string placementId) {
        adState = "Ad Started";
    } 
    

}
