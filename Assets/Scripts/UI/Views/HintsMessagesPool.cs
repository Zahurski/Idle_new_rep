using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using Zenject;

namespace IdleTycoon.UI.Views
{
    public class HintsMessagesPool : MonoMemoryPool<RectTransform, RectTransform, string, HintMessageFly>
    {
        protected override void OnCreated(HintMessageFly item)
        {
            base.OnCreated(item);
            item.ShowEnded += HintOnShowEnded;
        }

        protected override void Reinitialize(RectTransform startPoint, RectTransform endPoint, string message, HintMessageFly item)
        {
            item.Reinitialize(startPoint, endPoint, message);
        }

        private void HintOnShowEnded(HintMessageFly hintMessageFly)
        {
            this.Despawn(hintMessageFly);
        }
    }
}