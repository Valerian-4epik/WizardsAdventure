 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGoldChest : MonoBehaviour
{
    [SerializeField] private GameObject _shineFx;
    [SerializeField] private GameObject _explosionCoinsFx;
    [SerializeField] private GameObject _apperChestFx;

    private Animation _animation;

    private void OnEnable() =>_animation = GetComponent<Animation>();
    // private void Start() =>_animation = GetComponent<Animation>();

    public void OpenChest() => _animation.Play();
    
    private void PlayApperFx() => _apperChestFx.GetComponent<ParticleSystem>().Play();
    private void PlayShineFx() => _shineFx.GetComponent<ParticleSystem>().Play();
    private void PlayExplosionCoinsFx() => _explosionCoinsFx.GetComponent<ParticleSystem>().Play();
}
