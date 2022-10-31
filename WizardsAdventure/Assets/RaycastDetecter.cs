using UI;
using UnityEngine;

public class RaycastDetecter : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;

    private float _clicked = 0;
    private float _clickTime = 0;
    private float _clickDelay = 0.5f;
    private UIInventory _shopInterface;
    
    void Update()
    {
        if (DoubleClick())
        {
            if(GetWizardInventory() != null)
                GetWizardInventory().ReturnItems(_shopInterface);
        }
    }

    public void SetShopInterface(UIInventory shopInterface) => 
        _shopInterface = shopInterface;

    private bool DoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _clicked++;
            if (_clicked == 1) _clickTime = Time.time;
        }
        if (_clicked > 1 && Time.time - _clickTime < _clickDelay)
        {
            _clicked = 0;
            _clickTime = 0;
            return true;
        }
        if (_clicked > 2 || Time.time - _clickTime > 1) _clicked = 0;
        return false;
    }

    private InventoryFighter GetWizardInventory()
    {
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 1000, _targetMask))
            {
                if (hit.collider.TryGetComponent(out InventoryFighter inventory))
                {
                    return inventory;
                }
            }
        }

        return null;
    }
}