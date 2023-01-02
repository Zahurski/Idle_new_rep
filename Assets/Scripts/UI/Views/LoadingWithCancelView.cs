using Cysharp.Threading.Tasks;
using IdleTycoon.Utils;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace IdleTycoon.UI.Views
{
    public class LoadingWithCancelView : BaseView
    {
        [SerializeField] private Button cancelButton;

        private bool isCanceled = false;
        private CancellationTokenSource waitHideCts;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            cancelButton.onClick.AddListener(ClickOnCancelButton);
        }

        private void OnDisable()
        {
            cancelButton.onClick.RemoveListener(ClickOnCancelButton);
        }

        public void Hide()
        {
            isCanceled = false;
            waitHideCts.Cancel();
            this.gameObject.SetActive(false);
        }

        public void Show()
        {
            isCanceled = false;
            AsyncUtils.CancelAndCreateCts(ref waitHideCts, this.GetCancellationTokenOnDestroy());
            this.gameObject.SetActive(false);
        }

        public async UniTask<bool> WaitCancelAsync()
        {
            await UniTask.WaitUntilCanceled(waitHideCts.Token);
            return isCanceled;
        }

        private void ClickOnCancelButton()
        {
            isCanceled = true;
            waitHideCts.Cancel();
        }
    }
}