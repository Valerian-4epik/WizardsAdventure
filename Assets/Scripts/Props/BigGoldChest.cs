using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using Wizards;

public class BigGoldChest : MonoBehaviour
{
    private string LAYER_NAME = "Default";
    
    [SerializeField] private GameObject _shineFx;
    [SerializeField] private GameObject _explosionCoinsFx;
    [SerializeField] private GameObject _apperChestFx;

    private Animation _animation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ChestOpener>())
        {
            gameObject.layer = LayerMask.NameToLayer(LAYER_NAME);
        }
    }

    private void OnEnable() => _animation = GetComponent<Animation>();
    // private void Start() =>_animation = GetComponent<Animation>();

    public void OpenChest() => _animation.Play();

    private void PlayApperFx() => _apperChestFx.GetComponent<ParticleSystem>().Play();
    private void PlayShineFx() => _shineFx.GetComponent<ParticleSystem>().Play();
    private void PlayExplosionCoinsFx() => _explosionCoinsFx.GetComponent<ParticleSystem>().Play();
}