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
    [SerializeField] private InventoryFighter _inventory;

    private Vector3 _localScale;
    private Vector3 _effectScale = new Vector3(1.2f, 1.2f, 1.2f);

    public bool ScaleChanged { get; } = false;

    private void Start()
    {
        _localScale = transform.localScale;
        _inventory.ArmorDressed += PlayGradeEffect;
        _inventory.WeaponDressed += PlayGradeEffect;
    }

    public void ScaleUp() =>
        transform.DOScale((_effectScale), DURATION);
    
    private void PlayGradeEffect(ItemInfo item)
    {
        Instantiate(_graderEffect, _point.position, Quaternion.identity);
        transform.DOScale((_effectScale), DURATION).OnComplete(() => transform.DOScale(_localScale, DURATION));
        //Test test test Test test test Test test test Test test test  Test test test Test test test Test test test
        // StartCoroutine(ScaleUp());
    }

    public void NormalizeScale() =>
        transform.DOScale(_localScale, DURATION);
}