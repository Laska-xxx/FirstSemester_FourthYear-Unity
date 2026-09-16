using Player;
using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Wall : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerController>(out var player))
            {
                player.Die();
            }
        }
    }
}