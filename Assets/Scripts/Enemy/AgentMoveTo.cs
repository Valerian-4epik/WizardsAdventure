using Enemy;
using Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;
using Wizards;

public class AgentMoveTo : MonoBehaviour
{
    private const float MINIMAL_DISTANCE = 1;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private AnimateAlongAgent _animateAlongAgent;

    private Transform _targetTransform;
    private IGameFactory _gameFactory;
    private bool _isTargetInAttackZone;
    
    public bool IsTargetInAttackZone
    {
        set
        {
            _isTargetInAttackZone = value;
            _animateAlongAgent.StopMoving();
        }
    }

    private void Update()
    {
        if (_agent.enabled)
        {
            if (Initialized() && HeroNotReached() && !_isTargetInAttackZone)
            {
                _agent.destination = _targetTransform.position;
            }
            else
            {
                _agent.destination = transform.position;
            }
        }
    }

    public void SetTarget(Transform target) =>
        _targetTransform = target;

    private bool Initialized() =>
        _targetTransform != null;

    private bool HeroNotReached() =>
        Vector3.Distance(_agent.transform.position, _targetTransform.position) >= MINIMAL_DISTANCE;
}