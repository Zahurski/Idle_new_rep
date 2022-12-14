using System.Collections;
using IdleTycoon.Ads;
using IdleTycoon.Components;
using IdleTycoon.GasStation.Config;
using UnityEngine;

namespace IdleTycoon.GasStation
{
    public class CarMovable : MonoBehaviour
    {
        private const string CAR_TAG = "Car";
        private const string GAS_STATION_TAG = "GasStation";

        [SerializeField] private Transform target;
        [SerializeField] private GasStationConfig config;

        private float currentSpeed;
        private bool stop;
        private bool fuel;

        private PoolObject poolObject;
        private FuelingLoading fuelingLoading;
        private MoneyIncreaseText moneyIncreaseText;
        private AdsController ads;

        private void Awake()
        {
            ads = FindObjectOfType<AdsController>();
            poolObject = GetComponent<PoolObject>();
            fuelingLoading = FindObjectOfType<FuelingLoading>();
            moneyIncreaseText = FindObjectOfType<MoneyIncreaseText>();
            target = FindObjectOfType<TargetComponent>().transform;
        }

        private void Update()
        {
            Destroy();
            currentSpeed = !stop ? config.CarSpeed : 0f;

            transform.position = Vector3.MoveTowards(transform.position, target.position, currentSpeed * Time.deltaTime);
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

        private void Destroy()
        {
            if (gameObject.transform.position == target.position)
            {
                fuel = false;
                stop = false;
                poolObject.ReturnToPool();
            }
        }

        private IEnumerator Fueling()
        {
            fuelingLoading.IsActive = true;
            yield return new WaitForSeconds(config.FuelingTime);
            fuelingLoading.IsActive = false;
            fuel = true;
            moneyIncreaseText.Fuel = true;
            stop = false;
            GameManager.Instance.Money += config.Cost * ads.AdvMultiplier;
        }
    }
}