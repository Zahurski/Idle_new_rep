using IdleTycoon.UI.Views;
using UnityEngine;
using Zenject;

namespace IdleTycoon.Zenject
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private RectTransform rootForSpawnComponents;
        [SerializeField] private HintMessageFly hintMessageFlyPrefab;

        public override void InstallBindings()
        {
            Container.BindMemoryPool<HintMessageFly, HintsMessagesPool>()
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(hintMessageFlyPrefab)
                .UnderTransform(rootForSpawnComponents);
        }

    }
}