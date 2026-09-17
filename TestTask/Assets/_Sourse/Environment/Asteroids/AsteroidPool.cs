using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace Environment.Asteroids
{
    public class AsteroidPool
    {
        private IObjectResolver _resolver;
        private Asteroid _prefab;
        private ObjectPool<Asteroid> _pool;

        public AsteroidPool(IObjectResolver resolver, Asteroid prefab)
        {
            _resolver = resolver;
            _prefab = prefab;
            _pool = new ObjectPool<Asteroid>(Create, OnGet, OnRelease, OnDestroy, defaultCapacity: 4);
        }

        private Asteroid Create()
        {
            Asteroid asteroid = _resolver.Instantiate(_prefab);
            asteroid.SetPool(this);
            return asteroid;
        }

        public Asteroid Spawn(Vector3 start, Vector3 end, float duration)
        {
            Asteroid asteroid = _pool.Get();
            asteroid.Configure(start, end, duration);
            return asteroid;
        }

        public void Despawn(Asteroid asteroid)
        {
            _pool.Release(asteroid);
        }

        private void OnGet(Asteroid asteroid)
        {
            asteroid.gameObject.SetActive(true);
        }

        private void OnRelease(Asteroid asteroid)
        {
            asteroid.gameObject.SetActive(false);
        }

        private void OnDestroy(Asteroid asteroid)
        {
            Object.Destroy(asteroid.gameObject);
        }
    }
}