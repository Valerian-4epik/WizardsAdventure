using System;
using Blobcreate.ProjectileToolkit;
using UnityEngine;
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
        private float _additionaluAttackSpeed;
        private bool _isAttacking;
        private float _cooldown;
        private bool _attackIsActive;
        private Weapon _weapon;
        private Transform _projectileShootPoint;

        public Weapon Weapon
        {
            get => _weapon;
            set { _weapon = value; }
        }

        public void SetAdditionalyAttackSpeed(float value) => _additionaluAttackSpeed = value;

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
                PlayOrdinaryAttack();
                
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
                target.position, 1);
            projectile.AddForce(direction, ForceMode.VelocityChange);
        }

        private Health GetTargetHealth()
        {
            if (_targetTransform != null)
            {
                var health = _targetTransform.gameObject.GetComponent<Health>();

                if (health.IsDead)
                    return null;
                else
                    return health;
            }

            return null;
        }

        private void ChoiceAttackAnimation(ItemInfo item)
        {
            switch (item.TypeOfObject)
            {
                case TypeOfObject.Sword:
                    PLayAttack(item.AttackSpeed);
                    break;
                case TypeOfObject.Staff:
                    PlayStaffAttack(item.AttackSpeed);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PLayAttack(float speed)
        {
            if (_additionaluAttackSpeed != 0)
            {
                var allSpeed = speed + _additionaluAttackSpeed;
                _animator.SetSpeed(allSpeed);
                _animator.PlayAttack();
            }
            else
            {
                _animator.SetSpeed(speed);
                _animator.PlayAttack();
            }
        }

        private void PlayStaffAttack(float speed)//посмотреть если различие межну обычной аттакой и стафф аттакой
        {
            if (_additionaluAttackSpeed != 0)
            {
                var allSpeed = speed + _additionaluAttackSpeed;
                _animator.SetSpeed(allSpeed);
                _animator.PlayStaffAttack();
            }
            else
            {
                _animator.SetSpeed(speed);
                _animator.PlayStaffAttack();
            }
        }

        private void PlayOrdinaryAttack()
        {
            if(_additionaluAttackSpeed != 0)
                _animator.PlayAttack();
            else
            {
                _animator.SetSpeed(1.5f + _additionaluAttackSpeed);//вот тут залипуху сделаю потом нужно будет исправить 
                _animator.PlayAttack();
            }
        }
    }
}