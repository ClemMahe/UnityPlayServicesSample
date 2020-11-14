namespace SpaceScavengersSocial
{
    public class AdsManager
    {
        private static AdsManager mInstance;
        private static string mGameId;
        private AdsManager(string gameId){
            mGameId = gameId;
        }

        public static AdsManager GetInstance(string gameId){
            if(mInstance==null){
                mInstance = new AdsManager(gameId);
            }
            return mInstance;
        }


    }
}