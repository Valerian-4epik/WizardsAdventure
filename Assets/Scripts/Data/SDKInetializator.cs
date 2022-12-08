using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class SDKInetializator : MonoBehaviour
{
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return StartCoroutine(YandexGamesSdk.Initialize());
        if(PlayerAccount.IsAuthorized && !PlayerAccount.HasPersonalProfileDataPermission)
            PlayerAccount.RequestPersonalProfileDataPermission();

        StartCoroutine(LoadDefaultLocale());
    }
    
    private IEnumerator LoadDefaultLocale()
    {
        while(!LocalizationSettings.InitializationOperation.IsDone)
            yield return null;
        
        var browserLang = YandexGamesSdk.Environment.i18n.lang;

        var localeIndex = browserLang switch
        {
            "en" => 0,
            "ru" => 1,
            "tr" => 2,
            _ => 0
        };
            
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];
    }
}
