using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject gasStationUpgradeMenu;
    [SerializeField] private GameObject oilPumpUpgradeMenu;
    [SerializeField] private GameObject firtsEntry;
    [SerializeField] private GameObject levelMenu;
    [SerializeField] private GameObject settings;
    
    private GameObject _currentScreen;
    
    public event Action CloseMenu;

    public GameObject CurrentScreen => _currentScreen;
    public GameObject GameScreen => gameScreen;

    private void Awake()
    {
        _currentScreen = gameScreen;
    }

    public void Initialize()
    {
        ShowFirstEntryMenu();
    }

    public void ShowGameScreen()
    {
        _currentScreen.SetActive(false);
        gameScreen.SetActive(true);
        _currentScreen = gameScreen;
    }
    
    public void ShowFirstEntryMenu()
    {
        _currentScreen.SetActive(false);
        firtsEntry.SetActive(true);
        _currentScreen = firtsEntry;
    }
    
    public void ShowSettingsMenu()
    {
        _currentScreen.SetActive(false);
        settings.SetActive(true);
        _currentScreen = settings;
    }

    public void ShowGasStationUpgradeMenu()
    {
        _currentScreen.SetActive(false);
        gasStationUpgradeMenu.SetActive(true);
        _currentScreen = gasStationUpgradeMenu;
    }

    public void ShowOilPumpUpgradeMenu()
    {
        _currentScreen.SetActive(false);
        oilPumpUpgradeMenu.SetActive(true);
        _currentScreen = oilPumpUpgradeMenu;
    }

    public void ShowLevelMenu()
    {
        _currentScreen.SetActive(false);
        levelMenu.SetActive(true);
        _currentScreen = levelMenu;
    }

    public void Close()
    {
        CloseMenu?.Invoke();
        _currentScreen.SetActive(false);
        _currentScreen = gameScreen;
        _currentScreen.SetActive(true);
    }
}