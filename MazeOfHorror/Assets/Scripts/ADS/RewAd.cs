using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class RewAd : MonoBehaviour
{
    [SerializeField] private WinMenu _winMenu;
    [SerializeField] private ShopMenu _shopMenu;
    [SerializeField] private LosMenu _losMenu;
    [SerializeField] private GameObject _noLoadADS;

    private RewardedAd _rewardedAd;
    private int _prize;
    private string _rewardUnitID = "ca-app-pub-3940256099942544/5224354917"; //TEST!!!!!!!!!!!

    private void Start()
    {
        CreateAndLoadRewardedAd();
    }

    private void CreateAndLoadRewardedAd()
    {
        _rewardedAd = new RewardedAd(_rewardUnitID);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(adRequest);
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;
    }

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        _rewardedAd.Destroy();
        CreateAndLoadRewardedAd();
    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        switch (_prize)
        {
            case 1:
                _shopMenu.ADSfromShop();
                break;
            case 2:
                _winMenu.X2();
                break;
            case 3:
                _losMenu.ADS();
                break;
            default:
                print("OPS(((");
                break;
        }
    }

    public void ShowAd(int prize)
    {
        if (_rewardedAd.IsLoaded() == true)
        {
            _prize = prize;
            _rewardedAd.Show();
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
