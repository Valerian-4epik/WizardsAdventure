using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.Playables;

namespace Infrastructure.Logic
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private float _smooth;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Quaternion _rotation;
        [SerializeField] private PlayableDirector _playableDirector;

        private Transform _target;
        private Camera _cameraMain;
        private Transform _cameraPosition;
        private UIInventory _shopInterface;
        private float _deltaZ;
        private bool _canFollowing = false;

        private void OnEnable()
        {
            if (Camera.main != null)
                _cameraMain = Camera.main;
        }

        private void Update()
        {
            if (_canFollowing)
            {
                var cameraPosition = _cameraMain.transform.position;
                var targetPosition = new Vector3(cameraPosition.x, _target.position.y, _target.position.z);
                cameraPosition = Vector3.Lerp(cameraPosition, targetPosition + _offset, Time.deltaTime * _smooth);
                _cameraMain.transform.position = cameraPosition;
            }
        }

        public void SetShopInterface(UIInventory shopInterface)
        {
            _shopInterface = shopInterface;
            _shopInterface.Fight += ActivateTargetFollower;
        }

        public void SetTarget(Transform target) => _target = target;

        public void ShowEnemyPosition()
        {
            
        }
        
        private void ActivateTargetFollower()
        {
            _canFollowing = true;
            SetupRotate();
        }

        private void SetupRotate() => _cameraMain.transform.DORotateQuaternion(_rotation, 2);
    }
}
