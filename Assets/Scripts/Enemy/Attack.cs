using System;
using Blobcreate.ProjectileToolkit;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.Utilities;
using UnityEngine.Serialization;
using Wizards;

namespace Enemy
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private float _baseDamage = 5;
        [SerializeField] private WizardAnimator _animator;
        [Range(0, 10)] [SerializeField] private float _attackCooldown;
        [SerializeField] private Aggro _aggroZone;
        [SerializeField] private AudioPlayerForWizard _audioPlayer;

        private Transform _targetTransform;
        private bool _isAttacking;
        private float _cooldown;
        private bool _attackIsActive;
        private Weapon _weapon;
        private Transform _projectileShootPoint;

        public Weapon Weapon
        {
            get => _weapon;
            set
            {
                _weapon = value;
            }
        }

        public void EnableAttack(Transform target)
        {
            SetTarget(target);
            _attackIsActive = true;
        }

        public void SetProjectileShootPoint(Transform shootPoint) => _projectileShootPoint = shootPoint;  
        
        public void DisableAttack()
        {
            ResetTarget();
            _attackIsActive = false;

            if (_weapon != null)
            {
                if (_weapon.Info.AttackType != AttackType.RangeAttack)
                    _animator.ExitAttack();
                else
                    _animator.ExitRangeAttack();
            }
            else
                _animator.ExitAttack();
        }

        private void ResetTarget() => _targetTransform = null;

        private void SetTarget(Transform target)
        {
            _targetTransform = target;
            StartAttack();
        }

        private void StartAttack()
        {
            transform.LookAt(_targetTransform);
            if (_animator != null && _weapon != null)
                ChoiceAttackAnimation(_weapon.Info);
            else if (_animator != null && _weapon == null)
                _animator.PlayAttack();
        }

        private void OnAttack()
        {
            if (GetTargetHealth() != null)
            {
                if (_weapon != null)
                {
                    if (_weapon.Info.AttackType == AttackType.MeleeAttack)
                        GetTargetHealth().TakeDamage(_weapon.Info.Damage);
                    else
                        RangeAttack(_targetTransform);
                }
                else
                {
                    GetTargetHealth().TakeDamage(_baseDamage);
                    _audioPlayer.PlayAttackSoundWithoutWeapon();
                }
            }
        }

        private void RangeAttack(Transform target)
        {
            var projectile = Instantiate(_weapon.ProjecttileRig, _projectileShootPoint.position, Quaternion.identity);
            var etfxProjectileScript = projectile.gameObject.GetComponent<ETFXProjectileScript>();
            etfxProjectileScript.TargetMask = _aggroZone.TargetMask;
            etfxProjectileScript.Damage = _weapon.Info.Damage;
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