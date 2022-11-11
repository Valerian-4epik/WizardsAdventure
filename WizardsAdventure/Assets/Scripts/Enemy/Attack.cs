using System;
using Blobcreate.ProjectileToolkit;
using UnityEngine;
using Wizards;

namespace Enemy
{
    public class Attack : MonoBehaviour
    {
        private const float BASE_DAMAGE = 5;

        [SerializeField] private WizardAnimator _animator;
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
            if (_animator != null && _weapon != null)
                ChoiceAttackAnimation(_weapon.Info);
            else if (_animator != null && _weapon == null)
                _animator.PlayAttack();

            _isAttacking = true;
            _cooldown = _attackCooldown;
            // OnAttack(_damage);
        }

        private void OnAttack()
        {
            if (_weapon != null)
            {
                Debug.Log("Атака с оружием");
                if (_weapon.Info.AttackType == AttackType.MeleeAttack)
                    GetTargetHealth().TakeDamage(_weapon.Info.Damage);
                else
                    RangeAttack(_targetTransform);

                _isAttacking = false;
            }
            else
            {
                Debug.Log("Атака без оружия");
                GetTargetHealth().TakeDamage(BASE_DAMAGE);
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

        private void ChoiceAttackAnimation(ItemInfo item)
        {
            switch (item.TypeOfObject)
            {
                case TypeOfObject.Sword:
                    _animator.PlayAttack();
                    break;
                case TypeOfObject.Staff:
                    _animator.PlayStaffAttack();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}