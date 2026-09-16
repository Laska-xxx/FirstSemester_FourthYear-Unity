using UnityEngine;
using System.Collections;
using Zenject;

namespace Environment.Asteroids
{
    public class AsteroidFactory : MonoBehaviour
    {
        [SerializeField] private float minY;
        [SerializeField] private float maxY;
        [SerializeField] private float spawnX;
        [SerializeField] private float despawnX;
        [SerializeField] private float flightDuration;
        [SerializeField] private float minSpawnDelay;
        [SerializeField] private float maxSpawnDelay;

        private Asteroid.Pool _asteroidPool;

        [Inject] private void Init(Asteroid.Pool asteroidPool)
        {
            _asteroidPool = asteroidPool;
        }

        private void OnEnable()
        {
            StartCoroutine(SpawnCooldown());
        }

        private IEnumerator SpawnCooldown()
        {
            while (true)
            {
                float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);

                yield return new WaitForSeconds(spawnDelay);
                
                SpawnAsteroids();

            }
        }

        private void SpawnAsteroids()
        {
            Vector3 startPosition = new Vector3(spawnX, Random.Range(minY, maxY), 0f);
            Vector3 endPosition = new Vector3(despawnX, Random.Range(minY, maxY), 0f);
            _asteroidPool.Spawn(startPosition, endPosition, flightDuration);
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