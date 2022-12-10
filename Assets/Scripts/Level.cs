using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DefaultNamespace
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Button levelUpButton;
        [SerializeField] private GameObject levelContainer;
        [SerializeField] private GameObject levelMenu;
        [SerializeField] private Image levelImage;
        
        private UIManager _uiManager;

        private int _level = 1;

        private Action _activeUpMenu;

        private void Awake()
        {
            _uiManager = FindObjectOfType<UIManager>();
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            levelImage.fillAmount = 0f;
            _activeUpMenu += StartCoroutineShakeButton;
            StartCoroutine(IncreaseLevel());
        }

        private IEnumerator IncreaseLevel()
        {
            while(Math.Abs(levelImage.fillAmount - 1) > 0)
            {
                levelImage.fillAmount += 1f / (_level * 5) * Time.deltaTime;
                yield return null;
            }
            
            StopCoroutine(IncreaseLevel());
            LevelUpMenu();
        }

        private void LevelUpMenu()
        {
            levelContainer.SetActive(false);
            levelUpButton.gameObject.SetActive(true);
            _activeUpMenu?.Invoke();
        }

        private void StartCoroutineShakeButton()
        {
            StartCoroutine(ShakeButton());
            _activeUpMenu -= StartCoroutineShakeButton;
        }

        private IEnumerator ShakeButton()
        {
            while (true)
            {
                levelUpButton.gameObject.transform.DOScale(1.1f, 1);
                yield return new WaitForSeconds(1f);
                levelUpButton.gameObject.transform.DOScale(1f, 1);
                yield return new WaitForSeconds(1f);
            }
        }

        public void OnClick()
        {
            _uiManager.ShowLevelMenu();
        }

        public void GetBonus()
        {
            StopCoroutine(ShakeButton());
            GameManager.Instance.Diamond += 10;
            
            if (_level < 4)
            {
                _level++;
            }
            
            levelUpButton.gameObject.SetActive(false);
            levelContainer.SetActive(true);
            Initialize();
            _uiManager.Close();
        }
    }
}