using Agava.YandexGames;
using UI;
using UnityEngine;

public class RewardADForRandomItemButton : MonoBehaviour
{
    [SerializeField] private UIInventory _inventory;
    [SerializeField] private RandomItemADS _randomItemADS;
    
    public void Show()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        Reward();
        CloseButton();
#endif
        VideoAd.Show(onRewardedCallback:Reward, onCloseCallback:CloseButton, onErrorCallback:ErrorReturn);
    }

    private void Reward() =>
        _inventory.SetupItem(_randomItemADS.Item);

    private void ErrorReturn(string value)
    {
        return;
    }

    private void CloseButton() => _randomItemADS.BlockButton();
}