using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAd : MonoBehaviour
{
    private BannerView _bannerView;

    private string _bannerUnitId = "ca-app-pub-3940256099942544/6300978111";

    private void OnEnable()
    {
        _bannerView = new BannerView(_bannerUnitId, AdSize.Banner, AdPosition.Top);

        _bannerView.OnAdLoaded += HandleOnAdLoaded;
        _bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        _bannerView.OnAdOpening += HandleOnAdOpened;

        AdRequest adRequest = new AdRequest.Builder().Build();
        _bannerView.LoadAd(adRequest);
        _bannerView.Hide();
    }

    public void ShowBanner()
    {
        _bannerView.Show();
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("HandleFailedToReceiveAd event received with message: "
        + args.LoadAdError.GetMessage());
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        _bannerView.Hide();
        Debug.Log("HandleAdOpened event received");
    }
}
