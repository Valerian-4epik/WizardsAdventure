using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WizardsSpawner : MonoBehaviour
{
    [SerializeField] private List<InitPoint> _initPoints = new List<InitPoint>();
    [SerializeField] private GameObject _wizard;
    [SerializeField] private GameObject _wizardForViewingADS;
    [SerializeField] private GameObject _wizardForMoney;
    [SerializeField] private Transform _wizardForViewingADSPoint;
    [SerializeField] private Transform _wizardForMoneyPoint;

    private PlayerStats _player;
    private WizardForMoney _wizardShop;

    public event Action<GameObject> SquadChanged;
        
    private void OnEnable()
    {
        _player = new PlayerStats(3);
        AddAllInitPoints();
        Spawn();
        SpawnWizardShop();
    }

    public void AddWizard()
    {
        if (GetEmptyInitPoint() != null)
        {
            var wizard = Instantiate(_wizard, GetEmptyInitPoint().transform.position, Quaternion.identity);
            GetEmptyInitPoint().IsEmpty = false;
            SquadChanged?.Invoke(wizard);
        }
        else
            _wizardShop.gameObject.SetActive(false);
    }

    private void AddAllInitPoints()
    {
        InitPoint[] array = GetComponentsInChildren<InitPoint>();
        _initPoints = array.ToList();
    }

    private void Spawn()
    {
        for (int i = 0; i < _player.AmountWizards; i++)
        {
            var wizard = Instantiate(_wizard, GetEmptyInitPoint().transform.position, Quaternion.identity);
            GetEmptyInitPoint().IsEmpty = false;
        }
    }

    private InitPoint GetEmptyInitPoint()
    {
        foreach (var initPoint in _initPoints)
        {
            if (initPoint.IsEmpty)
                return initPoint;
        }

        return null;
    }

    private void SpawnWizardShop()
    {
        var wizardForMoney = Instantiate(_wizardForMoney, _wizardForMoneyPoint.position, Quaternion.identity);
        var wizardForADS = Instantiate(_wizardForViewingADS, _wizardForViewingADSPoint.position, Quaternion.identity);

        SetupWizardShop(wizardForMoney);
    }

    private void SetupWizardShop(GameObject wizardForMoney)
    {
        _wizardShop = wizardForMoney.GetComponent<WizardForMoney>();
        _wizardShop.SetWizardSpawner(this);
    }
}