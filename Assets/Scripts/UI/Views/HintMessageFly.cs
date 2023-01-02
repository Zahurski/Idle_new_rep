using AnimeTask;
using Cysharp.Threading.Tasks;
using IdleTycoon.Utils;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using Easing = AnimeTask.Easing;

namespace IdleTycoon.UI.Views
{
    public class HintMessageFly : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private float startScale;
        [SerializeField] private float speedFly;
        [SerializeField] private float delayShowAfterMove;
        [SerializeField] private float durationHide;
        [SerializeField] private CanvasGroup canvasGroup;

        private CancellationTokenSource animationCts;
        private RectTransform startPoint;
        private RectTransform endPoint;

        public event Action<HintMessageFly> ShowEnded;

        public void Reinitialize(RectTransform startPoint, RectTransform endPoint, string message)
        {
            this.endPoint = endPoint;
            this.startPoint = startPoint;

            label.text = message;
        }

        public void Show()
        {
            AsyncUtils.CancelAndCreateCts(ref animationCts, this.GetCancellationTokenOnDestroy());
            RunAnimationAsync(animationCts.Token).Forget();
        }

        private async UniTask RunAnimationAsync(CancellationToken cancellationToken)
        {
            var distance = Vector2.Distance(startPoint.anchoredPosition, endPoint.anchoredPosition);

            float durationFly = (distance / 100f) / speedFly;
            await UniTask.WhenAll(
                Easing.Create<Linear>(startPoint.anchoredPosition, endPoint.anchoredPosition, durationFly)
                    .ToAnchoredPosition((RectTransform) this.transform, cancellationToken),
                Easing.Create<Linear>(Vector3.one * startScale, Vector3.one, durationFly)
                    .ToLocalScale(this, cancellationToken: cancellationToken),
                Easing.Create<Linear>(0, 1, durationFly)
                    .ToBind(() => canvasGroup.alpha, value => canvasGroup.alpha = value, cancellationToken: cancellationToken));

            await UniTask.Delay(TimeSpan.FromSeconds(delayShowAfterMove), cancellationToken: cancellationToken);

            await Easing.Create<Linear>(1, 0, durationHide)
                .ToBind(() => canvasGroup.alpha, value => canvasGroup.alpha = value, cancellationToken: cancellationToken);
            ShowEnded?.Invoke(this);
        }
    }
}