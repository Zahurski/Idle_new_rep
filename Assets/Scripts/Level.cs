using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace IdleTycoon
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Button levelUpButton;
        [SerializeField] private GameObject levelContainer;
        [SerializeField] private GameObject levelMenu;
        [SerializeField] private Image levelImage;

        private UIManager uiManager;

        private int level = 1;

        private Action activeUpMenu;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            levelImage.fillAmount = 0f;
            activeUpMenu += StartCoroutineShakeButton;
            StartCoroutine(IncreaseLevel());
        }

        private IEnumerator IncreaseLevel()
        {
            while (Math.Abs(levelImage.fillAmount - 1) > 0)
            {
                levelImage.fillAmount += 1f / (level * 5) * Time.deltaTime;
                yield return null;
            }

            StopCoroutine(IncreaseLevel());
            LevelUpMenu();
        }

        private void LevelUpMenu()
        {
            levelContainer.SetActive(false);
            levelUpButton.gameObject.SetActive(true);
            activeUpMenu?.Invoke();
        }

        private void StartCoroutineShakeButton()
        {
            StartCoroutine(ShakeButton());
            activeUpMenu -= StartCoroutineShakeButton;
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
            uiManager.ShowLevelMenu();
        }

        public void GetBonus()
        {
            StopCoroutine(ShakeButton());
            GameManager.Instance.Diamond += 10;

            if (level < 4)
            {
                level++;
            }

            levelUpButton.gameObject.SetActive(false);
            levelContainer.SetActive(true);
            Initialize();
            uiManager.Close();
        }
    }
}