using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Effector : MonoBehaviour
{
    private const float DURATION = 0.2f;

    [SerializeField] private Transform _point;
    [SerializeField] private GameObject _graderEffect;
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] private GameObject _armorDefenceFx;
    [SerializeField] private InventoryFighter _inventory;

    private Vector3 _localScale;
    private Vector3 _effectScale = new Vector3(1.2f, 1.2f, 1.2f);

    public bool ScaleChanged { get; } = false;

    private void Start()
    {
        _localScale = transform.localScale;
        if (_inventory != null)
        {
            _inventory.ArmorDressed += PlayGradeEffect;
            _inventory.WeaponDressed += PlayGradeEffect;
        }
    }

    public void ScaleUp() =>
        transform.DOScale((_effectScale), DURATION);

    public void NormalizeScale() =>
        transform.DOScale(_localScale, DURATION);

    public void PlayHitEffect()
    {
        var effectObject = InstantiateFx(_hitEffect, transform);
        effectObject.GetComponent<ParticleSystem>().Play();
    }

    public void PlayArmorDefenceEffect() => InstantiateFx(_armorDefenceFx, transform);

    private GameObject InstantiateFx(GameObject gameObject, Transform transform)
    {
        var objectFx = Instantiate(gameObject, transform.position, Quaternion.identity);
        return objectFx;
    }

    private void PlayGradeEffect(ItemInfo item)
    {
        Instantiate(_graderEffect, _point.position, Quaternion.identity);
        transform.DOScale((_effectScale), DURATION).OnComplete(() => transform.DOScale(_localScale, DURATION));
    }
}