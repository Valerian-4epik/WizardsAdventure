using System;
using UnityEngine;

namespace Enemy
{
    public class Attack : MonoBehaviour
    {
        [Range(0, 10)] [SerializeField] private float _attackCooldown;

        private Transform _targetTransform;
        private bool _isAttacking;
        private float _cooldown;

        private void Start()
        {
            _cooldown = _attackCooldown;
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
            { 
                StartAttack();
            }
        }

        public void SetTarget(Transform target) => 
            _targetTransform = target;

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _cooldown -= Time.deltaTime;
            else
                _isAttacking = false;
        }

        private bool CanAttack() => 
            !_isAttacking && CooldownIsUp();

        private bool CooldownIsUp() => 
            _cooldown <= 0;

        private void StartAttack()
        {
            transform.LookAt(_targetTransform);
            _isAttacking = true;
            _cooldown = _attackCooldown;
            Debug.Log("Attack");
        }
     }
}