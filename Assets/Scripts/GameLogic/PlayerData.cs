using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceScavengersSocial;

public class PlayerData : ISaveGame
{
    private const string SAVE_KEY = "spacescavengers-save";
    private const string CURRENT_VERSION = "SCv1";

    public int playerLevel;
    public PlayerData(){
    }
    public void increaseLevel(){
        playerLevel++;
    }


    public bool MergeLocalWithCloud(PlayerData cloudGame){
        //In this version we will keep a simple version
        bool mergeNeeded = false;
        if(this.playerLevel < cloudGame.playerLevel){
            mergeNeeded = true;
            this.playerLevel = cloudGame.playerLevel;   
        }
        return mergeNeeded;
    }

    public static PlayerData LoadFromDisk()
    {
        string s = PlayerPrefs.GetString(SAVE_KEY, "");
        if (s == null || s.Trim().Length == 0)
        {
            return new PlayerData();
        }
        return PlayerData.FromString(s);
    }
    public void SaveToDisk()
    {
        PlayerPrefs.SetString(SAVE_KEY, ToString());
    }
    public static PlayerData BytesToObject(byte[] b){
        return PlayerData.FromString(System.Text.ASCIIEncoding.Default.GetString(b));
    }
    public byte[] ObjectToBytes(){
        return System.Text.ASCIIEncoding.Default.GetBytes(ToString());
    }
    public override string ToString(){
        string s = "SCv1:" + playerLevel.ToString();
        return s;
    }
    public static PlayerData FromString(string s){
        PlayerData pd = new PlayerData();
        string[] p = s.Split(new char[] {':'});
        if (!p[0].StartsWith("SCv1"))
        {
            //Unrecognized version of format - new game 
            Debug.LogError("Failed to parse game progress from: " + s);
            return pd;
        }
        pd.playerLevel = System.Convert.ToInt32(p[1]);
        return pd;
    }

        


}
