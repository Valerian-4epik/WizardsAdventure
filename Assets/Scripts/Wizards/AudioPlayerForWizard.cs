using System;
using UnityEngine;

namespace Wizards
{
    public class AudioPlayerForWizard : MonoBehaviour
    {
        [SerializeField] private AudioClip _rejoicesTakeWeaponSound;
        [SerializeField] private AudioClip _attackSound;
        [SerializeField] private AudioClip _hitSound;
        [SerializeField] private AudioClip _hitArmorSound;
        
        private AudioSource _audioSource;
        
        private void Start() => _audioSource = gameObject.GetComponent<AudioSource>();

        public void PlayRejoicedEmotion() => PlaySound(_rejoicesTakeWeaponSound);
        public void PlayAttackSoundWithoutWeapon() => PlaySound(_attackSound);
        public void PLayHitSound() => PlaySound(_hitSound);
        public void PLayHitArmorSound() => PlaySound(_hitArmorSound);
        
        private void PlaySound(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }
    }
}
