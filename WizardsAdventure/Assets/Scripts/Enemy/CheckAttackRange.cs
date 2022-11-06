using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Aggro))]
    [RequireComponent(typeof(Attack))]
    public class CheckAttackRange : MonoBehaviour
    {
        private const float BASE_ATTACK_RANGE = 1;

        [SerializeField] private Attack _attack;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AgentMoveTo _agentMoveTo;
        [SerializeField] private Aggro _aggro;

        private float _attackRange;
        private List<GameObject> _targets = new List<GameObject>();

        public float AttackRange
        {
            get => _attackRange;
            set => _attackRange = value;
        }


        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            _attack.enabled = false;
        }

        private void TriggerEnter(Collider obj)
        {
            if (IsEnemy(obj))
            {
                _targets.Add(obj.gameObject);
                _attack.enabled = true;
                _attack.SetTarget(_targets[0].transform);
                _agentMoveTo.IsTargetInAttackZone = true;
                _attack.EnableAttack();
            }
        }

        private void TriggerExit(Collider obj)
        {
            _attack.DisableAttack();
            _targets.Remove(obj.gameObject);

            if (_targets.Count != 0)
            {
                _attack.SetTarget(_targets[0].transform);
                _attack.EnableAttack();
            }
            else
                _agentMoveTo.IsTargetInAttackZone = false;
        }

        public void ChangeAttackRange(float value) =>
            _triggerObserver.gameObject.GetComponent<SphereCollider>().radius = value;
        
        public void SetupDefaultRange() =>
            _triggerObserver.gameObject.GetComponent<SphereCollider>().radius = BASE_ATTACK_RANGE;

        private bool IsEnemy(Collider obj) =>
            LayerMask.LayerToName(obj.gameObject.layer) == _aggro.Target;
    }
}