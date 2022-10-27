using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    [RequireComponent(typeof(Health))]
    public class Death : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private GameObject _deathFx;

        public event Action Happened;

        private void Start() => 
            _health.HealthChanged += HealthChanged;

        private void OnDestroy() => 
            _health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (_health.CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            SpawnDeathFx();
            Debug.Log("Точно подох");
            StartCoroutine(DestroyTimer());
            
            Happened?.Invoke();
        }

        private void SpawnDeathFx() => 
            Instantiate(_deathFx, transform.position, Quaternion.identity);

        private IEnumerator DestroyTimer()
        {
            yield return null;
            gameObject.SetActive(false);
        }
    }
}