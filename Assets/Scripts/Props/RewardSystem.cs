using Cinemachine.Utility;
using UnityEngine;

namespace Props
{
    public class RewardSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _chest;

        private Transform _instantiatePoint;

        public void GetInstantiatePoint(Transform transform)
        {
            var offset = new Vector3(0, 0, -5);
            _instantiatePoint = transform;
            _instantiatePoint.position += offset;
            ChestInstantiate(_instantiatePoint, Quaternion.identity);
        }

        private GameObject ChestInstantiate(Transform transform, Quaternion rotation)
        {
            var chest = Instantiate(_chest, transform.position, rotation);
            return chest;
        }  
        
        
            
    }
}