using IdleTycoon.GasStation;
using UnityEngine;
using Zenject;

namespace IdleTycoon.Zenject
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private CarMovable carMovablePrefab;
        [SerializeField] private Transform carsRoot;

        public override void InstallBindings()
        {
            Container.BindMemoryPool<CarMovable, CarMovablePool>()
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(carMovablePrefab)
                .UnderTransform(carsRoot);
        }
    }
}