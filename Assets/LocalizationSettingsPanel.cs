using System;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationSettingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject _checkEng;
    [SerializeField] private GameObject _checkRus;
    [SerializeField] private GameObject _checkTurk;

    private void OnEnable()
    {
        GetLanguage();
    }

    private void GetLanguage()
    {
        var localeIdentifier = LocalizationSettings.SelectedLocale.Identifier.Code;
        Debug.Log(localeIdentifier);

        if (localeIdentifier == "ru")
            _checkRus.SetActive(true);
        if (localeIdentifier == "en")
            _checkEng.SetActive(true);
        if (localeIdentifier == "tr")
            _checkTurk.SetActive(true);
    }
}
