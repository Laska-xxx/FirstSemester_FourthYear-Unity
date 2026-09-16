using DG.Tweening;
using Environment.Asteroids;
using Player;
using Score;
using System;
using UnityEngine;
using Zenject;

namespace Environment.Boosters
{
    public class Booster : Flyer
    {
        [SerializeField] private float magnetRadius;
        [SerializeField] private float magnetSpeed;
        [SerializeField] private int scoreValue = 1;

        public event Action<Booster> OnDespawned;

        private ScoreManager _scoreManager;
        private IMemoryPool _pool;
        private Transform _playerTransform;
        private bool _isHoming;

        [Inject] public void Init(ScoreManager scoreManager)
        {
            _scoreManager = scoreManager;
        }

        public void Configure(Vector3 start, Vector3 end, float duration, Transform playerTransform, IMemoryPool pool)
        {
            _pool = pool;
            _playerTransform = playerTransform;
            _isHoming = false;
            transform.position = start;

            Tween tween = transform.DOMove(end, duration)
                .SetEase(Ease.Linear)
                .OnComplete(ReturnToPool);

            SetFlightTween(tween);
        }

        private void Update()
        {
            if (_isHoming || _playerTransform == null)
                return;

            float distance = Vector3.Distance(transform.position, _playerTransform.position);

            if (distance <= magnetRadius)
            {
                _isHoming = true;
                StopFlightTween();
            }
        }

        private void FixedUpdate()
        {
            if (!_isHoming)
                return;

            transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, magnetSpeed * Time.fixedDeltaTime);
        }

        protected override void ProcessPlayerContact(PlayerController player)
        {
            _scoreManager.AddScore(scoreValue);

            ReturnToPool();
        }

        public void ForceDespawn()
        {
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            StopFlightTween();
            OnDespawned?.Invoke(this);
            _isHoming = false;
            _pool.Despawn(this);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, magnetRadius);
        }

        public class Pool : MonoMemoryPool<Vector3, Vector3, float, Transform, Booster>
        {
            protected override void Reinitialize(Vector3 start, Vector3 end, float duration, Transform playerTransform, Booster item)
            {
                item.Configure(start, end, duration, playerTransform, this);
            }
        }
    }
}