using Player;
using System.Collections;
using System.Collections.Generic;
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

        private BoosterPool _boosterPool;
        private Transform _playerTransform;
        private readonly List<Booster> _activeBoosters = new List<Booster>();

        [Inject]
        private void Init(BoosterPool pool, PlayerController player)
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
            DespawnAll();
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
            float randomY = Random.Range(minY, maxY);
            Vector3 startPosition = new Vector3(spawnX, randomY, 0f);
            Vector3 endPosition = new Vector3(despawnX, randomY, 0f);

            Booster booster = _boosterPool.Spawn(startPosition, endPosition, flightDuration, _playerTransform);
            _activeBoosters.Add(booster);
            booster.OnDespawned += HandleDespawned;
        }

        private void HandleDespawned(Booster booster)
        {
            booster.OnDespawned -= HandleDespawned;
            _activeBoosters.Remove(booster);
        }

        private void DespawnAll()
        {
            for (int i = _activeBoosters.Count - 1; i >= 0; i--)
            {
                _activeBoosters[i].ForceDespawn();
            }
            _activeBoosters.Clear();
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