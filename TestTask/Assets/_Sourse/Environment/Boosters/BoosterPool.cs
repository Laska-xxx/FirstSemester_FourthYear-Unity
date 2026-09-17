using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace Environment.Boosters
{
    public class BoosterPool
    {
        private IObjectResolver _resolver;
        private Booster _prefab;
        private ObjectPool<Booster> _pool;

        public BoosterPool(IObjectResolver resolver, Booster prefab)
        {
            _resolver = resolver;
            _prefab = prefab;
            _pool = new ObjectPool<Booster>(Create, OnGet, OnRelease, OnDestroy, defaultCapacity: 4);
        }

        private Booster Create()
        {
            Booster booster = _resolver.Instantiate(_prefab);
            booster.SetPool(this);
            return booster;
        }

        public Booster Spawn(Vector3 start, Vector3 end, float duration, Transform playerTransform)
        {
            Booster booster = _pool.Get();
            booster.Configure(start, end, duration, playerTransform);
            return booster;
        }

        public void Despawn(Booster booster)
        {
            _pool.Release(booster);
        }

        private void OnGet(Booster booster)
        {
            booster.gameObject.SetActive(true);
        }

        private void OnRelease(Booster booster)
        {
            booster.gameObject.SetActive(false);
        }

        private void OnDestroy(Booster booster)
        {
            Object.Destroy(booster.gameObject);
        }
    }
}