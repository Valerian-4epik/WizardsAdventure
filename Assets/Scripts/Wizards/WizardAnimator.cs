using System;
using Infrastructure.Logic;
using UnityEngine;

namespace Wizards
{
    public class WizardAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Attack = Animator.StringToHash("Attack_1_bool");
        private static readonly int StaffAttack = Animator.StringToHash("Attack_2_bool");
        private static readonly int AttackSpeed = Animator.StringToHash("AttackSpeed");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsVictory = Animator.StringToHash("IsVictory");
        private static readonly int TakeWeapon = Animator.StringToHash("TakeWeapon");
        private static readonly int ReturnWeapon = Animator.StringToHash("ReturnWeapon");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Dead");
        
        private readonly int _idleStateHash = Animator.StringToHash("idle");
        private readonly int _attackStateHash = Animator.StringToHash("attack01");
        private readonly int _walkingStateHash = Animator.StringToHash("Move");
        private readonly int _deathStateHash = Animator.StringToHash("die");
        
        private Animator _animator;
        
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        
        public AnimatorState State { get; private set; }
        
        private void Awake() =>
            _animator = GetComponent<Animator>();
        
        public void PlayHit() => _animator.SetTrigger(Hit);
        public void PlayDeath() => _animator.SetTrigger(Die);
        
        public void Move(float speed)
        {
            _animator.SetBool(IsMoving, true);
            //_animator.SetFloat(Speed, speed);
        }
        
        public void StopMoving() => _animator.SetBool(IsMoving, false);
        public void ExitAttack() => _animator.SetBool(Attack, false);
        public void PlayAttack() => _animator.SetBool(Attack, true);
        public void PlayStaffAttack() => _animator.SetBool(StaffAttack, true);
        public void ExitRangeAttack() => _animator.SetBool(StaffAttack, false);
        public void PlayVictory() => _animator.SetBool(IsVictory, true);
        public void PlayTakeWeapon() => _animator.SetTrigger(TakeWeapon);
        public void PlayReturnWeapon() => _animator.SetTrigger(ReturnWeapon);
        public void PlayDie() => _animator.SetTrigger(Die);
        public void SetSpeed(float value) => _animator.SetFloat(AttackSpeed, value);


        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) =>
            StateExited?.Invoke(StateFor(stateHash));

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _attackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _walkingStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;
        
            return state;
        }
    }
}