using UI;
using UnityEngine;

public class TutorialLogic : MonoBehaviour
{
    private UIInventory _inventoryItem;
    private RaycastDetecter _raycastDetecter;
    private ArenaDisposer _arenaDisposer;

    public void FindAllObjects()
    {
        Debug.Log("Ищем обьекты");
        _inventoryItem = FindObjectOfType<UIInventory>();
        _raycastDetecter = FindObjectOfType<RaycastDetecter>();
        _arenaDisposer = FindObjectOfType<ArenaDisposer>();
    }

    public void ActivateInventory()
    {
        Debug.Log("Активируем Инвентарь");
        _raycastDetecter.ActivateUIInventory();
    }

    public void DisableAllObject()
    {
        _raycastDetecter.enabled = true;
        _arenaDisposer.DisableRaycaster();
    }
}
