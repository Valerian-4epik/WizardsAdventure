using System;
using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Audio;

public class WizardForADS : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private BoxCollider _boxCollider;
    
    private WizardsSpawner _spawner;

    public void SetupSpawner(WizardsSpawner spawner) => _spawner = spawner;
    
    public void BuyWizard() => Show();

    private void Show()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        Reward();
        CloseButton();
#endif
        OnSwitchMusicVolume(false);
        VideoAd.Show(onRewardedCallback:Reward, onCloseCallback:CloseButton, onErrorCallback:Return);
    }
    
    private void Reward()
    {
        _spawner.AddWizardForADS();
    }

    private void Return(string value)
    {
        OnSwitchMusicVolume(true);
        DisabeleGameObject();
    }

    private void CloseButton()
    {
        OnSwitchMusicVolume(true);
        _boxCollider.enabled = false;
        StartCoroutine(PlaySoundFx(DisabeleGameObject));
    }

    private void DisabeleGameObject() => gameObject.SetActive(false);
    
    private IEnumerator PlaySoundFx(Action onCallBack = null)
    {
        var length = _audioSource.clip.length;
        
        if (!_audioSource.isPlaying)
            _audioSource.Play();

        yield return new WaitForSeconds(length);
        
        onCallBack?.Invoke();
    }
    
    private void OnSwitchMusicVolume(bool value)
    {
        if (value)
            _audioMixer.audioMixer.SetFloat("Master", 0);
        else
            _audioMixer.audioMixer.SetFloat("Master", -80);
    }
}