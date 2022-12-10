using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private PoolObject _prefab;
    
    [Space(10)] [SerializeField] private Transform _container;
    [SerializeField] private int _minCapacity;
    [SerializeField] private int _maxCapacity;
    [Space(10)] [SerializeField] private bool _autoExpand;
    
    private List<PoolObject> _pool;
    
    private void OnValidate()
    {
        if (_autoExpand)
        {
            _maxCapacity = int.MaxValue;
        }
    }
    
    private void Awake()
    {
        CreatePool();
    }
    
    private void CreatePool()
    {
        _pool = new List<PoolObject>(_minCapacity);
    
        for (int i = 0; i < _minCapacity; i++)
        {
            CreateElement();
        }
    }
    
    private PoolObject CreateElement(bool isActiveByDefault = false)
    {
        var createdObject = Instantiate(_prefab, _container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        
        _pool.Add(createdObject);
    
        return createdObject;
    }
    
    public bool TryGetElement(out PoolObject element)
    {
        foreach (var item in _pool)
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
    
        if (_autoExpand)
        {
            return CreateElement(true);
        }
        
        if(_pool.Count < _maxCapacity)
        {
            return CreateElement(true);
        }
    
        throw new Exception("Pool is over!");
    }
}
