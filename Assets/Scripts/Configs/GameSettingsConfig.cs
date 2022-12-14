using UnityEngine;
using Zenject;

namespace IdleTycoon.Configs
{
    [CreateAssetMenu(fileName = nameof(GameSettingsConfig), menuName = "Configs/" + nameof(GameSettingsConfig))]
    public class GameSettingsConfig : ScriptableObjectInstaller<GameSettingsConfig>
    {
        [SerializeField] private GasStationConfig gasStationConfig;
        [SerializeField] private OilPumpConfig oilPumpConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(gasStationConfig);
            Container.BindInstance(oilPumpConfig);
        }
    }
}