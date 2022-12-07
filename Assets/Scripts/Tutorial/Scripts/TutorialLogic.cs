using System.Collections.Generic;
using Data;
using ES3Types;
using Infrastructure.Logic;
using UI;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

namespace Tutorial.Scripts
{
    public class TutorialLogic : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _playableDirector;
        [SerializeField] private PlayableAsset _mergeBranch;
        [SerializeField] private PlayableAsset _returnItems;
        [SerializeField] private PlayableAsset _goodluckHero;

        private PlayerProgress _playerProgress;
        private LevelFinishInterface _levelFinishInterface;
        private Button _branchButton;
        private WizardsSpawner _wizardsSpawner;
        private UIInventory _inventory;
        private RaycastDetecter _raycastDetecter;
        private ArenaDisposer _arenaDisposer;
        private CameraFollower _cameraFollower;
        private BoxCollider2D _trigger;
        private int _amountButtonClick;
        private List<InventoryFighter> _inventoryFighters = new List<InventoryFighter>();

        public void FindAllObjects()
        {
            Debug.Log("Ищем обьекты");
            _inventory = FindObjectOfType<UIInventory>();
            _raycastDetecter = FindObjectOfType<RaycastDetecter>();
            _arenaDisposer = FindObjectOfType<ArenaDisposer>();
            _playerProgress = FindObjectOfType<PlayerProgress>();
            _wizardsSpawner = FindObjectOfType<WizardsSpawner>();
            _cameraFollower = FindObjectOfType<CameraFollower>();
        }

        public void ActivateInventory()
        {
            Debug.Log("Активируем Инвентарь");
            _raycastDetecter.ActivateUIInventory();
            _inventory.CanvasGroup.interactable = false;
            _branchButton = _inventory.BranchButton;
            _branchButton.onClick.AddListener(AddAmount);
        }

        public void DisableAllObject()
        {
            _cameraFollower.DisableButtons();
            _arenaDisposer.DisableRaycaster();
            _wizardsSpawner.DisableWizardShop();
        }

        public void ActivateBranchButton()
        {
            _inventory.CanvasGroup.interactable = true;
            _inventory.ActivateBranchButtonForTutor();
            _raycastDetecter.enabled = true;
        }

        public void ActivateTrigger()
        {
            _trigger.enabled = true;
        }

        public void SubscribToWeaponChanged()
        {
            var wizards = _arenaDisposer.Wizards;
            foreach (var wizard in wizards)
            {
                wizard.GetComponent<InventoryFighter>().WeaponDressed += NextReturnItemState;
            }
        }
        
        public void SubscribToWeaponReturn()
        {
            var wizards = _arenaDisposer.Wizards;
            foreach (var wizard in wizards)
            {
                wizard.GetComponent<InventoryFighter>().WeaponDressed += NextGoodluckHeroState;
            }
        }

        public void FinishTutor()
        {
            _playerProgress.SaveTutorialStartState(false);
            _arenaDisposer.ActivateFinishInterface(false);
            _levelFinishInterface = FindObjectOfType<LevelFinishInterface>();
            _levelFinishInterface.gameObject.SetActive(true);
            _levelFinishInterface.GoNextLevel("FinishTutorial");
        }
        
        private void AddAmount()
        {
            _amountButtonClick++;
            if (_amountButtonClick >= 2)
            {
                _inventory.BranchButton.interactable = false;
                ChangePlayableTrack(_mergeBranch);
            }
        }

        private void ChangePlayableTrack(PlayableAsset playableTrack)
        {
            _playableDirector.playableAsset = playableTrack;
            _playableDirector.Play();
        }

        private void NextReturnItemState(ItemInfo item)
        {
            var wizards = _arenaDisposer.Wizards;
            foreach (var wizard in wizards)
            {
                wizard.GetComponent<InventoryFighter>().WeaponDressed -= NextReturnItemState;
            }
            ChangePlayableTrack(_returnItems);
        }

        private void NextGoodluckHeroState(ItemInfo item) => ChangePlayableTrack(_goodluckHero);
    }
}
