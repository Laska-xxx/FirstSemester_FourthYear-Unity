using DG.Tweening;
using Player;
using Pooling;
using System;
using UnityEngine;

namespace Environment.Asteroids
{
    public partial class Asteroid : Flyer, IPoolable<Asteroid>
    {
        [SerializeField] private float arcHeight = 2f;

        public event Action<Asteroid> OnDespawned;

        private Pool<Asteroid> _pool;

        public void SetPool(Pool<Asteroid> pool)
        {
            _pool = pool;
        }

        public void Configure(Vector3 start, Vector3 end, float duration)
        {
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

        public void ForceDespawn()
        {
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            StopFlightTween();
            OnDespawned?.Invoke(this);
            _pool.Release(this);
        }
    }
}