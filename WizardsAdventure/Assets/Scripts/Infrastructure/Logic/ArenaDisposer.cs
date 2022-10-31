using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Enemy;
using Infrastructure;
using UI;
using UnityEngine;

public class ArenaDisposer : MonoBehaviour
{
    private const string Wizard = "Wizard";
    private const string Enemy = "Enemy";

    private UIInventory _shopInterface;
    private GameObject _levelFinishInterface;
    private WizardsSpawner _wizardsSpawner;
    private List<GameObject> _wizards = new List<GameObject>();
    private List<GameObject> _enemies = new List<GameObject>();

    public event Action<bool> EndFight;
    
    
    private void OnEnable()
    {
        FindAllFighters();
    }

    public void SetShopInterface(UIInventory inventory)
    {
        _shopInterface = inventory;
        _shopInterface.Fight += ActiveBattleState;
    }

    public void SetLevelFinishInterface(GameObject finishInterface)
    {
        _levelFinishInterface = finishInterface;
        _levelFinishInterface.GetComponent<LevelFinishInterface>().SubscribeToEndFight(this);
    }

    public void SetWizardSpawner(GameObject wizardSpawner)
    {
        _wizardsSpawner = wizardSpawner.GetComponent<WizardsSpawner>();
        _wizardsSpawner.SquadChanged += AddWizard;
    }

    private void SubscribeToDeath()
    {
        foreach (var enemy in _enemies)
        {
            enemy.GetComponent<Death>().Happened += RemoveEnemy;
        }

        foreach (var wizard in _wizards)
        {
            wizard.GetComponent<Death>().Happened += RemoveWizard;
        }
    }

    private void AddWizard(GameObject wizard)
    {
        _wizards.Add(wizard);
        SubscribeToDeath(wizard);
    }
    
    private void SubscribeToDeath(GameObject gameObject) => 
        gameObject.GetComponent<Death>().Happened += RemoveWizard;

    private void RemoveEnemy(GameObject fighter)
    {
        _enemies.Remove(fighter);

        if (_enemies.Count == 0)
        {
            _levelFinishInterface.SetActive(true);
            EndFight?.Invoke(true);
        }
    }

    private void RemoveWizard(GameObject fighter)
    {
        _wizards.Remove(fighter);

        if (_wizards.Count == 0)
        {
            _levelFinishInterface.SetActive(true);
            EndFight?.Invoke(false);
        }
    }

    private void FindAllFighters()
    {
        foreach (var wizard in GameObject.FindGameObjectsWithTag(Wizard))
            _wizards.Add(wizard);
        foreach (var enemy in GameObject.FindGameObjectsWithTag(Enemy))
            _enemies.Add(enemy);
        
        SubscribeToDeath();
    }

    private void ActiveBattleState()
    {
        var activeFighters = _wizards.Concat(_enemies);
        
        foreach (var fighter in activeFighters)
        {
            fighter.GetComponent<Idle>().SwitchOnStartFight();
        }
        DisableShopInterface();
    }

    private void DisableShopInterface()
    {
        _shopInterface.Fight -= ActiveBattleState;
        _shopInterface.gameObject.SetActive(false);
    }

    private GameObject Fighter(GameObject fighter)
    { 
       fighter.GetComponent<Idle>().SwitchOnStartFight();
       return fighter;
    }
}