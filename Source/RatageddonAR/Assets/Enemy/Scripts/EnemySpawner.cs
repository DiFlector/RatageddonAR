using System;
using UnityEngine;
using Zenject;

namespace Enemy.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [Inject] private GameManager _gameManager;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private int _maxSpawn;
        private int _counter;
        private float _timer;
        public float _spawnRate;
        [SerializeField] private bool _canSpawn;

        private void Start()
        {
            //_gameManager.GetTask<PreparationTask>().OnTaskCompleted += ((() => _canSpawn = true));
        }

        private void SpawnEnemy()
        {
            if(!_canSpawn) return;
            if (_counter>=_maxSpawn) return;
            Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
            _counter++;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if(_timer <= _spawnRate) return;
            SpawnEnemy();
            _timer = 0f;
        }
    }
}
