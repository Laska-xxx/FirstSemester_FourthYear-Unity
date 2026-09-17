using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Pooling
{
    public class Factory<T> where T: Component
    {
        private readonly IObjectResolver _resolver;
        private readonly T _prefab;

        public Factory(IObjectResolver resolver, T prefab)
        {
            _resolver = resolver;
            _prefab = prefab;
        }

        public T Create()
        {
            return _resolver.Instantiate(_prefab);
        }
    }
}