using System;
using System.Collections;
using GasStation.Config;
using UnityEngine;

public class GasStationSpawn : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private Transform spawn;
    [SerializeField] private GasStationConfig config;
    private bool _isAvailable = true;
    private bool _spawnDelay = true;

    private Pool _pool;

    private void Start()
    {
        _pool = FindObjectOfType<Pool>();
        StartCoroutine(SpawnDelay());
    }

    private void Update()
    {
        if (!_isAvailable) return;
        if (!_spawnDelay) return;
        _pool.GetFreeElement(spawn.position, Quaternion.identity);
        _isAvailable = false;
        _spawnDelay = false;
        StartCoroutine(SpawnDelay());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            _isAvailable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            _isAvailable = true;
        }
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(config.SpawnDelay);
        _spawnDelay = true;
    }
}