using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroesSpawner : MonoBehaviour
{
    [SerializeField] private List<InitPoint> _initPoints = new List<InitPoint>();

    private void OnEnable()
    {
        AddAllInitPoints();
    }

    private void AddAllInitPoints()
    {
        InitPoint[] array = GetComponentsInChildren<InitPoint>();
        _initPoints = array.ToList();
    }
}
