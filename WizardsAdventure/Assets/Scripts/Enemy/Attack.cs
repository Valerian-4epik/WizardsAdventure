using System;
using UnityEngine;

namespace Enemy
{
    public class Attack : MonoBehaviour
    {
        [Range(0, 10)] [SerializeField] private float _attackCooldown;

        private Transform _targetTransform;
        private bool _isAttacking;

        private void Update()
        {
            if (!CooldownIsUp())
                _attackCooldown -= Time.deltaTime;
            
            if (!_isAttacking && CooldownIsUp())
            {
                StartAttack();
            }
        }

        public void SetTarget(Transform target)
        {
            _targetTransform = target;
        }

        private bool CooldownIsUp() => 
            _attackCooldown <= 0;

        private void StartAttack()
        {
            transform.LookAt(_targetTransform);
            _isAttacking = true;
            Debug.Log("Attack");
        }
    }
}