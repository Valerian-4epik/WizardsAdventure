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
    private List<GameObject> _enemy = new List<GameObject>();

    private void OnEnable()
    {
        FindAllFighters();
    }

    public void SetShopInterface(UIInventory inventory) =>
        _shopInterface = inventory;
    
    private void FindAllFighters()
    {
        foreach (var wizard in GameObject.FindGameObjectsWithTag(Wizard))
            _wizards.Add(wizard);
        foreach (var enemy in GameObject.FindGameObjectsWithTag(Enemy))
            _enemy.Add(enemy);
    }

    private void ActiveBattleState(List<GameObject> squad1, List<GameObject> squad2)
    {
        var activeFighters = squad1.Concat(squad2);
        var allIdleComponents = from fighter in activeFighters where Fighter(fighter) select fighter;
    }

    private GameObject Fighter(GameObject fighter)
    { 
       fighter.GetComponent<Idle>().SwitchOnStartFight();
       return fighter;
    }
}