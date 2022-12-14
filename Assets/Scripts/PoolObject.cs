using UnityEngine;

namespace IdleTycoon
{
    public class PoolObject : MonoBehaviour
    {
        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}