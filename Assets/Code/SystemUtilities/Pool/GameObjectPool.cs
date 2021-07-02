using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SystemUtilities.Pool
{
    public class GameObjectPool
    {
        private readonly GameObject _prefab;
        private readonly int _initialInstances;
        private readonly HashSet<GameObject> _instancesInUse;
        private readonly Queue<GameObject> _recycledInstances;

        public GameObjectPool(GameObject prefab, int initialInstances)
        {
            _prefab = prefab;
            _initialInstances = initialInstances;

            _instancesInUse = new HashSet<GameObject>();
            _recycledInstances = new Queue<GameObject>(initialInstances);

            InstanceInitialInstances();
        }

        private void InstanceInitialInstances()
        {
            for (var i = 0; i < _initialInstances; ++i)
            {
                var instance = Object.Instantiate(_prefab);
                instance.SetActive(false);
                _recycledInstances.Enqueue(instance);
            }
        }

        public void Recycle(GameObject objectToRecycle)
        {
            if (!_instancesInUse.Remove(objectToRecycle))
            {
                throw new Exception("The recycled object do not belongs to this pool");
            }

            objectToRecycle.SetActive(false);
            _recycledInstances.Enqueue(objectToRecycle);
        }
        
        public void RecycleAll()
        {
            foreach (var instance in _instancesInUse)
            {
                instance.SetActive(false);
                _recycledInstances.Enqueue(instance);
            }
            _instancesInUse.Clear();
        }

        public GameObject Get()
        {
            var instance = GetInstance();
            instance.SetActive(true);
            return instance;
        }

        private GameObject GetInstance()
        {
            var containsRecycledObject = _recycledInstances.Count > 0;
            if (containsRecycledObject)
            {
                var recycledInstance = _recycledInstances.Dequeue();
                _instancesInUse.Add(recycledInstance);
                return recycledInstance;
            }

            Debug.LogWarning("Not enough recycled instances, consider increase the initial instances");
            var instance = Object.Instantiate(_prefab);
            _instancesInUse.Add(instance);
            return instance;
        }

       
    }
}
