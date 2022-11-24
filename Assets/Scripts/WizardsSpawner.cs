using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Infrastructure.Logic;
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
    private WizardForADS _wizardADSShop;
    private WizardPrice _wizardPrice;
    private CameraFollower _cameraFollower;

    public event Action<GameObject> SquadChanged;

    private void OnEnable()
    {
        _wizardPrice = new WizardPrice();
        AddAllInitPoints();
    }

    public void SetupPlayerProgress(PlayerProgress playerProgress)
    {
        _playerProgress = playerProgress;
        Spawn();
        SpawnWizardShop();
    }

    public void AddWizard()
    {
        if (GetEmptyInitPoint() != null && _playerProgress.LoadCurrentMoney() >= _wizardShop.Price)
        {
            _playerProgress.SaveCurrentMoney(_playerProgress.LoadCurrentMoney() - _wizardShop.Price);
            var wizard = InstantiateWizard();
            _playerProgress.PlayerWizardAmount++;
            _wizardShop.Price = _wizardPrice.GetPrice(_playerProgress.PlayerWizardAmount);
            SquadChanged?.Invoke(wizard);
        }
        else if (GetEmptyInitPoint() == null)
            _wizardShop.gameObject.SetActive(false);
        else
            Debug.Log("Недостаточно денег");//вот тут нужно добавить звук
    }

    public void AddWizardForADS()
    {
        if (GetEmptyInitPoint() != null)
        {
            var wizard = InstantiateWizard();
            _playerProgress.PlayerWizardAmount++;
            _wizardShop.Price = _wizardPrice.GetPrice(_playerProgress.PlayerWizardAmount);
            SquadChanged?.Invoke(wizard);
        }
    }

    public void SetCameraFollower(CameraFollower cameraFollower) => _cameraFollower = cameraFollower;
    
    private GameObject InstantiateWizard()
    {
        var transformLookAtCamera = Quaternion.Euler(0, 180, 0);
        var wizard = Instantiate(_wizard, GetEmptyInitPoint().transform.position, transformLookAtCamera);
        GetEmptyInitPoint().IsEmpty = false;
        return wizard;
    }

    private void AddAllInitPoints()
    {
        InitPoint[] array = GetComponentsInChildren<InitPoint>();
        _initPoints = array.ToList();
    }

    private void Spawn()
    {
        if (_playerProgress.GetPLayerWizardAmount() == 0)
        {
            SpawnStartSquad();
        }
        else
        {
            for (int i = 0; i < _playerProgress.GetPLayerWizardAmount(); i++)
            {
                
                var wizardStandardBearer = InstantiateWizard();
                if (i == BASE_AMOUNT_WIZARDS - 1)
                {
                    _cameraFollower.SetTarget(wizardStandardBearer.transform);
                }
            }
        }
    }

    private void SpawnStartSquad()
    {
        for (int i = 0; i < BASE_AMOUNT_WIZARDS; i++)
        {
            var wizardStandardBearer = InstantiateWizard();
            if (i == BASE_AMOUNT_WIZARDS - 1)
            {
                _cameraFollower.SetTarget(wizardStandardBearer.transform);
            }
        }

        _playerProgress.PlayerWizardAmount = BASE_AMOUNT_WIZARDS;
        _playerProgress.SavePlayerWizardsAmount();
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
        wizardForMoney.gameObject.transform.SetParent(transform);
        var wizardForADS = Instantiate(_wizardForViewingADS, _wizardForViewingADSPoint.position, Quaternion.identity);
        wizardForADS.gameObject.transform.SetParent(transform);
        SetupWizardShop(wizardForMoney, wizardForADS);
    }

    private void SetupWizardShop(GameObject wizardForMoney, GameObject wizardForADS)
    {
        _wizardShop = wizardForMoney.GetComponent<WizardForMoney>();
        _wizardShop.SetWizardSpawner(this, _wizardPrice.GetPrice(_playerProgress.PlayerWizardAmount));
        _wizardADSShop = wizardForADS.GetComponent<WizardForADS>();
        _wizardADSShop.SetupSpawner(this);
    }
}