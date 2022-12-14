using System;
using System.Globalization;
using UnityEngine;

namespace IdleTycoon
{
    public class GameManager : MonoBehaviour
    {
        private const string LAST_PLAYED_TIME = "LastPlayedTime";
        private const string MONEY = "Money";
        private const string DIAMOND = "Diamond";

        public static string LastPlayedTime => LAST_PLAYED_TIME;
        public static GameManager Instance { get; set; }

        private UIManager uiManager;
        //private FirebaseSave _firebaseSave;

        private float money = 0;
        private int diamond = 0;

        public Action<float> OnMoneyValueChange { get; set; } = null;
        public Action<float> OnDiamondValueChange { get; set; } = null;
        public Action IncreaseMoney { get; set; }
        public Action LoadData { get; set; }


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            uiManager = FindObjectOfType<UIManager>();
            //_firebaseSave = FindObjectOfType<FirebaseSave>();
        }

        private void Start()
        {
            Load();
            //LoadDataTest();
        }

        // private async void LoadDataTest()
        // {
        //     await _firebaseSave.LoadData();
        // }

        public float Money
        {
            get => money;

            set
            {
                if (value >= 0)
                {
                    var currentMoney = value;
                    if (currentMoney - money >= 0)
                    {
                        IncreaseMoney?.Invoke();
                    }

                    money = value;
                    money = (float)Math.Round(money, 0);
                    OnMoneyValueChange?.Invoke(money);
                }
            }
        }

        public int Diamond
        {
            get => diamond;

            set
            {
                if (value >= 0)
                {
                    diamond = value;
                    OnDiamondValueChange?.Invoke(diamond);
                }
            }
        }

        private void OnApplicationQuit()
        {
            //_firebaseSave.SaveData();
            PlayerPrefs.SetFloat(MONEY, money);
            PlayerPrefs.SetInt(DIAMOND, diamond);
            PlayerPrefs.SetString(LAST_PLAYED_TIME, DateTime.UtcNow.ToString(CultureInfo.CurrentCulture));
        }

        private void Load()
        {
            if (!PlayerPrefs.HasKey(MONEY))
            {
                PlayerPrefs.SetFloat(MONEY, 0);
                uiManager.ShowGameScreen();
            }
            else
            {
                uiManager.Initialize();
                //_money = PlayerPrefs.GetFloat(MONEY);
            }

            if (!PlayerPrefs.HasKey(DIAMOND))
            {
                PlayerPrefs.SetInt(DIAMOND, 0);
            }
            else
            {
                diamond = PlayerPrefs.GetInt(DIAMOND);
            }

            LoadData?.Invoke();
        }
    }
}