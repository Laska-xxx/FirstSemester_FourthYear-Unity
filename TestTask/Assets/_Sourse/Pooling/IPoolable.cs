using UnityEngine;

namespace Pooling
{
    public interface IPoolable<T> where T : Component, IPoolable<T>
    {
        void SetPool(Pool<T> pool);
    }
}