using IdleTycoon.UI.Views;
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

        private HintsMessagesPool hintsMessagesPool;
        private Game game;

        [Inject]
        private void Init(Game game, HintsMessagesPool hintsMessagesPool)
        {
            this.game = game;
            this.hintsMessagesPool = hintsMessagesPool;

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

        private async void AdButtonUpSoftMoneyCoefficientOnClick()
        {
            bool result = await game.TryUpSoftMoneyCoefficientAsync();

            if (!result)
            {
                var hintMessage = hintsMessagesPool.Spawn(hintStartPosition, hintEndPosition, adNotInitializedMessage);
                hintMessage.Show();
            }
        }
    }
}