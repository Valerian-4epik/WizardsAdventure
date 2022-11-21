using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    [RequireComponent(typeof(Health))]
    public class Death : MonoBehaviour
    {
        private const string DEAD_LAYER = "Deadboy";
        
        [SerializeField] private Health _health;
        [SerializeField] private GameObject _deathFx;
        [SerializeField] private Animator _animator;
        [SerializeField] private SkinnedMeshRenderer _mesh;
        [SerializeField] private Material _deadMaterial;

        public event Action<GameObject> Happened;
        public event Action<GameObject> Dead;

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
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Dead?.Invoke(gameObject);
            PlayDead();
            
            // SpawnDeathFx();
            Happened?.Invoke(gameObject);
            // gameObject.SetActive(false);
        }

        private void PlayDead()
        {
            gameObject.layer = LayerMask.NameToLayer(DEAD_LAYER);
            _animator.enabled = false;
            _mesh.material = _deadMaterial;
        }

        private void SpawnDeathFx() => 
            Instantiate(_deathFx, transform.position, Quaternion.identity);
    }
}