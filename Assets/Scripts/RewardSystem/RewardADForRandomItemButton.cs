using Agava.YandexGames;
using UI;
using UnityEngine;
using UnityEngine.Audio;

public class RewardADForRandomItemButton : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private UIInventory _inventory;
    [SerializeField] private RandomItemADS _randomItemADS;

    public void Show()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        Reward();
        CloseButton();
#endif
        OnSwitchMusicVolume(false);
        VideoAd.Show(onRewardedCallback: Reward, onCloseCallback: CloseButton, onErrorCallback: ErrorReturn);
    }

    private void Reward()
    {
        _inventory.SetupItem(_randomItemADS.Item);
    }

    private void ErrorReturn(string value)
    {
        OnSwitchMusicVolume(true);
        return;
    }

    private void CloseButton()
    {
        OnSwitchMusicVolume(true);
        _randomItemADS.BlockButton();
    }
    
    private void OnSwitchMusicVolume(bool value)
    {
        if (value)
            _audioMixer.audioMixer.SetFloat("Master", 0);
        else
            _audioMixer.audioMixer.SetFloat("Master", -80);
    }
}