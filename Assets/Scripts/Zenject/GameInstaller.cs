using IdleTycoon.GasStation;
using IdleTycoon.Meta;
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
            Container.Bind<MetaValues>().AsSingle();
            Container.Bind<IMetaValues>().To<MetaValues>().FromResolve();

            Container.BindMemoryPool<CarMovable, CarMovablePool>()
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(carMovablePrefab)
                .UnderTransform(carsRoot);
        }
    }
}