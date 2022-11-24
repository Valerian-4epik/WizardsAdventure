using System.Collections.Generic;
using UnityEngine;
using ParadoxNotion;
using Wizards;

namespace Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetLayerMask;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AgentMoveTo _follow;

        private List<GameObject> _targets = new List<GameObject>();

        public string Target => _targetLayerMask.MaskToString();
        public LayerMask TargetMask => _targetLayerMask;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            _follow.enabled = false;
        }

        private void RemoveTarget(GameObject target)
        {
            SwitchFollowOff();
            _targets.Remove(target);
            
            if (_targets.Count != 0)
            {
                SwitchFollowOn();
                _follow.SetTarget(_targets[0].GetComponent<Transform>());
            }
        }

        private void TriggerEnter(Collider obj)
        {
            if (LayerMask.LayerToName(obj.gameObject.layer) == _targetLayerMask.MaskToString())
            {
                _targets.Add(obj.gameObject);
                // obj.gameObject.GetComponent<Death>().Dead += RemoveTarget;
                SwitchFollowOn();
                _follow.SetTarget(_targets[0].GetComponent<Transform>());
            }
        }

        private void TriggerExit(Collider obj)
        {
            SwitchFollowOff();
            _targets.Remove(obj.gameObject);
            
            if (_targets.Count != 0)
            {
                SwitchFollowOn();
                _follow.SetTarget(_targets[0].GetComponent<Transform>());
            }
        }

        private void SwitchFollowOn() => _follow.enabled = true;

        private bool SwitchFollowOff() =>
            _follow.enabled = false;
    }
}