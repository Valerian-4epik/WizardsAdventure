using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WizardsSpawner : MonoBehaviour
{
    [SerializeField] private List<InitPoint> _initPoints = new List<InitPoint>();
    [SerializeField] private GameObject _wizard;

    private PlayerStats _player;

    private void OnEnable()
    {
        _player = new PlayerStats(3);
        AddAllInitPoints();
        Spawn();
    }

    private void AddAllInitPoints()
    {
        InitPoint[] array = GetComponentsInChildren<InitPoint>();
        _initPoints = array.ToList();
    }

    private void Spawn()
    {
        for (int i = 0; i < _player.AmountWizards; i++)
        {
            var wizard = Instantiate(_wizard, _initPoints[i].transform.position, Quaternion.identity);
        }
    }
}