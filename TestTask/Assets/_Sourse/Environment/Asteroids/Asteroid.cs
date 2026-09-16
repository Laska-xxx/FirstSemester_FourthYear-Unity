using DG.Tweening;
using Player;
using UnityEngine;
using Zenject;

namespace Environment.Asteroids
{
    public class Asteroid : Flyer
    {
        [SerializeField] private float arcHeight = 2f;

        private IMemoryPool _pool;

        public void Configure(Vector3 start, Vector3 end, float duration, IMemoryPool pool)
        {
            _pool = pool;

            transform.position = start;

            Tween tween = transform.DOJump(end, arcHeight, 1, duration)
                .SetEase(Ease.Linear)
                .OnComplete(ReturnToPool);

            SetFlightTween(tween);
        }

        protected override void ProcessPlayerContact(PlayerController player)
        {
            player.Die();
        }

        private void ReturnToPool()
        {
            StopFlightTween();
            _pool.Despawn(this);
        }

        public class Pool : MonoMemoryPool<Vector3, Vector3, float, Asteroid>
        {
            protected override void Reinitialize(Vector3 start, Vector3 end, float duration, Asteroid item)
            {
                item.Configure(start, end, duration, this);
            }
        }
    }
}