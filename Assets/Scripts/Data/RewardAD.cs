using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class RewardAD : MonoBehaviour
{
    [SerializeField] private UIInventory _inventory;
    [SerializeField] private RandomItemADS _randomItemADS;
    
    public void Show()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        Reward();
        CloseButton();
#endif
        VideoAd.Show(onRewardedCallback:Reward, onCloseCallback:CloseButton, onErrorCallback:null);
    }

    private void Reward() => 
        _inventory.SetupItem(_randomItemADS.Item);

    private void CloseButton() => _randomItemADS.BlockButton();
}
