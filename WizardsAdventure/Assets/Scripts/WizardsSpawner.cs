using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

public class WizardsSpawner : MonoBehaviour
{
    private const int BASE_AMOUNT_WIZARDS = 2;

    [SerializeField] private List<InitPoint> _initPoints = new List<InitPoint>();
    [SerializeField] private GameObject _wizard;
    [SerializeField] private GameObject _wizardForViewingADS;
    [SerializeField] private GameObject _wizardForMoney;
    [SerializeField] private Transform _wizardForViewingADSPoint;
    [SerializeField] private Transform _wizardForMoneyPoint;

    private PlayerProgress _playerProgress;
    private WizardForMoney _wizardShop;

    public event Action<GameObject> SquadChanged;

    private void OnEnable()
    {
        AddAllInitPoints();
        SpawnWizardShop();
    }

    public void SetupPlayerProgress(PlayerProgress playerProgress)
    {
        _playerProgress = playerProgress;
        Spawn();
    }

    public void AddWizard()
    {
        if (GetEmptyInitPoint() != null)
        {
            var wizard = Instantiate(_wizard, GetEmptyInitPoint().transform.position, Quaternion.identity);
            _playerProgress.PlayerWizardsAmount++;
            ES3.Save("mySquad", _playerProgress.PlayerWizardsAmount, "Squad.es3");
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
        _playerProgress.PlayerWizardsAmount = ES3.Load("mySquad", "Squad.es3", _playerProgress.PlayerWizardsAmount);

        Debug.Log("Spawn");
        if (_playerProgress.PlayerWizardsAmount == 0)
        {
            SpawnStartSquad();
            ES3.Save("mySquad", _playerProgress.PlayerWizardsAmount, "Squad.es3");
        }
        else
        {
            for (int i = 0; i < _playerProgress.PlayerWizardsAmount; i++)
            {
                var wizard = Instantiate(_wizard, GetEmptyInitPoint().transform.position, Quaternion.identity);
                GetEmptyInitPoint().IsEmpty = false;
            }
        }
    }

    private void SpawnStartSquad()
    {
        for (int i = 0; i < BASE_AMOUNT_WIZARDS; i++)
        {
            var wizard = Instantiate(_wizard, GetEmptyInitPoint().transform.position, Quaternion.identity);
            _playerProgress.PlayerWizardsAmount = BASE_AMOUNT_WIZARDS;
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