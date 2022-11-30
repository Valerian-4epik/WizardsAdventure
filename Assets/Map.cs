using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private List<Transform> _enemyPoints = new List<Transform>();

    public List<Transform> EnemyPoints => _enemyPoints;
}
