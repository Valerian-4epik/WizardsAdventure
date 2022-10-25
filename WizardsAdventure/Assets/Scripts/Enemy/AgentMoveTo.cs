using Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;

public class AgentMoveTo : MonoBehaviour
{
    private const float MinimalDistance = 1;

    [SerializeField] private NavMeshAgent _agent;

    private Transform _targetTransform;
    private IGameFactory _gameFactory;

    private void Start()
    {
        // _gameFactory = AllServices.Container.Single<IGameFactory>(); // получение фактори через сингелнтон.
    }

    private void Update()
    {
        if (Initialized() && HeroNotReached())
            _agent.destination = _targetTransform.position;
    }

    public void SetTarget(Transform target) => 
        _targetTransform = target;

    private bool Initialized() => 
        _targetTransform != null;

    private bool HeroNotReached() => 
        Vector3.Distance(_agent.transform.position, _targetTransform.position) >= MinimalDistance;
}