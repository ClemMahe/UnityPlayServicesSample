namespace SpaceScavengersSocial
{
    public delegate void SocialCallbackAuthentication(bool successResult);
    public delegate void SocialCallbackSaveGame(ESocialCloudState saveState);
    public delegate void SocialCallbackLoadGame(ESocialCloudState loadState, byte[] genericGameSave);
   
    public interface ISocialServices
    {
        void ConnectUser(SocialCallbackAuthentication successResult);
        void DisconnectUser();
        bool IsUserConnected();

        void SaveGame(ISaveGame objectToSave, SocialCallbackSaveGame saveDelegate);     
        void LoadGame(SocialCallbackLoadGame loadDelegate);
        void LeaderboardSetDefaultKeyForUI(string identifierDefaultLeaderboard);
        void LeaderboardReportScoreForKey(string leaderboardKey, long value);
        void LeaderboardShowDefaultUI();
    }

    public enum ESocialCloudState{
        ESocialCloudState_NotAuthenticated,
        ESocialCloudState_NotSupportedByPlatform,
        ESocialCloudState_Completed,
        ESocialCloudState_Failure_CannotOpenSavedGame,
        ESocialCloudState_Failure_CannotSaveGame,
        ESocialCloudState_Failure_CannotLoadGame
    }

}