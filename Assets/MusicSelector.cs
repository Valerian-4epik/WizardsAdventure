using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicSelector : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _clips;

    private void OnEnable()
    {
        _audioSource.clip = GetRandomClips();
        _audioSource.Play();
    }

    private AudioClip GetRandomClips()
    {
        var number = Random.Range(0, _clips.Count);
        return _clips[number];
    }
}
