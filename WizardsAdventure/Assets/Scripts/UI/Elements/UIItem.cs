using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private LayerMask _targetMask;

        private CanvasGroup _canvasGroup;
        private Canvas _mainCanvas;
        private RectTransform _rectTransform;
        
        // protected Wizard inventoryFighter;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
            _mainCanvas = GetComponentInParent<Canvas>(); // чтобы добраться до канвас скейлера и переменная скейл фактор она определяет разницу в разрешении
            //в общем чтобы перетаскиваться обьект плавно.
        }

        public void OnDrag(PointerEventData eventData) => 
            _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            var slotTransform = _rectTransform.parent; // достаем родителя
            slotTransform.SetAsLastSibling(); //перемещаем его вниз в ерархии;
            _canvasGroup.blocksRaycasts = false;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = Vector3.zero;
            _canvasGroup.blocksRaycasts = true;
            
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, 1000, _targetMask))
                {
                    Debug.Log(hit.collider);
                    hit.collider.GetComponent<InventoryFighter>().SetWeapon(this);
                }
            }
        }
    }
}