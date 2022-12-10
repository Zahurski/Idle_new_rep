using UnityEngine;

namespace OilPump
{
    public class OilPumpClick : MonoBehaviour
    {
        private UIManager _uiManager;
        private CameraController _cameraController;

        private void Start()
        {
            _uiManager = FindObjectOfType<UIManager>();
            _cameraController = FindObjectOfType<CameraController>();
        }

        private void OnMouseUp()
        {
            if(_uiManager.CurrentScreen != _uiManager.GameScreen) return;
            if(_cameraController.Moveble) return;
            _uiManager.ShowOilPumpUpgradeMenu();
        }
    }
}