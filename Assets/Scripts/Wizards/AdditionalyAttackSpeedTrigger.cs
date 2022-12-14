using System;
using System.Collections;
using Agava.YandexGames;
using Data;
using UnityEngine;
using UnityEngine.Audio;

public class AdditionalyAttackSpeedTrigger : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _successfulBuyWizard;
    [SerializeField] private AudioClip _unsuccessfulBuyWizard;
    [SerializeField] private BoxCollider _boxCollider;

    private WizardsSpawner _wizardsSpawner;
    private PlayerProgress _playerProgress;

    private float _additionalyAttackSpeed = 0.05f;

    public void SetWizardSpawner(WizardsSpawner wizardsSpawner)
    {
        _wizardsSpawner = wizardsSpawner;
        _playerProgress = _wizardsSpawner.PlayerProgress;
    }

    public void Show()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        BuyAttackSpeed();
        CloseButton();
#endif
        OnSwitchMusicVolume(false);
        VideoAd.Show(onRewardedCallback: BuyAttackSpeed, onCloseCallback: CloseButton, onErrorCallback: Return);
    }

    private void BuyAttackSpeed()
    {
        _playerProgress.AddCurrentAttackSpeed(_additionalyAttackSpeed);
    }

    private void Return(string value)
    {
        OnSwitchMusicVolume(true);
        Deactivate();
    }

    private void CloseButton()
    {
        OnSwitchMusicVolume(true);
        _boxCollider.enabled = false;
        _wizardsSpawner.InitialPoints.ForEach(point => point.PlayASGrade());
        StartCoroutine(PlaySoundFx(Deactivate));
    }

    private void Deactivate() => gameObject.SetActive(false);

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