using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UISlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private LayerMask _targetMask;
            
        protected InventoryFighter inventoryFighter;

        public virtual void OnDrop(PointerEventData eventData)
        {
            var otherItemTransform = eventData.pointerDrag.transform;
            otherItemTransform.SetParent(transform);
            otherItemTransform.localPosition = Vector3.zero;
            
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, 1000, _targetMask))
                {
                    Debug.Log(hit.collider);
                    inventoryFighter = hit.collider.GetComponent<InventoryFighter>();
                }
            }
        }
    }
}