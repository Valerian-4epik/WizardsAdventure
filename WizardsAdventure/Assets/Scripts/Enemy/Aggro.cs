using System.Collections.Generic;
using UnityEngine;
using ParadoxNotion;

namespace Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetLayerMask;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AgentMoveTo _follow;

        private List<GameObject> _targets = new List<GameObject>();

        public string Target => _targetLayerMask.MaskToString();

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            _follow.enabled = false;
        }

        private void TriggerEnter(Collider obj)
        {
            if (LayerMask.LayerToName(obj.gameObject.layer) == _targetLayerMask.MaskToString())
            {
                _targets.Add(obj.gameObject);
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

        private bool SwitchFollowOn() =>
            _follow.enabled = true;

        private bool SwitchFollowOff() =>
            _follow.enabled = false;
    }
}