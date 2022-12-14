using System;
using System.Globalization;
using IdleTycoon.Ads;
using IdleTycoon.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleTycoon
{
    public class FirstLoad : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMoneyOffline;
        [SerializeField] private GasStationConfig gasStation;
        [SerializeField] private OilPumpConfig oilPump;
        [SerializeField] private Button button;

        private RewardedAdsButton ads;
        private InterstitialAdExample interstitial;
        private UIManager uiManager;

        private int decreaseDiamond = 20;
        private float moneySum;
        private float totalMoney;
        private bool active = false;

        private void Awake()
        {
            ads = FindObjectOfType<RewardedAdsButton>();
            interstitial = FindObjectOfType<InterstitialAdExample>();
            uiManager = FindObjectOfType<UIManager>();
        }

        private void Start()
        {
            RefreshDiamondButton();
            moneySum = gasStation.Cost + oilPump.Cost;
            CalculateOfflineIncome();
            textMoneyOffline.text = "Пока вас не было\nвы заработали: " + FormatNums.FormatNum(totalMoney);
            ads.RewardedAdsShowComplete += GetBounty;
        }

        private void RefreshDiamondButton()
        {
            button.interactable = GameManager.Instance.Diamond > decreaseDiamond;
        }

        public void Getx3()
        {
            GameManager.Instance.Diamond -= decreaseDiamond;
            GameManager.Instance.Money += (float)Math.Round(totalMoney * 3, 0);
            uiManager.Close();
            interstitial.ShowAd();
        }

        public void Getx2()
        {
            active = true;
            ads.ShowAd();
        }

        public void Getx1()
        {
            GameManager.Instance.Money += (float)Math.Round(totalMoney, 0);
            print("Добавлено: " + (float)Math.Round(totalMoney, 0));
            ads.RewardedAdsShowComplete -= GetBounty;
            uiManager.Close();
        }

        private void GetBounty()
        {
            if (!active) return;
            GameManager.Instance.Money += (float)Math.Round(totalMoney * 2, 0);
            print("Добавлено: " + (float)Math.Round(totalMoney, 0));
            ads.RewardedAdsShowComplete -= GetBounty;
            active = false;
            uiManager.Close();
        }

        private void CalculateOfflineIncome()
        {
            var lastPlayedTimeString = PlayerPrefs.GetString(GameManager.LastPlayedTime, null);
            if (lastPlayedTimeString == null) return;

            var lastPlayedTime = DateTime.Parse(lastPlayedTimeString, CultureInfo.CurrentCulture);
            int timeSpanRestriction = 2 * 60 * 60;
            double secondSpan = (DateTime.UtcNow - lastPlayedTime).TotalSeconds;

            if (secondSpan > timeSpanRestriction)
                secondSpan = timeSpanRestriction;

            totalMoney = (float)secondSpan * moneySum / 10;
            print("Вас небыло в игре: " + secondSpan + "Вы заработали: " + totalMoney);
        }
    }
}