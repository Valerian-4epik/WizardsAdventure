using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WizardForMoney : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _successfulBuyWizard;
    [SerializeField] private AudioClip _unsuccessfulBuyWizard;

    private WizardsSpawner _wizardsSpawner;
    private int _price;

    public int Price
    {
        get => _price;
        set
        {
            _price = value;
            Show();
        }
    }

    public void SetWizardSpawner(WizardsSpawner wizardsSpawner, int price)
    {
        _wizardsSpawner = wizardsSpawner;
        Price = price;
        Show();
    }

    public void BuyWizard() => _wizardsSpawner.AddWizard(PLaySoundFx, PLaySoundFx);

    private void PLaySoundFx(bool value)
    {
        if (value)
            _audioSource.clip = _successfulBuyWizard;
        else
            _audioSource.clip = _unsuccessfulBuyWizard;

        if (!_audioSource.isPlaying)
            _audioSource.Play();
    }

    private void Show() => _text.text = _price.ToString();
}