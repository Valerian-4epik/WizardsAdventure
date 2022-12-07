using Data;
using UI;
using UnityEngine;

public class SellSlot : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private PlayerProgress _playerProgress;

    public void SetPlayerProgress(PlayerProgress playerProgress) => _playerProgress = playerProgress;
    
    public void SellItem(UIItem uiItem)
    {
        var item = uiItem as UIInventoryItem;
        _playerProgress.SaveCurrentMoney(_playerProgress.LoadCurrentMoney() + item.Item.Price);
        Refresh(item);
        _audioSource.Play();
    }
    
    private void Refresh(UIInventoryItem item) =>
        item.GetComponentInParent<UIInventorySlot>().Refresh();
}
