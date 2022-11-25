using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Enemy;
using ES3Types;
using Infrastructure.Logic;
using Props;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using Wizards;
using Wizard = Wizards.Wizard;

public class ArenaDisposer : MonoBehaviour
{
    private const string WIZARD = "Wizard";
    private const string ENEMY = "Enemy";
    private const string REWARD_POINT = "RewardPoint";

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _startFightSoundFx;
    
    private UIInventory _shopInterface;
    private RewardSystem _rewardSystem;
    private GameObject _levelFinishInterface;
    private WizardsSpawner _wizardsSpawner;
    private PlayerProgress _playerProgress;
    private List<GameObject> _wizards = new List<GameObject>();
    private List<GameObject> _enemies = new List<GameObject>();
    private Dictionary<int, List<string>> _wizardsInventory = new Dictionary<int, List<string>>();
    private CameraFollower _cameraFollower;

    public PlayerProgress PlayerProgress => _playerProgress;
    public event Action<bool> EndFight;

    private void OnEnable()
    {
        _rewardSystem = GetComponent<RewardSystem>();
        FindAllFighters();
        FindRewardPoint();
    }


    public void SetShopInterface(UIInventory inventory)
    {
        _shopInterface = inventory;
        _shopInterface.Fight += ActivateBattleState;
        _shopInterface.Fight += SaveSquadInventory;
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
        _cameraFollower = _wizardsSpawner.CameraFollower;
    }

    public void SaveSquadInventory()
    {
        for (int i = 0; i < _wizards.Count; i++)
        {
            AddWizardInventory(i, _wizards[i].GetComponent<InventoryFighter>().GetItemsID());
        }

        _playerProgress.SaveSquadItems(_wizardsInventory);
    }

    private void FindRewardPoint() =>
        _rewardSystem.GetInstantiatePoint(GameObject.FindGameObjectWithTag(REWARD_POINT).transform, this);

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
        if (_enemies.Count != 0)
        {
            _enemies.Remove(fighter);
        }

        if (_enemies.Count == 0)
        {
            EnterStateVictory();
            EndFight?.Invoke(true);
            RunToRewardChest();
            // _levelFinishInterface.SetActive(true);
            _playerProgress.GetReward();
        }
    }

    private void AddWizardInventory(int key, List<string> itemsID) =>
        _wizardsInventory.Add(key, itemsID);


    private void RemoveWizard(GameObject fighter)
    {
        if (_wizards.Count != 0)
        {
            _wizards.Remove(fighter);
            
            if(IsWizardStandardBearer(fighter))
                _cameraFollower.SetTarget(_wizards[0].transform);
        }

        if (_wizards.Count == 0)
        {
            _levelFinishInterface.SetActive(true);
            EndFight?.Invoke(false);
        }
    }

    private void RunToRewardChest()
    {
        var chestTransform = _rewardSystem.ChestTransform;
        foreach (var wizard in _wizards)
        {
            if (wizard.GetComponent<Wizard>().IsStandardBearer)
            {
                wizard.GetComponent<AgentMoveTo>().SetTarget(chestTransform);
            }
        }
    }
    
    private bool IsWizardStandardBearer(GameObject wizard)
    {
        var isWizardStandardBearer = wizard.GetComponent<Wizard>().IsStandardBearer;
        return isWizardStandardBearer;
    }

    private void FindAllFighters()
    {
        foreach (var wizard in GameObject.FindGameObjectsWithTag(WIZARD))
            _wizards.Add(wizard);
        foreach (var enemy in GameObject.FindGameObjectsWithTag(ENEMY))
            _enemies.Add(enemy);

        SubscribeToDeath();
    }

    private void ActivateBattleState()
    {
        PLaySoundFx(_startFightSoundFx);
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
    
    private void PLaySoundFx(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
            
        if (!_audioSource.isPlaying)
            _audioSource.Play();
    }

    private GameObject Fighter(GameObject fighter)
    {
        fighter.GetComponent<Idle>().SwitchOnStartFight();
        return fighter;
    }
}