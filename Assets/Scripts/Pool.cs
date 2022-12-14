using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace IdleTycoon
{
    public class Pool : MonoBehaviour
    {
        //todo d.gankov: remove all FormerlySerializedAs
        [FormerlySerializedAs("_prefab")]
        [SerializeField] private PoolObject prefab;

        [FormerlySerializedAs("_container")]
        [Space(10)]
        [SerializeField] private Transform container;
        [FormerlySerializedAs("_minCapacity")]
        [SerializeField] private int minCapacity;
        [FormerlySerializedAs("_maxCapacity")]
        [SerializeField] private int maxCapacity;
        [FormerlySerializedAs("_autoExpand")]
        [Space(10)]
        [SerializeField] private bool autoExpand;

        private List<PoolObject> pool;

        private void OnValidate()
        {
            if (autoExpand)
            {
                maxCapacity = int.MaxValue;
            }
        }

        private void Awake()
        {
            CreatePool();
        }

        private void CreatePool()
        {
            pool = new List<PoolObject>(minCapacity);

            for (int i = 0; i < minCapacity; i++)
            {
                CreateElement();
            }
        }

        private PoolObject CreateElement(bool isActiveByDefault = false)
        {
            var createdObject = Instantiate(prefab, container);
            createdObject.gameObject.SetActive(isActiveByDefault);

            pool.Add(createdObject);

            return createdObject;
        }

        public bool TryGetElement(out PoolObject element)
        {
            foreach (var item in pool)
            {
                if (!item.gameObject.activeInHierarchy)
                {
                    element = item;
                    item.gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }

        public PoolObject GetFreeElement(Vector3 position)
        {
            var element = GetFreeElement();
            element.transform.position = position;
            return element;
        }

        public PoolObject GetFreeElement(Vector3 position, Quaternion rotation)
        {
            var element = GetFreeElement(position);
            element.transform.rotation = rotation;
            return element;
        }

        public PoolObject GetFreeElement()
        {
            if (TryGetElement(out var element))
            {
                return element;
            }

            if (autoExpand)
            {
                return CreateElement(true);
            }

            if (pool.Count < maxCapacity)
            {
                return CreateElement(true);
            }

            throw new Exception("Pool is over!");
        }
    }
}