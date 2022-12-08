using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using Data;
using Unity.VisualScripting;
using UnityEngine;

public class AdditionalyAttackSpeedTrigger : MonoBehaviour
{
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
        VideoAd.Show(onRewardedCallback: BuyAttackSpeed, onCloseCallback: CloseButton, onErrorCallback: Return);
    }

    private void BuyAttackSpeed() =>
        _playerProgress.AddAdditionalAttackSpeed(_additionalyAttackSpeed);

    private void Return(string value)
    {
        Deactivate();
    }

    private void CloseButton()
    {
        _boxCollider.enabled = false;
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
}