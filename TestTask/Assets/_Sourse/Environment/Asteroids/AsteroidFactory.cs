using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

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

        private AsteroidPool _asteroidPool;
        private readonly List<Asteroid> _activeAsteroids = new List<Asteroid>();

        [Inject] private void Init(AsteroidPool asteroidPool)
        {
            _asteroidPool = asteroidPool;
        }

        private void OnEnable()
        {
            StartCoroutine(SpawnCooldown());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            DespawnAll();
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

            Asteroid asteroid = _asteroidPool.Spawn(startPosition, endPosition, flightDuration);
            _activeAsteroids.Add(asteroid);
            asteroid.OnDespawned += HandleDespawned;
        }

        private void HandleDespawned(Asteroid asteroid)
        {
            asteroid.OnDespawned -= HandleDespawned;
            _activeAsteroids.Remove(asteroid);
        }

        private void DespawnAll()
        {
            for (int i = _activeAsteroids.Count - 1; i >= 0; i--)
            {
                _activeAsteroids[i].ForceDespawn();
            }
            _activeAsteroids.Clear();
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