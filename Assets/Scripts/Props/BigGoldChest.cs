using System;
using System.Collections;
using UnityEngine;

public class BigGoldChest : MonoBehaviour
{
    private string LAYER_NAME = "Default";
    
    [SerializeField] private GameObject _shineFx;
    [SerializeField] private GameObject _explosionCoinsFx;
    [SerializeField] private GameObject _apperChestFx;

    private Animation _animation;
    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;
    private bool isOpen = false;

    public event Action<bool> ChestStateEnded;
    
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
        StartCoroutine(SpareСheck());
    }

    private IEnumerator SpareСheck()
    {
        yield return new WaitForSeconds(10f);
        if(!isOpen)
            OpenChest();
    }

    public void OpenChest()
    {
        isOpen = true;
        _animation.Play();
    }


    private void EndAnimation() => StartCoroutine(SlowLevelEnded());

    private IEnumerator SlowLevelEnded()
    {
        yield return new WaitForSeconds(1.5f);
        ChestStateEnded?.Invoke(true);
    }
    
    private void PlayApperFx() => _apperChestFx.GetComponent<ParticleSystem>().Play();

    private void PlayShineFx() => _shineFx.GetComponent<ParticleSystem>().Play();

    private void PlayExplosionCoinsFx() => _explosionCoinsFx.GetComponent<ParticleSystem>().Play();

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        OpenChest();
        _boxCollider.enabled = false;
    }
}