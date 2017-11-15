using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

using System.Collections.Generic;

public class StoreCashScript : MonoBehaviour {
    public GameObject AdResultPanel;
    public GameObject AdReadyPanel;
    public Text DiaText;
    public Text GoldText;
    public Text AdGoldText;

    public Button AdGoldBtn;
    public Text AdGoldBtnText;
    public Button AdDiaBtn;
    public Text AdDiaBtnText;

    private bool isAdTypeGold = false;

    // 인앱 결제(광고제거)
    public void RemoveAds()
    {

    }
    // 벙글 광고부분
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Vungle.onPause();
        }
        else
        {
            Vungle.onResume();
        }
    }
    /* Setup EventHandlers for all available Vungle events */
    private void initializeEventHandlers()
    {

        //Event triggered during when an ad is about to be played
        Vungle.onAdStartedEvent += () => {
            Debug.Log("Ad event is starting!  Pause your game  animation or sound here.");
        };

        //Event is triggered when a Vungle ad finished and provides the entire information about this event
        //These can be used to determine how much of the video the user viewed, if they skipped the ad early, etc.
        Vungle.onAdFinishedEvent += (args) => {
            Debug.Log("Ad finished - watched time:" + args.TimeWatched + ", total duration:" + args.TotalDuration
                      + ", was call to action clicked:" + args.WasCallToActionClicked + ", is completed view:"
                      + args.IsCompletedView);
            // 끝까지 광고 시청을 했다면?
            if (args.IsCompletedView)
            {
                if (isAdTypeGold)
                {
                    GetGoldAfterAdFinished();
                }
                else {
                    GetDiaAfterAdFinished();
                }
            }
            else {
                // 안했다면?
            }
        };

        //Event is triggered when the ad's playable state has been changed
        //It can be used to enable certain functionality only accessible when ad plays are available
        Vungle.adPlayableEvent += (isAdAvailable) =>
        {
            if (isAdAvailable)
            {
                Debug.Log("An ad is ready to show!");
            }
            else
            {
                Debug.Log("No ad is available at this moment.");
            }
        };

        //Fired log event from sdk
        Vungle.onLogEvent += (log) => {
            Debug.Log("Log: " + log);
        };

    }
    //
#if UNITY_ADS
      //
    // 광고 보기
    private void ShowRewardedAd_Unity()
    {

        if (Advertisement.IsReady("rewardedVideo_"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult_Unity };
            Advertisement.Show("rewardedVideo_", options);
        }
        else
        {
            // 준비가 안됐다면 벙글을 준비한다.
            if (Vungle.isAdvertAvailable())
            {
                Dictionary<string, object> options = new Dictionary<string, object>();
                options["incentivized"] = true;
                Vungle.playAdWithOptions(options);
            }
            else
            {
                AdReadyPanel.SetActive(true);
            }
        }
    }
    private void HandleShowResult_Unity(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                if (isAdTypeGold)
                    GetGoldAfterAdFinished();
                else
                    GetDiaAfterAdFinished();
                break;
            case ShowResult.Skipped:
                //Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                //Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
#endif
    // 정상적으로 끝났을 때 다이아를 얻음
    private void GetDiaAfterAdFinished()
    {
        FileData.OffAdDiaCooldown();
        //Debug.Log("The ad was successfully shown.");
        // 다이아 얻기
        int dia = Random.Range(1, 3);
        FileData.SetDiamond(dia);
        DiaText.text = FileData.GetDiamond().ToString();

        AdResultPanel.GetComponentInChildren<Text>().text = "다이아 " + dia + "개 획득!";

        // YOUR CODE TO REWARD THE GAMER
        // Give coins etc.
        // 경험치 획득 패널 보여줌~
        AdResultPanel.SetActive(true);
    }
    // 정상적으로 끝났을 때 골드를 얻음
    private void GetGoldAfterAdFinished() {
        FileData.OffAdGoldCooldown();
        //Debug.Log("The ad was successfully shown.");
        // 골드 얻기
        // 층수 얻기
        int floor = FileData.GetFloor();
        int gold = Random.Range(50 * floor, 120 * floor);
        FileData.SetGold(FileData.GetGold() + gold);
        GoldText.text = FileData.GetGold().ToString();

        AdResultPanel.GetComponentInChildren<Text>().text = "골드 " + gold + "개 획득!";

        // YOUR CODE TO REWARD THE GAMER
        // Give coins etc.
        // 경험치 획득 패널 보여줌~
        AdResultPanel.SetActive(true);
    }

    // 광고보기(다이아 얻기)
    public void AdShowToGetDia()
    {
        Debug.Log("AdShowToGetDia");
        isAdTypeGold = false;
#if UNITY_ADS
        ShowRewardedAd_Unity();
#endif
    }

    // 광고보기(골드 얻기)
    public void AdShowToGetGold()
    {
        isAdTypeGold = true;
#if UNITY_ADS
        ShowRewardedAd_Unity();
#endif
    }

    // 다이아를 골드로 환전
    public void ChangeGold()
    {

    }

    // 결과창 닫아줌
    public void CloseResultPanel() {
        AdResultPanel.SetActive(false);
    }
    // 준비안됐다는 창 닫아줌
    public void CloseReadyPanel()
    {
        AdReadyPanel.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        Vungle.init("58126cb7862ce57e2800008b", string.Empty);
        initializeEventHandlers();
    }
	
	// Update is called once per frame
	void Update () {
        // 광고보기 골드
        int floor = FileData.GetFloor();
        AdGoldText.text = "골드 "+(floor*50).ToString() + "~" + (floor*120).ToString()+"개를 획득합니다.\n쿨타임: 15분";

        // 골드 광고 볼수 있는 건지 확인좀
        if (FileData.GetIsAdGoldCooldown() == 1)
        {
            AdGoldBtnText.text = "시청하기";
            AdGoldBtn.interactable = true;
        }
        else {
            AdGoldBtn.interactable = false;
            // 쿨타임 얼마 남았는지 확인하기
            string t = FileData.GetAdGoldCooldown();
            // 만약 쿨타임이 남지 않았다면?
            if (t.CompareTo("-1") == 0)
            {
                // On 해줌
                FileData.OnAdGoldCooldown();
                return;
            }
            else {
                // 쿨타임이 남았다면 쿨타임 넣어줌
                AdGoldBtnText.text = t;
            }
        }

        // 광고보기 다이아
        // 다이아 광고 볼수 있는 건지 확인좀
        if (FileData.GetIsAdDiaCooldown() == 1)
        {
            AdDiaBtnText.text = "시청하기";
            AdDiaBtn.interactable = true;
        }
        else
        {
            AdDiaBtn.interactable = false;
            // 쿨타임 얼마 남았는지 확인하기
            string t = FileData.GetAdDiaCooldown();
            // 만약 쿨타임이 남지 않았다면?
            if (t.CompareTo("-1") == 0)
            {
                // On 해줌
                FileData.OnAdDiaCooldown();
                return;
            }
            else
            {
                // 쿨타임이 남았다면 쿨타임 넣어줌
                AdDiaBtnText.text = t;
            }
        }
    }
}
