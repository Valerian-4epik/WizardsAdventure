using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Aggro))]
    [RequireComponent(typeof(Attack))]
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private Attack _attack;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AgentMoveTo _agentMoveTo;
        [SerializeField] private Aggro _aggro;

        private List<GameObject> _targets = new List<GameObject>();

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
            Debug.Log("Подох");
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

        private bool IsEnemy(Collider obj) =>
            LayerMask.LayerToName(obj.gameObject.layer) == _aggro.Target;
    }
}