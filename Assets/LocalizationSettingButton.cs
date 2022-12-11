using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalizationSettingButton : MonoBehaviour
{
    [SerializeField] private Image _flag;
    [SerializeField] private Sprite _ruFlag;
    [SerializeField] private Sprite _enFlag;
    [SerializeField] private Sprite _trFlag;

    private void OnEnable()
    {
        GetLanguage();
    }

    private void GetLanguage()
    {
        var localeIdentifier = LocalizationSettings.SelectedLocale.Identifier.Code;
        Debug.Log(localeIdentifier);

        if (localeIdentifier == "ru")
            _flag.sprite = _ruFlag;
        if (localeIdentifier == "en")
            _flag.sprite = _enFlag;
        if (localeIdentifier == "tr")
            _flag.sprite = _trFlag;
    }
}
