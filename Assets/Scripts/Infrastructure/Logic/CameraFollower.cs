using DG.Tweening;
using UI;
using UnityEngine;

namespace Infrastructure.Logic
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private float _smooth;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Quaternion _rotation;

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
                // cameraPosition = new Vector3(cameraPosition.x, cameraPosition.z, _target.position.z + _deltaZ);
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

        // public void ChangePosition()
        // {
        //     _cameraMain.gameObject.transform.DOMove(_fightTimePosition, DURATION);
        //     _cameraMain.gameObject.transform.DORotateQuaternion(_fightTimeRotation, DURATION);
        // }

        private void ActivateTargetFollower()
        {
            _canFollowing = true;
            SetupRotate();
        }

        private void SetupRotate() => _cameraMain.transform.DORotateQuaternion(_rotation, 2);

        // private void SetupDeltaZ() => _deltaZ = _cameraMain.transform.position.z - _target.position.z;
    }
}

// public class CameraFollowe: MonoBehaviour
//     {
//         [SerializeField] private Transform _target;
//         [SerializeField] private float _smooth;
//         [SerializeField] private Vector3 _offset;
//
//         private float _deltaX;
//         private bool _isFinishPositionActive;
//     
//         public bool IsFinishPositionActive
//         {
//             set => _isFinishPositionActive = value;
//         }
//
//         private void Start()
//         {
//             _deltaX = transform.position.x - _target.position.x;
//         }
//
//         private void Update()
//         {
//             var position = transform.position;
//             var targetPosition = new Vector3(position.x, _target.position.y, position.z); 
//             position = new Vector3(_target.position.x + _deltaX, position.y, position.z);
//             position = Vector3.Lerp(position, targetPosition + _offset, Time.deltaTime * _smooth);
//             transform.position = position;
//
//             if (!_isFinishPositionActive) return;
//             const int yDistance = 18;
//             const int duration = 10;
//             SetYOffset(yDistance,duration);
//         }
//
//         private void SetYOffset(float yDistance, float duration)
//         {
//             _offset = Vector3.MoveTowards(_offset, new Vector3(_offset.x, yDistance, _offset.z),
//                 duration * Time.deltaTime);
//         }