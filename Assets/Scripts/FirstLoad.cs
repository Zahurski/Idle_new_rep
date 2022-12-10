using System;
using System.Globalization;
using Ads;
using GasStation.Config;
using OilPump.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirstLoad : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI textMoneyOffline;
    [SerializeField] private GasStationConfig gasStation;
    [SerializeField] private OilPumpConfig oilPump;
    [SerializeField] private Button button;
    private RewardedAdsButton _ads;
    private InterstitialAdExample _interstitial;
    private UIManager _uiManager;
    
    private int _decreaseDiamond = 20;
    private float _moneySum;
    private float _totalMoney;
    private bool _active = false;

    private void Awake()
    {
        _ads = FindObjectOfType<RewardedAdsButton>();
        _interstitial = FindObjectOfType<InterstitialAdExample>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        RefreshDiamondButton();
        _moneySum = gasStation.Cost + oilPump.Cost;
        CalculateOfflineIncome();
        textMoneyOffline.text = "Пока вас не было\nвы заработали: " + FormatNums.FormatNum(_totalMoney);
        _ads.RewardedAdsShowComplete += GetBounty;
    }

    private void RefreshDiamondButton()
    {
        button.interactable = GameManager.Instance.Diamond > _decreaseDiamond;
    }
    
    public void Getx3()
    {
        GameManager.Instance.Diamond -= _decreaseDiamond;
        GameManager.Instance.Money += (float)Math.Round(_totalMoney * 3, 0);
        _uiManager.Close();
        _interstitial.ShowAd();
    }
    
    public void Getx2()
    {
        _active = true;
        _ads.ShowAd();
    }
    
    public void Getx1()
    {
        GameManager.Instance.Money += (float)Math.Round(_totalMoney, 0);
        print("Добавлено: " + (float)Math.Round(_totalMoney, 0));
        _ads.RewardedAdsShowComplete -= GetBounty;
        _uiManager.Close();
    }

    private void GetBounty()
    {
        if(!_active) return;
        GameManager.Instance.Money += (float)Math.Round(_totalMoney * 2, 0);
        print("Добавлено: " + (float)Math.Round(_totalMoney, 0));
        _ads.RewardedAdsShowComplete -= GetBounty;
        _active = false;
        _uiManager.Close();
    }
    
    private void CalculateOfflineIncome()
    {
        var lastPlayedTimeString = PlayerPrefs.GetString(GameManager.LastPlayedTime, null);
        if(lastPlayedTimeString == null) return;

        var lastPlayedTime = DateTime.Parse(lastPlayedTimeString, CultureInfo.CurrentCulture);
        int timeSpanRestriction = 2 * 60 * 60;
        double secondSpan = (DateTime.UtcNow - lastPlayedTime).TotalSeconds;

        if (secondSpan > timeSpanRestriction)
            secondSpan = timeSpanRestriction;

        _totalMoney = (float)secondSpan * _moneySum/10;
        print("Вас небыло в игре: " + secondSpan + "Вы заработали: " + _totalMoney);
    }
}
