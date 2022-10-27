using Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;

public class AgentMoveTo : MonoBehaviour
{
    private const float MinimalDistance = 2;

    [SerializeField] private NavMeshAgent _agent;

    private Transform _targetTransform;
    private IGameFactory _gameFactory;
    private bool _isTargetInAttackZone;

    public bool IsTargetInAttackZone
    {
        set => _isTargetInAttackZone = value;
    }
    
    private void Update()
    {
        if (Initialized() && HeroNotReached() && !_isTargetInAttackZone)
            _agent.destination = _targetTransform.position;
        else
            _agent.destination = transform.position;
    }

    public void SetTarget(Transform target) => 
        _targetTransform = target;

    private bool Initialized() => 
        _targetTransform != null;

    private bool HeroNotReached() => 
        Vector3.Distance(_agent.transform.position, _targetTransform.position) >= MinimalDistance;
}