using UnityEngine;

namespace IdleTycoon.GasStation
{
    public class GasStationClick : MonoBehaviour
    {
        private UIManager uiManager;
        private CameraController cameraController;

        private void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
            cameraController = FindObjectOfType<CameraController>();
        }

        private void OnMouseUp()
        {
            if (uiManager.CurrentScreen != uiManager.GameScreen) return;
            if (cameraController.Movable) return;
            uiManager.ShowGasStationUpgradeMenu();
        }
    }
}