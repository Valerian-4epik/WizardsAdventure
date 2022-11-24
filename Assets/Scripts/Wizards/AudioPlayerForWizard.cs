using System;
using UnityEngine;

namespace Wizards
{
    public class AudioPlayerForWizard : MonoBehaviour
    {
        [SerializeField] private AudioClip _rejoicesTakeWeaponSound; 
        
        private AudioSource _audioSource;
        
        private void Start() => _audioSource = gameObject.GetComponent<AudioSource>();

        public void PlayRejoicedEmotion() => PlaySound(_rejoicesTakeWeaponSound);

        private void PlaySound(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }
    }
}
