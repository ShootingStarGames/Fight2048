using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdmobManager : MonoBehaviour
{
    private InterstitialAd interstitial;
    private BannerView banner;

    public void ShowInterstitialAd() // 전면광고 호출 함수
    {
        print("Show");
        interstitial.Show();
    }

    // 전면 광고 호출
    public void ShowInterstitialAdFunc() {
        print("ShowInterstitialAdFunc");
        interstitial = new InterstitialAd("ca-app-pub-4652101921040840/5104791419");
        interstitial.OnAdFailedToLoad += Interstitial_OnAdFailedToLoad;
        interstitial.OnAdLoaded += Interstitial_OnAdLoaded;
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }
    public void ShowBannerAdFunc() {
        print("ShowBannerAd");
        banner = new BannerView("ca-app-pub-4652101921040840/6295649815", AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        banner.LoadAd(request);
        banner.Show();
    }
    public void HideBannerAdFunc() {
        print("HideBannerAd");
        banner = new BannerView("ca-app-pub-4652101921040840/6295649815", AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        banner.LoadAd(request);
        banner.Hide();
    }
    void Start()
    {
    //    Screen.SetResolution(720, 1280, true);
    }

    private void Interstitial_OnAdLoaded(object sender, System.EventArgs e)
    {
        print("loaded");
        ShowInterstitialAd();

        throw new System.NotImplementedException();
    }

    private void Interstitial_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        print("failed");
        ShowInterstitialAd();

        throw new System.NotImplementedException();
    }

    void Update()
    {

    }
}