using IdleTycoon.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace IdleTycoon.OilPump
{
    public class OilPumpLoading : BaseLoadingCircle
    {
        [SerializeField] private Image loadingImage;
        [SerializeField] OilPumpConfig config;
        [SerializeField] private Canvas canvas;

        public bool IsActive { get; set; }

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