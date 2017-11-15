using UnityEngine;
using System.Collections;

public class GoogleScript : MonoBehaviour {
    // Use this for initialization
    void Start () {
    //    LoadCloud();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public string GetId()
    {
        return CGoogleplayGameServiceManager.GetNameGPGS();
    }

    //public void SaveCloud()
    //{
    //    CGoogleplayGameServiceManager.SaveToCloud("2048data");
    //}

    //public void LoadCloud()
    //{
    //    CGoogleplayGameServiceManager.LoadFromCloud("2048data");
    //}

    public void ShowLeaderBoard()
    {
        CGoogleplayGameServiceManager.ShowLeaderboard();
    }
}
