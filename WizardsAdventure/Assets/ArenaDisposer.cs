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
    private List<GameObject> _wizards = new List<GameObject>();
    private List<GameObject> _enemies = new List<GameObject>();

    private void OnEnable()
    {
        FindAllFighters();
    }

    public void SetShopInterface(UIInventory inventory)
    {
        _shopInterface = inventory;
        _shopInterface.Fight += ActiveBattleState;
        
    }

    private void SubscribeToDeath()
    {
        foreach (var enemy in _enemies)
        {
            enemy.GetComponent<Death>().Happened += RemoveFighter;
        }
    }

    private void RemoveFighter(GameObject fighter) => 
        _enemies.Remove(fighter);

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