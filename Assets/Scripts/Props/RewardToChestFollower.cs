using Cinemachine.Utility;
using Infrastructure.Logic;
using UnityEngine;

namespace Props
{
    public class RewardToChestFollower : MonoBehaviour
    {
        [SerializeField] private GameObject _chest;

        private Transform _instantiatePoint;
        private ArenaDisposer _arenaDisposer;
        private Transform _chestTransform;

        public Transform ChestTransform => _chestTransform;

        public void GetInstantiatePoint(Transform transform, ArenaDisposer arenaDisposer)
        {
            _instantiatePoint = transform;
            _arenaDisposer = arenaDisposer;
            _arenaDisposer.EndFight += CheckGameEnded;
        }

        private void CheckGameEnded(bool value)
        {
            if (value)
                ChestInstantiate();
        }
        
        private GameObject ChestInstantiate()
        {
            var chest = Instantiate(_chest, _instantiatePoint.position, Quaternion.LookRotation(Vector3.back));
            _chestTransform = chest.transform;
            chest.GetComponent<BigGoldChest>().ChestStateEnded += _arenaDisposer.ActivateFinishInterface;
            return chest;
        }
    }
}