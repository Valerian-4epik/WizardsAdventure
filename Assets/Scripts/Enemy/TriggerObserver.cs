using System;
using Infrastructure.Logic;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;

        private void OnTriggerEnter(Collider other)
        {
            ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
            TriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
            TriggerExit?.Invoke(other);
        }
    }
}