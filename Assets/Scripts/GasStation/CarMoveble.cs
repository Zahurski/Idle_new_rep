using System;
using System.Collections;
using Ads;
using Components;
using GasStation;
using GasStation.Config;
using Unity.VisualScripting;
using UnityEngine;

    public class CarMoveble : MonoBehaviour
    {
        private const string CarTag = "Car";
        private const string GasStationTag = "GasStation";
        
        [SerializeField] private Transform target;
        [SerializeField] private GasStationConfig config;
        
        private float _currentSpeed;
        private bool _stop;
        private bool _fuel;

        private PoolObject _poolObject;
        private FuelingLoading _fuelingLoading;
        private MoneyIncreaseText _moneyIncreaseText;
        private AdsController _ads;

        private void Awake()
        {
            _ads = FindObjectOfType<AdsController>();
            _poolObject = GetComponent<PoolObject>();
            _fuelingLoading = FindObjectOfType<FuelingLoading>();
            _moneyIncreaseText = FindObjectOfType<MoneyIncreaseText>();
            target = FindObjectOfType<TargetComponent>().transform;
        }

        private void Update()
        {
            Destroy();
            _currentSpeed = !_stop ? config.CarSpeed : 0f;

            transform.position = Vector3.MoveTowards(transform.position, target.position, _currentSpeed * Time.deltaTime);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (_fuel) return;
            if (other.gameObject.CompareTag(CarTag))
            {
                _stop = true;
            }
            else if (!other.gameObject.CompareTag(GasStationTag))
            {
                _stop = false;
            }
            else
            {
                _stop = true;
                StartCoroutine(Fueling());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_fuel) return;
            if (other.gameObject.CompareTag(CarTag))
            {
                _stop = false;
            }
        }

        private void Destroy()
        {
            if (gameObject.transform.position == target.position)
            {
                _fuel = false;
                _stop = false;
                _poolObject.ReturnToPool();
            }
        }

        private IEnumerator Fueling()
        {
            _fuelingLoading.IsActive = true;
            yield return new WaitForSeconds(config.FuelingTime);
            _fuelingLoading.IsActive = false;
            _fuel = true;
            _moneyIncreaseText.Fuel = true;
            _stop = false;
            GameManager.Instance.Money += config.Cost * _ads.AdvMultiplier;
        }
    }
