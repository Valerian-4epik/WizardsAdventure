using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Enemy.Spawner
{
    public class EnemyGenerator : MonoBehaviour
    {
        private LevelGenerator _levelGenerator;
        private Map _map;
        private LevelData _levelData;
        private List<Transform> _spawnPoints = new List<Transform>();
        private List<GameObject> _enemies = new List<GameObject>();

        public event Action EnemySpawned; 

        private void Start()
        {
            _levelGenerator = GetComponent<LevelGenerator>();
            _map = _levelGenerator.Map;
            _levelData = _levelGenerator.LevelInfo;
            Spawn();
            EnemySpawned?.Invoke();
        }

        private void Spawn()
        {
            AddAllEnemies();

            for (int i = 0; i < _enemies.Count; i++)
            { 
                Instantiate(_enemies[i], _map.EnemyPoints[i].position, Quaternion.Euler(0,180,0));
            }
        }

        private void AddAllEnemies()
        {
            if (_levelData.NumberOfEnemy1 != 0)
                AddEnemies(_levelData.NumberOfEnemy1, _levelData.Enemy1);
            if (_levelData.NumberOfEnemy2 != 0)
                AddEnemies(_levelData.NumberOfEnemy2, _levelData.Enemy2);
            if (_levelData.NumberOfEnemy3 != 0)
                AddEnemies(_levelData.NumberOfEnemy3, _levelData.Enemy3);
        }
        
        private void AddEnemies(int amount, GameObject enemy)
        {
            for (int i = 0; i < amount; i++)
            {
                _enemies.Add(enemy);
            }
        }
    }
}
