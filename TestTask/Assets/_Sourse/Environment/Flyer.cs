using DG.Tweening;
using Player;
using UnityEngine;

namespace Environment
{
    public abstract class Flyer : MonoBehaviour
    {
        private Tween _flightTween;

        protected void SetFlightTween(Tween flightTween)
        {
            _flightTween = flightTween;
        }

        protected void StopFlightTween()
        {
            _flightTween?.Kill();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<PlayerController>(out var player))
            {
                ProcessPlayerContact(player);
            }
        }

        protected abstract void ProcessPlayerContact(PlayerController player);

        private void OnDestroy()
        {
            StopFlightTween();
        }
    }
}