using Player;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Environment.Boosters
{
    public class BoosterFactory : MonoBehaviour
    {
        [SerializeField] private float minY;
        [SerializeField] private float maxY;
        [SerializeField] private float spawnX;
        [SerializeField] private float despawnX;
        [SerializeField] private float flightDuration;
        [SerializeField] private float minSpawnDelay;
        [SerializeField] private float maxSpawnDelay;

        private Booster.Pool _boosterPool;
        private Transform _playerTransform;

        [Inject]
        private void Init(Booster.Pool pool, PlayerController player)
        {
            _boosterPool = pool;
            _playerTransform = player.transform;
        }

        private void OnEnable()
        {
            StartCoroutine(SpawnCooldown());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator SpawnCooldown()
        {
            while (true)
            {
                float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);

                yield return new WaitForSeconds(spawnDelay);

                SpawnBooster();
            }
        }
        
        private void SpawnBooster()
        {
            Vector3 startPosition = new Vector3(spawnX, Random.Range(minY, maxY), 0f);
            Vector3 endPosition = new Vector3(despawnX, Random.Range(minY, maxY), 0f);

            _boosterPool.Spawn(startPosition, endPosition, flightDuration, _playerTransform);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(new Vector3(spawnX, minY, 0f), new Vector3(spawnX, maxY, 0f));

            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(despawnX, minY, 0f), new Vector3(despawnX, maxY, 0f));

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(new Vector3(spawnX, minY, 0f), new Vector3(despawnX, minY, 0f));
            Gizmos.DrawLine(new Vector3(spawnX, maxY, 0f), new Vector3(despawnX, maxY, 0f));
        }
    }
}