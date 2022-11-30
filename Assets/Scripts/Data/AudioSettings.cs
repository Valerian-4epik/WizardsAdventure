using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    private const string SOUND_FX = "SoundFx";
    private const string MUSIC = "Music";

    [SerializeField] private AudioMixerGroup _soundFXGroup;
    [SerializeField] private AudioMixerGroup _musicGroup;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundFxSlider;
    [SerializeField] private AudioSource _soundFxForSoundSlider;

    private bool _isPlaying;

    private void Start()
    {
        ChangeMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0.5f));
        ChangeSoundVolume(PlayerPrefs.GetFloat("SoundFx", 0.5f));
        _settingsButton.onClick.AddListener(SetupSlidersValue);
        _soundFxSlider.onValueChanged.AddListener(PlaySoundFx);
    }

    public void ChangeMusicVolume(float value)
    {
        _musicGroup.audioMixer.SetFloat(MUSIC, GetVolume(value));
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void ChangeSoundVolume(float value)
    {
        _soundFXGroup.audioMixer.SetFloat(SOUND_FX, GetVolume(value));
        PlayerPrefs.SetFloat("SoundFx", value);
    }

    private void PlaySoundFx(float value)
    {
        if (!_soundFxForSoundSlider.isPlaying)
            _soundFxForSoundSlider.Play();
    }

    private void SetupSlidersValue()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        _soundFxSlider.value = PlayerPrefs.GetFloat("SoundFx", 0.5f);
    }

    private static float GetVolume(float volume) =>
        Mathf.Lerp(-80, 0, volume);
}