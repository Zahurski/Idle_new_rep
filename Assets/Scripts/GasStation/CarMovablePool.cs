using UnityEngine;
using Zenject;

namespace IdleTycoon.GasStation
{
    public class CarMovablePool : MonoMemoryPool<Vector3, Quaternion, CarMovable>
    {
        protected override void OnCreated(CarMovable item)
        {
            base.OnCreated(item);
            item.MovedEnd += CarMovableOnMovedEnd;
        }

        protected override void Reinitialize(Vector3 position, Quaternion rotation, CarMovable item)
        {
            item.Reset(position, rotation);
        }

        private void CarMovableOnMovedEnd(CarMovable carMovable)
        {
            this.Despawn(carMovable);
        }
    }
}