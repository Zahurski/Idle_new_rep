using GasStation.Config;
using UnityEngine;
using UnityEngine.UI;

namespace GasStation
{
    public class FuelingLoading : BaseLoadingCircle
    {
        [SerializeField] private Image loadingImage;
        [SerializeField] GasStationConfig config;
        [SerializeField] private Canvas canvas;
        private bool _deactivateTimer;

        public bool IsActive { get; set; }

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
