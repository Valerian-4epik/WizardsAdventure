using System;
using Blobcreate.ProjectileToolkit;
using UnityEngine;
using Wizards;

namespace Enemy
{
    public class Attack : MonoBehaviour
    {
        [Range(0, 10)] [SerializeField] private float _attackCooldown;

        private Transform _targetTransform;
        private bool _isAttacking;
        private float _cooldown;
        private bool _attackIsActive;
        private Weapon _weapon;

        public Weapon Weapon
        {
            get => _weapon;
            set => _weapon = value;
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

        public void EnableAttack() =>
            _attackIsActive = true;

        public void DisableAttack() =>
            _attackIsActive = false;

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _cooldown -= Time.deltaTime;
        }

        private bool CanAttack() =>
            _attackIsActive && !_isAttacking && CooldownIsUp();

        private bool CooldownIsUp() =>
            _cooldown <= 0;

        private void StartAttack()
        {
            transform.LookAt(_targetTransform);
            _isAttacking = true;
            _cooldown = _attackCooldown;
            // Debug.Log("Attack");
            OnAttack(_damage);
        }

        private void OnAttack(float damage)
        {
            if (_weapon != null)
            {
                Debug.Log("Атака с оружием");
                RangeAttack(_targetTransform);
                _isAttacking = false;
            }
            else
            {
                Debug.Log("Атака без оружия");
                GetTargetHealth().TakeDamage(damage);
                _isAttacking = false;
            }
        }

        private void RangeAttack(Transform target)
        {
            var projectile = Instantiate(_weapon.ProjecttileRig, transform.position, Quaternion.identity);
            projectile.gameObject.GetComponent<ETFXProjectileScript>().Damage = _weapon.Info.Damage;
            var direction = Projectile.VelocityByTime(projectile.position,
                target.position, _weapon.Info.AttackSpeed);
            projectile.AddForce(direction, ForceMode.VelocityChange);
        }

        private Health GetTargetHealth() =>
            _targetTransform.gameObject.GetComponent<Health>();
    }
}