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
    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ChestOpener>())
        {
            _rigidbody.isKinematic = true;
            gameObject.layer = LayerMask.NameToLayer(LAYER_NAME);
            
            StartCoroutine(Delay());
        }
    }

    private void OnEnable()
    {
        _animation = GetComponent<Animation>();
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }
    // private void Start() =>_animation = GetComponent<Animation>();

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        OpenChest();
        _boxCollider.enabled = false;
    }
    public void OpenChest() => _animation.Play();

    private void PlayApperFx() => _apperChestFx.GetComponent<ParticleSystem>().Play();
    private void PlayShineFx() => _shineFx.GetComponent<ParticleSystem>().Play();
    private void PlayExplosionCoinsFx() => _explosionCoinsFx.GetComponent<ParticleSystem>().Play();
}