using TMPro;
using UnityEngine;
using DG.Tweening;

namespace UI
{
    public class FloatingText : MonoBehaviour
    {
        private const float DESTROY_TIME = 3f;
        private const float DURATION = 1f;

        [SerializeField] private TMP_Text _text;
        
        private readonly Vector3 _targetPosition = new Vector3(0, 1, 0);
        
        private void Start()
        {
            Destroy(gameObject, DESTROY_TIME);
            transform.DOJump(transform.position + _targetPosition + GetRandomDirection(), 1, 1, DURATION);
        }

        private Vector3 GetRandomDirection()
        {
            var randomValue = Random.Range(-4, 4);
            return new Vector3(randomValue, 0, 0);
        }

        public void SetValue(float value) => 
            _text.text = value.ToString();
    }
}
