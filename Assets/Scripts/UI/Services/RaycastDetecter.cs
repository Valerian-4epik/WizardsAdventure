using System;
using Infrastructure.Logic;
using NodeCanvas.Tasks.Actions;
using UI;
using UI.Services;
using UnityEngine;
using Wizards;

public class RaycastDetecter : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;

    private float _clicked = 0;
    private float _clickTime = 0;
    private float _clickDelay = 0.5f;
    private UIInventory _shopInterface;
    private CameraFollower _cameraFollower;
    private Effector _effector;
    private AdditionalyAttackSpeedTrigger _additionalyAttackSpeedTrigger;
    
    private void Update()
    {
        ScaleUpWizard();
        
        if (DoubleClick())
        {
            if (GetWizardInventory() != null)
                GetWizardInventory().ReturnItems(_shopInterface);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (GetWizardShop() != null)
            {
                GetWizardShop().BuyWizard();
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (GetWizardForADS() != null)
            {
                GetWizardForADS().BuyWizard();
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (GetAdditionalyHpTrigger() != null)
            {
                GetAdditionalyHpTrigger().BuyHP();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (GetAdditionalAttackSpeedTrigger() != null)
                _additionalyAttackSpeedTrigger.Show();
        }
    }

    public void SetShopInterface(UIInventory shopInterface) => 
        _shopInterface = shopInterface;

    public void ActivateUIInventory()
    {
        _shopInterface.ShowInventory();
        _cameraFollower = FindObjectOfType<CameraFollower>();
        _cameraFollower.ActivateEnemyButton();
    }


    private bool DoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _clicked++;
            if (_clicked == 1) _clickTime = Time.time;
        }

        if (_clicked > 1 && Time.time - _clickTime < _clickDelay)
        {
            _clicked = 0;
            _clickTime = 0;
            return true;
        }

        if (_clicked > 2 || Time.time - _clickTime > 1) _clicked = 0;
        return false;
    }

    private InventoryFighter GetWizardInventory()
    {
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 1000, _targetMask))
            {
                if (hit.collider.TryGetComponent(out InventoryFighter inventory))
                {
                    return inventory;
                }
            }
        }

        return null;
    }

    private WizardForMoney GetWizardShop()
    {
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 1000, _targetMask))
            {
                if (hit.collider.TryGetComponent(out WizardForMoney wizardShop))
                {
                    return wizardShop;
                }
            }

            return null;
        }

        return null;
    }

    private WizardForADS GetWizardForADS()
    {
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 1000, _targetMask))
            {
                if (hit.collider.TryGetComponent(out WizardForADS wizardForAds))
                {
                    return wizardForAds;
                }
            }

            return null;
        }

        return null;
    }
    
    private AdditionalyHpForMoney GetAdditionalyHpTrigger()
    {
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 1000, _targetMask))
            {
                if (hit.collider.TryGetComponent(out AdditionalyHpForMoney trigger))
                {
                    return trigger;
                }
            }

            return null;
        }

        return null;
    }
    
    private AdditionalyAttackSpeedTrigger GetAdditionalAttackSpeedTrigger()
    {
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 1000, _targetMask))
            {
                if (hit.collider.TryGetComponent(out AdditionalyAttackSpeedTrigger trigger))
                {
                    _additionalyAttackSpeedTrigger = trigger;
                    return trigger;
                }
            }

            return null;
        }

        return null;
    }
    
    private void ScaleUpWizard()
    {
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 1000, _targetMask))
            {
                if (hit.collider.TryGetComponent(out Effector effector))
                {
                    _effector = effector;
                    
                    if(!_effector.ScaleChanged)
                        _effector.ScaleUp();
                }
                else
                {
                    if (_effector != null)
                    {
                        _effector.NormalizeScale();
                    }
                }
            }
            else
            {
                if (_effector != null)
                {
                    _effector.NormalizeScale();
                }
            }
        }
    }
}