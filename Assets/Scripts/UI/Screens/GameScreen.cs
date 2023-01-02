using Cysharp.Threading.Tasks;
using IdleTycoon.Ads;
using IdleTycoon.UI.Views;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace IdleTycoon.UI.Screens
{
    public class GameScreen : BaseScreen
    {
        [SerializeField] private Button adButtonUpSoftMoneyCoefficient;
        [SerializeField] private GameObject loadingAdRoot;
        [SerializeField] private float delayShowLoadingAdRoot;
        [Header("Ad not initialize")]
        [SerializeField] private RectTransform hintStartPosition;
        [SerializeField] private RectTransform hintEndPosition;
        [SerializeField] private string adNotInitializedMessage;

        private IRewardedAd rewardedAd;
        private IAdsInitializer adsInitializer;
        private CancellationTokenSource adsInitializeWaitingCts;
        private HintsMessagesPool hintsMessagesPool;

        [Inject]
        private void Init(IRewardedAd rewardedAd, IAdsInitializer adsInitializer, HintsMessagesPool hintsMessagesPool)
        {
            this.hintsMessagesPool = hintsMessagesPool;
            this.adsInitializer = adsInitializer;
            this.rewardedAd = rewardedAd;

            loadingAdRoot.SetActive(false);
        }

        private void OnEnable()
        {
            adButtonUpSoftMoneyCoefficient.onClick.AddListener(AdButtonUpSoftMoneyCoefficientOnClick);
        }

        private void OnDisable()
        {
            adButtonUpSoftMoneyCoefficient.onClick.RemoveListener(AdButtonUpSoftMoneyCoefficientOnClick);
        }

        private void AdButtonUpSoftMoneyCoefficientOnClick()
        {
            if (adsInitializer.IsInitialized)
            {
                ShowRewardAdAsync().Forget();
            }
            else
            {
                var hintMessage = hintsMessagesPool.Spawn(hintStartPosition, hintEndPosition, adNotInitializedMessage);
                hintMessage.Show();
            }
        }

        private async UniTask ShowRewardAdAsync()
        {
            AsyncLazy<AdShowResultEnum> workTask = rewardedAd.ShowAsync().ToAsyncLazy();

            UniTask delayTask = UniTask.Delay(TimeSpan.FromSeconds(delayShowLoadingAdRoot));

            (bool hasResultLeft, AdShowResultEnum result) resultTask = await UniTask.WhenAny(workTask.Task, delayTask);

            if (workTask.Task.Status == UniTaskStatus.Succeeded || workTask.Task.Status == UniTaskStatus.Canceled)
            {
                return;
            }

            loadingAdRoot.SetActive(true);
            var result = await workTask;

            loadingAdRoot.SetActive(false);
        }
    }
}