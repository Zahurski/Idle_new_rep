using IdleTycoon.Configs;
using System.Collections;
using UnityEngine;
using Zenject;

namespace IdleTycoon.GasStation
{
    public class GasStationSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject carPrefab;
        [SerializeField] private Transform spawn;

        private bool isAvailable = true;
        private bool spawnDelay = true;

        private GasStationConfig config;
        private CarMovablePool carMovablePool;

        [Inject]
        private void Init(GasStationConfig config, CarMovablePool carMovablePool)
        {
            this.carMovablePool = carMovablePool;
            this.config = config;
        }

        private void Start()
        {
            StartCoroutine(SpawnDelay());
        }

        private void Update()
        {
            if (!isAvailable) return;
            if (!spawnDelay) return;

            CarMovable carMovable = carMovablePool.Spawn(spawn.position, Quaternion.identity);
            carMovable.MovedEnd += CarMovableOnMovedEnd;

            isAvailable = false;
            spawnDelay = false;
            StartCoroutine(SpawnDelay());
        }

        private void CarMovableOnMovedEnd(CarMovable carMovable)
        {
            carMovable.MovedEnd -= CarMovableOnMovedEnd;
            carMovablePool.Despawn(carMovable);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Car"))
            {
                isAvailable = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Car"))
            {
                isAvailable = true;
            }
        }

        private IEnumerator SpawnDelay()
        {
            yield return new WaitForSeconds(config.SpawnDelay);
            spawnDelay = true;
        }
    }
}