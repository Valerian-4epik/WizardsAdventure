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

        public event Action<GameObject> Happened;

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
            Happened?.Invoke(gameObject);
            gameObject.SetActive(false);
            // StartCoroutine(DestroyTimer());
            
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