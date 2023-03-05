using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class InterAd : MonoBehaviour
{
    [SerializeField] private GameObject _noLoadADS;

    private InterstitialAd _interstitialAd;

    private string _interstitialUnitID = "ca-app-pub-3940256099942544/1033173712"; // TEST!!!!!!!

    private void OnEnable()
    {
        _interstitialAd = new InterstitialAd(_interstitialUnitID);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _interstitialAd.LoadAd(adRequest);
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
