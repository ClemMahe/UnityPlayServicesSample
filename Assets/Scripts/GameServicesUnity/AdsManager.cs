using UnityEngine;
using UnityEngine.Advertisements;

namespace SpaceScavengersSocial
{
    public class AdsManager
    {
        private static AdsManager mInstance;
        private static string mGameId;
        private IUnityAdsListener mListener;
        bool testMode = true;
        string myPlacementId = "rewardedVideo";

        private AdsManager(string gameId, IUnityAdsListener listener){
            mGameId = gameId;
            mListener = listener;
            Advertisement.AddListener (mListener);
            Advertisement.Initialize (mGameId, testMode);
        }

        public static AdsManager GetInstance(string gameId, IUnityAdsListener listener){
            if(mInstance==null){
                mInstance = new AdsManager(gameId, listener);
            }
            return mInstance;
        }

        public void ShowRewardedVideo(){
            if (Advertisement.IsReady(myPlacementId)) {
                Advertisement.Show(myPlacementId);
            } 
            else {
                Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
            }
        }

        public void OnDestroy() {
            Advertisement.RemoveListener(mListener);
        }
    }

}