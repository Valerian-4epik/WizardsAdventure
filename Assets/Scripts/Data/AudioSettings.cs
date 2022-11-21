using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class AudioSettings : MonoBehaviour
{
    private const string SOUND_FX = "SoundFx";
    private const string MUSIC = "Music";
    
    [SerializeField] private AudioMixerGroup _soundFXGroup;
    [SerializeField] private AudioMixerGroup _musicGroup;

    private static float GetVolume(float volume) => 
        Mathf.Lerp(-80, 0, volume);
    
    public void ChangeSoundVolume(float value) =>
        _soundFXGroup.audioMixer.SetFloat(SOUND_FX, GetVolume(value));

    public void ChangeMusicVolume(float value) => 
        _musicGroup.audioMixer.SetFloat(MUSIC,GetVolume(value));
}
