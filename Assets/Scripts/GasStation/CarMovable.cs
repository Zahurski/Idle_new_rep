using System.Collections;
using IdleTycoon.Ads;
using IdleTycoon.Components;
using IdleTycoon.Configs;
using System;
using UnityEngine;
using Zenject;

namespace IdleTycoon.GasStation
{
    public class CarMovable : MonoBehaviour
    {
        private const string CAR_TAG = "Car";
        private const string GAS_STATION_TAG = "GasStation";

        [SerializeField] private Transform target;

        private float currentSpeed;
        private bool stop;
        private bool fuel;

        private FuelingLoading fuelingLoading;
        private MoneyIncreaseText moneyIncreaseText;
        private AdsController ads;
        private GasStationConfig config;

        public event Action<CarMovable> MovedEnd;

        [Inject]
        private void Init(GasStationConfig config)
        {
            this.config = config;
        }

        private void Awake()
        {
            ads = FindObjectOfType<AdsController>();
            fuelingLoading = FindObjectOfType<FuelingLoading>();
            moneyIncreaseText = FindObjectOfType<MoneyIncreaseText>();
            target = FindObjectOfType<TargetComponent>().transform;
        }

        private void Update()
        {
            currentSpeed = !stop ? config.CarSpeed : 0f;

            transform.position = Vector3.MoveTowards(transform.position, target.position, currentSpeed * Time.deltaTime);

            if (IsEndMove())
            {
                MovedEnd?.Invoke(this);
            }

            bool IsEndMove()
            {
                return Vector3.Distance(transform.position, target.position) <= 0.1f;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (fuel) return;
            if (other.gameObject.CompareTag(CAR_TAG))
            {
                stop = true;
            }
            else if (!other.gameObject.CompareTag(GAS_STATION_TAG))
            {
                stop = false;
            }
            else
            {
                stop = true;
                StartCoroutine(Fueling());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (fuel) return;
            if (other.gameObject.CompareTag(CAR_TAG))
            {
                stop = false;
            }
        }

        public void Reset(Vector3 position, Quaternion rotation)
        {
            fuel = false;
            stop = false;
            transform.SetPositionAndRotation(position, rotation);
        }

        private IEnumerator Fueling()
        {
            fuelingLoading.IsActive = true;
            yield return new WaitForSeconds(config.FuelingTime);
            fuelingLoading.IsActive = false;
            fuel = true;
            moneyIncreaseText.Fuel = true;
            stop = false;
            GameManager.Instance.Money += config.Cost * ads.AdditionalMultiplier;
        }
    }
}