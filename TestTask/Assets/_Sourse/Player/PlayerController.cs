using Core;
using System;
using UnityEngine;
using VContainer;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float riseAcceleration = 20f;
        [SerializeField] private float fallAcceleration = 15f;
        [SerializeField] private float maxRiseSpeed = 6f;
        [SerializeField] private float maxFallSpeed = 8f;
        [SerializeField] private ParticleSystem trail;

        public event Action OnPlayerDeath;

        private InputListener _input;
        private Rigidbody2D _rb;
        private Vector3 _startPosition;
        private bool _isRising;

        [Inject] private void Init(InputListener input)
        {
            _input = input;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _startPosition = transform.position;
        }

        private void OnEnable()
        {
            _input.OnJumpPressed += StartRise;
            _input.OnJumpReleased += StopRise;

            if (trail != null)
                trail.Play();
        }

        private void OnDisable()
        {
            _input.OnJumpPressed -= StartRise;
            _input.OnJumpReleased -= StopRise;

            if (trail != null)
                trail.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        private void FixedUpdate()
        {
            float acceleration = _isRising ? riseAcceleration : -fallAcceleration;
            float newVelocityY = Mathf.Clamp(_rb.linearVelocity.y + acceleration * Time.fixedDeltaTime,
                -maxFallSpeed, maxRiseSpeed);

            _rb.linearVelocity = new Vector2(0f, newVelocityY);
        }

        public void ResetPosition()
        {
            transform.position = _startPosition;
            _rb.linearVelocity = Vector2.zero;
            _isRising = false;
        }

        public void Die()
        {
            OnPlayerDeath?.Invoke();
        }

        private void StartRise()
        {
            _isRising = true;
        }

        private void StopRise()
        {
            _isRising = false;
        }
    }
}