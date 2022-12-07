using Agava.YandexGames;
using Data;
using UnityEngine;

public class AdditionalyAttackSpeedTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _successfulBuyWizard;
    [SerializeField] private AudioClip _unsuccessfulBuyWizard;

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
        PLaySoundFx();
#endif
        VideoAd.Show(onRewardedCallback: BuyAttackSpeed, onCloseCallback: PLaySoundFx, onErrorCallback: Return);
    }

    private void BuyAttackSpeed() =>
        _playerProgress.AddAdditionalAttackSpeed(_additionalyAttackSpeed);

    private void Return(string value)
    {
        return;
    }

    private void PLaySoundFx()
    {
        _audioSource.clip = _successfulBuyWizard;

        if (!_audioSource.isPlaying)
            _audioSource.Play();
    }
}