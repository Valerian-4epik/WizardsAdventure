using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Infrastructure.Logic
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private float _smooth;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Quaternion _rotation;
        [SerializeField] private PlayableAsset _toEnemyTarget;
        [SerializeField] private PlayableAsset _toWizardTarget;
        [SerializeField] private Button _enemyButton;
        [SerializeField] private Button _homeButton;
        
        private PlayableDirector _playableDirector;
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
            _playableDirector = _cameraMain.gameObject.GetComponent<PlayableDirector>();
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

        public void ActivateEnemyButton()
        {
            var canvasGroup = _enemyButton.gameObject.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
        }

        public void ActivateHomeButton()
        {
            var canvasGroup = _homeButton.gameObject.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
        }

        public void DisableButtons()
        {
            _enemyButton.gameObject.SetActive(false);
            _homeButton.gameObject.SetActive(false);
        }

        public void ShowEnemyPosition()
        {
            _shopInterface.CanvasGroup.alpha = 0;
            _shopInterface.CanvasGroup.interactable = false;
            _shopInterface.CanvasGroup.blocksRaycasts = false;
            _playableDirector.playableAsset = _toEnemyTarget;
            _playableDirector.Play();
        }

        public void ShowWizardPosition()
        {
            _shopInterface.CanvasGroup.alpha = 1;
            _shopInterface.CanvasGroup.interactable = true;
            _shopInterface.CanvasGroup.blocksRaycasts = true;
            _playableDirector.playableAsset = _toWizardTarget;
            _playableDirector.Play();
        }
        
        private void ActivateTargetFollower()
        {
            _canFollowing = true;
            SetupRotate();
            DisableButtons();
        }

        private void SetupRotate() => _cameraMain.transform.DORotateQuaternion(_rotation, 2);
    }
}
