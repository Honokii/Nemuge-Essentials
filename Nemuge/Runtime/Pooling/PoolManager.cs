using System.Collections.Generic;
using Nemuge.Core;
using UnityEngine;

namespace Nemuge.Pooling {
    public class PoolManager : Singleton<PoolManager> {

        private readonly Dictionary<string, Queue<Poolable>> _availablePools = new Dictionary<string, Queue<Poolable>>();
        private readonly Dictionary<string, Queue<Poolable>> _activePools = new Dictionary<string, Queue<Poolable>>();
        
        [SerializeField] private bool shouldInitializeOnStart = true;
        
        [SerializeField] private List<Pool> pools = new List<Pool>();

        #region Unity Methods

        private void Start() {
            if (shouldInitializeOnStart) 
                InitializePools();
        }

        #endregion

        public void InitializePools() {
            foreach (var pool in pools) {
                RegisterPool(pool);
            }
        }
        
        public void RegisterPool(Pool pool) {
            var poolTag = pool.poolTag;
            var poolObject = pool.poolable;
            var queue = new Queue<Poolable>();
                
            for (var i = 0; i < pool.initialCount; i++) {
                var pooledObject = Instantiate(poolObject, this.transform);
                pooledObject.gameObject.SetActive(false);
                pooledObject.poolTag = poolTag;
                queue.Enqueue(pooledObject);
            }
                
            _availablePools.Add(poolTag.value, queue);
        }

        public Poolable GetPoolable(string poolTag) {
            Poolable result = null;
            if (_availablePools.ContainsKey(poolTag)) {
                var availableQueue = _availablePools[poolTag];
                if (availableQueue.Count > 0)
                    result = availableQueue.Dequeue();
            }

            if (result == null) {
                var poolInfo = pools.Find(x => string.Equals(x.poolTag.value, poolTag));
                if (poolInfo != null) {
                    if (poolInfo.expandable) {
                        result = Instantiate(poolInfo.poolable, transform);
                    }
                    else {
                        if (_activePools.ContainsKey(poolTag) && _activePools[poolTag].Count > 0)
                            result = _activePools[poolTag].Dequeue();
                    }
                }
            }

            if (result == null)
                return null;

            var activeQueue = _activePools.ContainsKey(poolTag) ? _activePools[poolTag] : new Queue<Poolable>();
            activeQueue.Enqueue(result);
            _activePools[poolTag] = activeQueue;

            result.gameObject.SetActive(true);
            return result;
        }

        public Poolable GetPoolable(string poolTag, Transform parent) {
            var poolable = GetPoolable(poolTag);
            if (poolable == null)
                return null;

            poolable.transform.parent = parent;
            return poolable;
        }

        public T GetPoolable<T>(string poolTag, Transform parent) {
            var poolable = GetPoolable(poolTag);
            poolable.transform.parent = parent;
            var result = poolable.GetComponent<T>();
            return result;
        }

        public void DisablePoolable(Poolable poolable) {
            if (!_availablePools.ContainsKey(poolable.poolTag.value)) {
                Destroy(poolable.gameObject);
                return;
            }

            var queue = _availablePools[poolable.poolTag.value];
            queue.Enqueue(poolable);
            poolable.gameObject.SetActive(false);
        }
    }
}