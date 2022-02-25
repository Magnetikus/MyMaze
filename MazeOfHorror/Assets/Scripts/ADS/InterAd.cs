using System;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class InterAd : MonoBehaviour
{
    [SerializeField] private GameObject _noLoadADS;

    private InterstitialAd _interstitialAd;

    private string _interstitialUnitID = "ca-app-pub-6994105792436021/1798688902";
    //private string _interstitialUnitID = "ca-app-pub-3940256099942544/1033173712"; // TEST!!!!!!!

    private void Start()
    {
        CreatAndLoadADD();
    }

    private void OnEnable()
    {
        _interstitialAd = new InterstitialAd(_interstitialUnitID);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _interstitialAd.LoadAd(adRequest);
    }

    private void CreatAndLoadADD()
    {
        _interstitialAd = new InterstitialAd(_interstitialUnitID);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _interstitialAd.LoadAd(adRequest);
        _interstitialAd.OnAdClosed += HandleRewardedAdClosed;
    }

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        _interstitialAd.Destroy();
        CreatAndLoadADD();
    }

    public void ShowAd()
    {
        if (_interstitialAd.IsLoaded() == true)
        {
            _interstitialAd.Show();
        }
        else
        {
            _noLoadADS.SetActive(true);
            Invoke("ClosedWindowNoADS", 2f);
        }
    }

    private void ClosedWindowNoADS()
    {
        _noLoadADS.SetActive(false);
    }
}
