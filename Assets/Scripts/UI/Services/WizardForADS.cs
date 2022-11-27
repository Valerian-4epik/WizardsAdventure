using System;
using System.Collections;
using Agava.YandexGames;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class WizardForADS : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    
    private WizardsSpawner _spawner;

    public void SetupSpawner(WizardsSpawner spawner) => _spawner = spawner;
    
    public void BuyWizard() => Show();

    private void Show()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        Reward();
        CloseButton();
#endif
        VideoAd.Show(onRewardedCallback:Reward, onCloseCallback:CloseButton, onErrorCallback:Return);
    }
    
    private void Reward() => 
        _spawner.AddWizardForADS();

    private void Return(string value)
    {
        return;
    }

    private void CloseButton() => StartCoroutine(PlaySoundFx(Deactivate));
    
    private void Deactivate() => gameObject.SetActive(false);
    
    private IEnumerator PlaySoundFx(Action onCallBack = null)
    {
        var length = _audioSource.clip.length;
        
        if (!_audioSource.isPlaying)
            _audioSource.Play();

        yield return new WaitForSeconds(length);
        
        onCallBack?.Invoke();
    }
}