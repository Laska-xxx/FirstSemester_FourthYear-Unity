using UnityEngine;
using UnityEngine.Pool;

namespace Pooling
{
    public class Pool<T> where T : Component, IPoolable<T>
    {
        private readonly Factory<T> _factory;
        private readonly ObjectPool<T> _pool;

        public Pool(Factory<T> factory)
        {
            _factory = factory;
            _pool = new ObjectPool<T>(CreateItem, OnGet, OnRelease, OnDestroy, defaultCapacity: 4);
            
        }

        private T CreateItem()
        {
            T item = _factory.Create();
            item.SetPool(this);
            return item;
        }

        public T Get()
        {
            return _pool.Get();
        }

        public void Release(T item)
        {
            _pool.Release(item);
        }

        private void OnGet(T item)
        {
            item.gameObject.SetActive(true);
        }

        private void OnRelease(T item)
        {
            item.gameObject.SetActive(false);
        }

        private void OnDestroy(T item)
        {
            Object.Destroy(item.gameObject);
        }
    }
}