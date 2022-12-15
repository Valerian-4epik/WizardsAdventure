using System;
using System.Collections.Generic;
using Data;
using DefaultNamespace;
using Enemy.Spawner;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<LevelData> _levelsInfo = new List<LevelData>();
        
    private LevelData _levelInfo;
    private PlayerProgress _playerProgress;
    private Map _map;
    private EnemyGenerator _enemyGenerator;

    public Map Map => _map; 
    public LevelData LevelInfo => _levelInfo;

    public event Action GenerateCompleted;

    private void Start()
    {
        _enemyGenerator = GetComponent<EnemyGenerator>();
        _enemyGenerator.EnemySpawned += CompleteLevelGenerate;
        
    }

    public void SetLevelProgress(PlayerProgress playerProgress)
    {
        _playerProgress = playerProgress;
        CreateMap();
    }

    private void CreateMap()
    {
        var levelData = GetLevelData(GetLevelIndex());
        SetLevelInfo(levelData);
        RenderSettings.skybox = _levelInfo.Skybox;
        var map = Instantiate(levelData.Map, transform.position, Quaternion.identity);
        _map = map.GetComponent<Map>();
    }

    private void CompleteLevelGenerate() => GenerateCompleted?.Invoke();
    private void SetLevelInfo(LevelData levelInfo) => _levelInfo = levelInfo;
    private int GetLevelIndex() => _playerProgress.GetLevel();

    private LevelData GetLevelData(int index)
    {
        foreach (var levelData in _levelsInfo)
        {
            if (levelData.Level == index)
                return levelData;
        }

        return null;
    }
}