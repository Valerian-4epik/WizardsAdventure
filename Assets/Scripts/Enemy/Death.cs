using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;
using UnityEngine.Serialization;

namespace Enemy
{
    [RequireComponent(typeof(Health))]
    public class Death : MonoBehaviour
    {
        private const string DEAD_LAYER = "Deadboy";

        [SerializeField] private Health _health;
        [SerializeField] private GameObject _deathFx;
        [SerializeField] private Animator _animator;
        [SerializeField] private SkinnedMeshRenderer _mesh;
        [SerializeField] private Material _deadMaterial;

        public event Action<GameObject> Happened;

        private AgentMoveTo _agentMoveTo;
        private BoxCollider _boxCollider;
        private NavMeshAgent _navMeshAgent;
        private AnimateAlongAgent _animateAlongAgent;

        private void Start()
        {
            _animateAlongAgent = gameObject.GetComponent<AnimateAlongAgent>();
            _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            _boxCollider = gameObject.GetComponent<BoxCollider>();
            _agentMoveTo = gameObject.GetComponent<AgentMoveTo>();
            _health.HealthChanged += HealthChanged;
    }

        private void OnDestroy() =>
            _health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (_health.CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            PlayExplosion();
            PlayDead();
            Happened?.Invoke(gameObject);
        }

        private void PlayExplosion() => _animator.PlayDie();

        private void PlayDead()
        {
            DisableCompanents();
            gameObject.layer = LayerMask.NameToLayer(DEAD_LAYER);
            _mesh.material = _deadMaterial;
        }

        private void DisableCompanents()
        {
            _agentMoveTo.enabled = false;
            _boxCollider.enabled = false;
            _navMeshAgent.enabled = false;
            _animateAlongAgent.enabled = false;
        }

        private void SpawnDeathFx() =>
            Instantiate(_deathFx, transform.position, Quaternion.identity);
    }
}