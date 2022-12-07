using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Infrastructure.Logic;
using UI.Services;
using Unity.VisualScripting;
using UnityEngine;
using Wizard = Wizards.Wizard;

public class WizardsSpawner : MonoBehaviour
{
    private const int BASE_AMOUNT_WIZARDS = 2;

    [SerializeField] private List<InitPoint> _initPoints = new List<InitPoint>();
    [SerializeField] private GameObject _wizard;
    [SerializeField] private GameObject _wizardForViewingADS;
    [SerializeField] private GameObject _wizardForMoney;
    [SerializeField] private GameObject _additionalyHP;
    [SerializeField] private GameObject _additionalyAttackSpeed;
    [SerializeField] private Transform _wizardForViewingADSPoint;
    [SerializeField] private Transform _wizardForMoneyPoint;

    private PlayerProgress _playerProgress;
    private WizardForMoney _wizardShop;
    private WizardForADS _wizardADSShop;
    private WizardPrice _wizardPrice;
    
    private CameraFollower _cameraFollower;

    public CameraFollower CameraFollower => _cameraFollower;
    public PlayerProgress PlayerProgress => _playerProgress;
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

    public void AddWizard(Action<bool> onCompleteCallBack = null, Action<bool> onErrorCallBack = null)
    {
        if (GetEmptyInitPoint() != null && _playerProgress.LoadCurrentMoney() >= _wizardShop.Price)
        {
            onCompleteCallBack?.Invoke(true);
            _playerProgress.SaveCurrentMoney(_playerProgress.LoadCurrentMoney() - _wizardShop.Price);
            // _playerProgress.SaveCurrentMoney(_playerProgress.LoadCurrentMoney() - _wizardShop.Price);
            var wizard = InstantiateWizard();
            _playerProgress.PlayerWizardAmount++;
            _wizardShop.Price = _wizardPrice.GetPrice(_playerProgress.PlayerWizardAmount);
            SquadChanged?.Invoke(wizard);
        }
        if (GetEmptyInitPoint() == null)
        {
            _wizardShop.gameObject.SetActive(false);
            _wizardForViewingADS.gameObject.SetActive(false);
            CreateAdditionalyHpTrigger();
            CreateAdditionalyAttackSpeedTrigger();
        }
        else
            onErrorCallBack?.Invoke(false);
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
                    wizardStandardBearer.GetComponent<Wizard>().IsStandardBearer = true;
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
                wizardStandardBearer.GetComponent<Wizard>().IsStandardBearer = true;
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
        if (_playerProgress.PlayerWizardAmount < 10)
        {
            var wizardForMoney = Instantiate(_wizardForMoney, _wizardForMoneyPoint.position, Quaternion.identity);
            wizardForMoney.gameObject.transform.SetParent(transform);
            var wizardForADS = Instantiate(_wizardForViewingADS, _wizardForViewingADSPoint.position,
                Quaternion.identity);
            wizardForADS.gameObject.transform.SetParent(transform);
            SetupWizardShop(wizardForMoney, wizardForADS);
        }
        else
        {
            CreateAdditionalyHpTrigger();
            CreateAdditionalyAttackSpeedTrigger();
        }

    }

    private void SetupWizardShop(GameObject wizardForMoney, GameObject wizardForADS)
    {
        _wizardShop = wizardForMoney.GetComponent<WizardForMoney>();
        _wizardShop.SetWizardSpawner(this, _wizardPrice.GetPrice(_playerProgress.PlayerWizardAmount));
        _wizardADSShop = wizardForADS.GetComponent<WizardForADS>();
        _wizardADSShop.SetupSpawner(this);
    }
    
    private void CreateAdditionalyAttackSpeedTrigger()
    {
        var additionalyAttackSpeedTrigger = Instantiate(_additionalyAttackSpeed, _wizardForViewingADSPoint.position,
            Quaternion.identity);
        additionalyAttackSpeedTrigger.gameObject.transform.SetParent(transform);
        additionalyAttackSpeedTrigger.GetComponent<AdditionalyAttackSpeedTrigger>().SetWizardSpawner(this);
    }

    private void CreateAdditionalyHpTrigger()
    {
        var additionalyHpTrigger = Instantiate(_additionalyHP, _wizardForMoneyPoint.position, Quaternion.identity);
        additionalyHpTrigger.gameObject.transform.SetParent(transform);
        additionalyHpTrigger.GetComponent<AdditionalyHpForMoney>().SetWizardSpawner(this);
    }
}