using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace IdleTycoon
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameScreen;
        [SerializeField] private GameObject gasStationUpgradeMenu;
        [SerializeField] private GameObject oilPumpUpgradeMenu;
        [FormerlySerializedAs("firtsEntry")] 
        [SerializeField] private GameObject firstEntry;
        [SerializeField] private GameObject levelMenu;
        [SerializeField] private GameObject settings;

        private GameObject currentScreen;

        public event Action CloseMenu;

        public GameObject CurrentScreen => currentScreen;
        public GameObject GameScreen => gameScreen;

        private void Awake()
        {
            currentScreen = gameScreen;
        }

        public void Initialize()
        {
            ShowFirstEntryMenu();
        }

        public void ShowGameScreen()
        {
            currentScreen.SetActive(false);
            gameScreen.SetActive(true);
            currentScreen = gameScreen;
        }

        public void ShowFirstEntryMenu()
        {
            currentScreen.SetActive(false);
            firstEntry.SetActive(true);
            currentScreen = firstEntry;
        }

        public void ShowSettingsMenu()
        {
            currentScreen.SetActive(false);
            settings.SetActive(true);
            currentScreen = settings;
        }

        public void ShowGasStationUpgradeMenu()
        {
            currentScreen.SetActive(false);
            gasStationUpgradeMenu.SetActive(true);
            currentScreen = gasStationUpgradeMenu;
        }

        public void ShowOilPumpUpgradeMenu()
        {
            currentScreen.SetActive(false);
            oilPumpUpgradeMenu.SetActive(true);
            currentScreen = oilPumpUpgradeMenu;
        }

        public void ShowLevelMenu()
        {
            currentScreen.SetActive(false);
            levelMenu.SetActive(true);
            currentScreen = levelMenu;
        }

        public void Close()
        {
            CloseMenu?.Invoke();
            currentScreen.SetActive(false);
            currentScreen = gameScreen;
            currentScreen.SetActive(true);
        }
    }
}