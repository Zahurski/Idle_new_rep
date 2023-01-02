using Cysharp.Threading.Tasks;
using System;
using System.Globalization;
using IdleTycoon.Ads;
using IdleTycoon.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace IdleTycoon
{
    public class FirstLoad : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMoneyOffline;
        [SerializeField] private Button button;

        private UIManager uiManager;

        private int decreaseDiamond = 20;
        private float moneySum;
        private float totalMoney;
        private bool active = false;

        private GasStationConfig gasStation;
        private OilPumpConfig oilPump;
        private IInterstitialAd interstitialAd;
        private IRewardedAd rewardedAd;

        [Inject]
        private void Init(GasStationConfig gasStation, OilPumpConfig oilPump)
        {
            this.oilPump = oilPump;
            this.gasStation = gasStation;
        }

        [Inject]
        private void Init(IRewardedAd rewardedAd, IInterstitialAd interstitialAd)
        {
            this.rewardedAd = rewardedAd;
            this.interstitialAd = interstitialAd;
        }

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        private void Start()
        {
            RefreshDiamondButton();
            moneySum = gasStation.Cost + oilPump.Cost;
            CalculateOfflineIncome();
            textMoneyOffline.text = "Пока вас не было\nвы заработали: " + FormatNums.FormatNum(totalMoney);
        }

        private void RefreshDiamondButton()
        {
            button.interactable = GameManager.Instance.Diamond > decreaseDiamond;
        }

        public void Getx3()
        {
            GameManager.Instance.Diamond -= decreaseDiamond;
            GameManager.Instance.Money += (float) Math.Round(totalMoney * 3, 0);
            uiManager.Close();
            //todo d.gankov: check the
            interstitialAd.Show();
        }

        public void Getx2()
        {
            active = true;
            //todo d.gankov: check the
            rewardedAd.ShowAsync().Forget();
        }

        public void Getx1()
        {
            GameManager.Instance.Money += (float) Math.Round(totalMoney, 0);
            print("Добавлено: " + (float) Math.Round(totalMoney, 0));
            uiManager.Close();
        }

        private void GetBounty()
        {
            if (!active) return;
            GameManager.Instance.Money += (float) Math.Round(totalMoney * 2, 0);
            print("Добавлено: " + (float) Math.Round(totalMoney, 0));
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

            totalMoney = (float) secondSpan * moneySum / 10;
            print("Вас небыло в игре: " + secondSpan + "Вы заработали: " + totalMoney);
        }
    }
}