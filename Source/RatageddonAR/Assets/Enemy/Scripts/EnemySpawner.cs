using UnityEngine;

namespace Enemy.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private int _maxSpawn;
        [SerializeField] private float _xRandomOffset;
        private int _counter;
        private float _timer;
        public float _spawnRate;
        [SerializeField] private bool _canSpawn;

        public void StartSpawn() => _canSpawn = true;


        private void SpawnEnemy()
        {
            if(!_canSpawn) return;
            if (_counter>=_maxSpawn) return;
            Instantiate(_enemyPrefab, GetRandomPosition(), Quaternion.identity);
            _counter++;
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 randomOffset = new Vector3(Random.Range(-_xRandomOffset, _xRandomOffset), 0, 0);
            Vector3 worldOffset = _spawnPoint.TransformDirection(randomOffset);
            return _spawnPoint.position + worldOffset;
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
