using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UISlot : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData) //мы должны опередилить что перетаскивается с помощью хендлера и положить внутрь слота
        {
            var otherItemTransform = eventData.pointerDrag.transform; // поинтерДраг это то что мы тащим
            otherItemTransform.SetParent(transform);
            otherItemTransform.localPosition = Vector3.zero;
        }
    }
}