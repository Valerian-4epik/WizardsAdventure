using System.Collections.Generic;
using UI;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData")]
    public class LevelData: ScriptableObject
    {
        [SerializeField] private string _iD;
        [SerializeField] private int _level;
        [SerializeField] private GameObject _map;
        [SerializeField] private GameObject _enemy1;
        [SerializeField] private int _numberOfEnemy1;
        [SerializeField] private GameObject _enemy2; 
        [SerializeField] private int _numberOfEnemy2; 
        [SerializeField] private GameObject _enemy3;
        [SerializeField] private int _numberOfEnemy3;
        [SerializeField] private Material _skybox;

        public string ID => _iD;
        public int Level => _level;
        public GameObject Map => _map;
        public GameObject Enemy1 => _enemy1;
        public int NumberOfEnemy1 => _numberOfEnemy1;
        public GameObject Enemy2 => _enemy2;
        public int NumberOfEnemy2 => _numberOfEnemy2;
        public GameObject Enemy3 => _enemy3;
        public int NumberOfEnemy3 => _numberOfEnemy3;
        public Material Skybox => _skybox;
    }
}