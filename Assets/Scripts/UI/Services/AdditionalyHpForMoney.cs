using Data;
using TMPro;
using UnityEngine;

namespace UI.Services
{
    public class AdditionalyHpForMoney : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _successfulBuyWizard;
        [SerializeField] private AudioClip _unsuccessfulBuyWizard;

        private WizardsSpawner _wizardsSpawner;
        private PlayerProgress _playerProgress;
        
        private int _additionalyHPValue = 10;
        private int _price = 500;

        public void SetWizardSpawner(WizardsSpawner wizardsSpawner)
        {
            _wizardsSpawner = wizardsSpawner;
            _playerProgress = _wizardsSpawner.PlayerProgress;
        }

        public void BuyHP()
        {
            if (_playerProgress.LoadCurrentMoney() >= _price)
            {
                _playerProgress.SaveCurrentMoney(_playerProgress.LoadCurrentMoney() - _price);
                _playerProgress.AddAdditionalHP(_additionalyHPValue);
                PLaySoundFx(true);
            }
            else
                PLaySoundFx(false);
        }

        private void PLaySoundFx(bool value)
        {
            if (value)
                _audioSource.clip = _successfulBuyWizard;
            else
                _audioSource.clip = _unsuccessfulBuyWizard;

            if (!_audioSource.isPlaying)
                _audioSource.Play();
        }
    }
}