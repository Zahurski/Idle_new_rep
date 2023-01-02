using IdleTycoon.Configs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace IdleTycoon.GasStation
{
    public class FuelingLoading : BaseLoadingCircle
    {
        [SerializeField] private Image loadingImage;
        [SerializeField] private Canvas canvas;

        private bool deactivateTimer;

        private GasStationConfig config;

        public bool IsActive { get; set; }

        [Inject]
        private void Init(GasStationConfig config)
        {
            this.config = config;
        }

        private void Awake()
        {
            loadingImage.fillAmount = 0;
        }

        private void Update()
        {
            GetCircle(canvas, loadingImage, IsActive, config.FuelingTime);
        }
    }
}