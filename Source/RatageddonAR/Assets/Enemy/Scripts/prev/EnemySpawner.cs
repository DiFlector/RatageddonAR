/*using UnityEditor;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab; // Префаб врага
        public bool drawGizmos;

        [Header("Distance settings")]
        public int minDistance = 40; // Минимальное расстояние от точки спавна до врага

        public int maxDistance = 60; // Максимальное расстояние от точки спавна до врага

        [Header("Spawn settings")]
        [Tooltip("Максимальное количество врагов для спавна")]
        public int maxEnemies = 5;
        [Tooltip("Максимальное количество врагов на сцене в один момент времени")]
        public int maxSimultaneousEnemies = 5;
        [Tooltip("Текущее количество врагов на сцене")]
        public int _enemiesCount;
        public float spawnInterval;
        private int _enemiesSpawned;
        private float _spawnTimer;
        
        public bool ForestZone;

        private void Update()
        {
            if(_enemiesSpawned >= maxEnemies) return;
            if(_enemiesCount >= maxSimultaneousEnemies) return;
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                _spawnTimer = 0;
            }
        }
        

        private void SpawnEnemy()
        {
            // Выбираем случайное расстояние между 40 и 60
            float distance = Random.Range(minDistance, maxDistance);

            // Генерируем случайный угол
            float angle = Random.Range(0f, 360f);

            // Вычисляем позицию для спавна врага на основе расстояния и угла
            Vector3 spawnPosition = transform.position + Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;
            
            var terrain = GetClosestTerrain(spawnPosition);
            
            spawnPosition.y = terrain.SampleHeight(spawnPosition);

            // Создаем врага на вычисленной позиции
            var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.GetComponent<EnemyController>().isInForest = ForestZone;
            _enemiesCount++;
            _enemiesSpawned++;
        }
        
        private Terrain GetClosestTerrain(Vector3 spawnPosition)
        {
            Terrain[] terrains = Terrain.activeTerrains;
            Terrain closestTerrain = null;
            float closestDistance = float.MaxValue;

            foreach (Terrain terrain in terrains)
            {
                Vector3 closestPoint = terrain.transform.position + terrain.GetComponent<TerrainCollider>().ClosestPoint(spawnPosition);
                float distance = Vector3.Distance(spawnPosition, closestPoint);
        
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTerrain = terrain;
                }
            }

            return closestTerrain;
        }

        // Для отображения точки спавна в редакторе
        private void OnDrawGizmosSelected()
        {
            if (!drawGizmos) return;
            Handles.color = new Color(0f, 1f, 0f, 0.5f); 
            Handles.DrawSolidDisc(transform.position, Vector3.up, maxDistance);
            Handles.color = new Color(1f, 0f, 0f, 0.5f);
            Handles.DrawSolidDisc(transform.position, Vector3.up, minDistance);
        }
    }
}*/