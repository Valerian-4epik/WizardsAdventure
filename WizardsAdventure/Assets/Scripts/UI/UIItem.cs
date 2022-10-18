using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private CanvasGroup _canvasGroup;
        private Canvas _mainCanvas;
        private RectTransform _rectTransform;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
            _mainCanvas = GetComponentInParent<Canvas>(); // чтобы добраться до канвас скейлера и переменная скейл фактор она определяет разницу в разрешении
            //в общем чтобы перетаскиваться обьект плавно.
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor; //можем хватать обьект 
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var slotTransform = _rectTransform.parent; // достаем родителя
            slotTransform.SetAsLastSibling(); //перемещаем его вниз в ерархии;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = Vector3.zero; //если мы отпустили обьект непонятно куда он вернется в свою изначальную ячейку
            _canvasGroup.blocksRaycasts = true;
        }
    }
}