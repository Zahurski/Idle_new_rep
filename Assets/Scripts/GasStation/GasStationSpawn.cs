using IdleTycoon.Configs;
using System.Collections;
using UnityEngine;

namespace IdleTycoon.GasStation
{
    public class GasStationSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject carPrefab;
        [SerializeField] private Transform spawn;
        [SerializeField] private GasStationConfig config;

        private bool isAvailable = true;
        private bool spawnDelay = true;

        private Pool pool;

        private void Start()
        {
            pool = FindObjectOfType<Pool>();
            StartCoroutine(SpawnDelay());
        }

        private void Update()
        {
            if (!isAvailable) return;
            if (!spawnDelay) return;
            pool.GetFreeElement(spawn.position, Quaternion.identity);
            isAvailable = false;
            spawnDelay = false;
            StartCoroutine(SpawnDelay());
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