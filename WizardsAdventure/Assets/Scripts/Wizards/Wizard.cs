using System;
using Enemy;
using UnityEngine;

namespace Wizards
{
    public class Wizard : MonoBehaviour
    {
        [SerializeField] private ItemInfo _item;
        [SerializeField] private CheckAttackRange _checkAttackRange;
        [SerializeField] private Attack _attack;

        private Weapon _weapon;
        
        private void Start()
        {
            _weapon = new Weapon(transform, _item);
            _attack.Weapon = _weapon;
            SetupAttackRange();
        }

        private void SetupAttackRange() => 
            _checkAttackRange.ChangeAttackRange(_item.AttackRange);
    }
}