using System;
using UnityEngine;
using UnityEngine.AI;
using Wizards;

namespace Enemy
{
    public class AnimateAlongAgent : MonoBehaviour
    {
        private const float MinimalVelocity = 0.1f;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private WizardAnimator _animator;

        private void Update()
        {
            if (ShouldMove())
            {
                _animator.Move(_agent.velocity.magnitude);
            }
            else
            {
                _animator.StopMoving();
            }
        }

        public void StopMoving() => _animator.StopMoving();
        
        private bool ShouldMove() =>
            _agent.velocity.magnitude > MinimalVelocity && _agent.remainingDistance > _agent.radius;
    }
}