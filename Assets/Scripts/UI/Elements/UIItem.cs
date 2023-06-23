using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UI
{
    public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private AudioClip _onBeginDragSoundFx;
        [SerializeField] private AudioClip _dropSoundFx;

        private Camera _camera;
        private CanvasGroup _canvasGroup;
        private Canvas _mainCanvas;
        private RectTransform _rectTransform;
        private AudioSource _audioSource;

        public virtual void Start()
        {
            _camera = Camera.main;
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
            _mainCanvas = GetComponentInParent<Canvas>();
            _audioSource = GetComponent<AudioSource>();
        }

        public void OnDrag(PointerEventData eventData) =>
            _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            PlaySoundFx(_onBeginDragSoundFx);
            var slotTransform = _rectTransform.parent;
            slotTransform.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;
        }

        private void PlaySoundFx(AudioClip soundFx)
        {
            _audioSource.clip = soundFx;
            _audioSource.Play();
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = Vector3.zero;
            _canvasGroup.blocksRaycasts = true;
            PlaySoundFx(_dropSoundFx);

            if (_camera == null) return;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 1000, _targetMask))
            {
                if (hit.collider.TryGetComponent(out InventoryFighter inventory))
                {
                    inventory.SetWeapon(this);
                }
                else if (hit.collider.TryGetComponent(out SellSlot sellSlot))
                {
                    sellSlot.SellItem(this);
                }
            }
        }
    }
}