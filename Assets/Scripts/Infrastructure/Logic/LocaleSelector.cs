using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;


public class LocaleSelector : MonoBehaviour
{
    public void SetLocale(int localeIndex) =>
        StartCoroutine(ChangeLocale(localeIndex));

    private IEnumerator ChangeLocale(int localeIndex)
    {
        yield return StartCoroutine(LocalizationSettings.InitializationOperation);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];
    }
}