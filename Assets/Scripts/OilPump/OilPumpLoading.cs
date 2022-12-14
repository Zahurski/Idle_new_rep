using IdleTycoon.Configs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace IdleTycoon.OilPump
{
    public class OilPumpLoading : BaseLoadingCircle
    {
        [SerializeField] private Image loadingImage;
        [SerializeField] private Canvas canvas;

        private OilPumpConfig config;

        public bool IsActive { get; set; }

        [Inject]
        private void Init(OilPumpConfig config)
        {
            this.config = config;
        }

        private void Awake()
        {
            loadingImage.fillAmount = 0;
        }

        private void Update()
        {
            GetCircle(canvas, loadingImage, IsActive, config.PumpingTime);
        }
    }
}