using System;
using ParadoxNotion;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private LayerMask _target;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AgentMoveTo _follow;
        
        private bool _hasAggroTarget;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            _follow.enabled = false;
        }

        private void TriggerEnter(Collider obj)
        {
            if (!_hasAggroTarget)
            {
                _hasAggroTarget = true;

                if (LayerMask.LayerToName(obj.gameObject.layer) == _target.MaskToString())
                {
                    SwitchFollowOn();
                    _follow.SetTarget(obj.gameObject.GetComponent<Transform>());
                }
            }
        }

        private void TriggerExit(Collider obj) =>
            SwitchFollowOff();

        private bool SwitchFollowOn() =>
            _follow.enabled = true;

        private bool SwitchFollowOff() =>
            _follow.enabled = false;
    }
}