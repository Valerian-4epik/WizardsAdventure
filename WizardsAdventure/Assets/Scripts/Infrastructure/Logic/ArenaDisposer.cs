using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Enemy;
using UI;
using UnityEngine;
using Wizards;

public class ArenaDisposer : MonoBehaviour
{
    private const string Wizard = "Wizard";
    private const string Enemy = "Enemy";

    private UIInventory _shopInterface;
    private GameObject _levelFinishInterface;
    private WizardsSpawner _wizardsSpawner;
    private PlayerProgress _playerProgress;
    private List<GameObject> _wizards = new List<GameObject>();
    private List<GameObject> _enemies = new List<GameObject>();
    private Dictionary<int, List<string>> _wizardsInventory = new Dictionary<int, List<string>>();

    public event Action<bool> EndFight;

    private void OnEnable()
    {
        FindAllFighters();
    }

    private void GiveItems()
    {
        for (int i = 0; i < _wizards.Count; i++)
        {
            if (_playerProgress.LoadSquadItems() != null)
            {
                for (int j = 0; j < _playerProgress.LoadSquadItems().Count; j++)
                {
                    if (i == j)
                    {
                        _wizards[i].GetComponent<InventoryFighter>().SetWeapon(_playerProgress.LoadSquadItems()[j]);
                    }
                }
            }
        }
    }

    public void SetShopInterface(UIInventory inventory)
    {
        _shopInterface = inventory;
        _shopInterface.Fight += ActivateBattleState;
    }

    public void SetLevelFinishInterface(GameObject finishInterface)
    {
        _levelFinishInterface = finishInterface;
        _levelFinishInterface.GetComponent<LevelFinishInterface>().SubscribeToEndFight(this);
    }

    public void SetPlayerProgress(PlayerProgress progress)
    {
        _playerProgress = progress;
        GiveItems();
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

    private void EnterStateVictory()
    {
        foreach (var wizard in _wizards)
        {
            wizard.GetComponent<WizardAnimator>().PlayVictory();
        }
    }

    private void RemoveEnemy(GameObject fighter)
    {
        _enemies.Remove(fighter);

        if (_enemies.Count == 0)
        {
            _levelFinishInterface.SetActive(true);
            
            for (int i = 0; i < _wizards.Count; i++)
            {
                AddWizardInventory(i, _wizards[i].GetComponent<InventoryFighter>().GetItemsID());
            }

            _playerProgress.SaveSquadItems(_wizardsInventory);
            _playerProgress.GetReward();
            Debug.Log("Victory");
            EnterStateVictory();
            EndFight?.Invoke(true);
        }
    }

    private void AddWizardInventory(int key, List<string> itemsID) =>
        _wizardsInventory.Add(key, itemsID);


    private void RemoveWizard(GameObject fighter)
    {
        _wizards.Remove(fighter);
        _playerProgress.PlayerWizardsAmount = _wizards.Count;
        ES3.Save("mySquad", _playerProgress.PlayerWizardsAmount, "Squad.es3");

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

    private void ActivateBattleState()
    {
        var activeFighters = _wizards.Concat(_enemies);

        foreach (var fighter in activeFighters)
        {
            fighter.GetComponent<Idle>().SwitchOnStartFight();
        }

        DisableWizardsSpawner();
        DisableShopInterface();
    }

    public void Activate() //test
    {
        var activeFighters = _wizards.Concat(_enemies);

        foreach (var fighter in activeFighters)
        {
            fighter.GetComponent<Idle>().SwitchOnStartFight();
        }

        // DisableShopInterface();
    }

    private void DisableWizardsSpawner() => _wizardsSpawner.gameObject.SetActive(false);


    private void DisableShopInterface()
    {
        _shopInterface.Fight -= ActivateBattleState;
        _shopInterface.gameObject.SetActive(false);
    }

    private GameObject Fighter(GameObject fighter)
    {
        fighter.GetComponent<Idle>().SwitchOnStartFight();
        return fighter;
    }
}