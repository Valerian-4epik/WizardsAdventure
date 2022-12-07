using UnityEngine;
using UnityEngine.Localization;

namespace Tutorial.Scripts
{
    [CreateAssetMenu(fileName = "DialogInfo", menuName = " DialogInfo/DialogInfo")]
    public class DialogInfo : ScriptableObject
    {
        [SerializeField] private LocalizedString _helpHero;
        [SerializeField] private LocalizedString _money;
        [SerializeField] private LocalizedString _fighters;
        [SerializeField] private LocalizedString _buyWeapon;
        [SerializeField] private LocalizedString _buyTwoBranches;
        [SerializeField] private LocalizedString _mergeWeapon;
        [SerializeField] private LocalizedString _dragAndDropWeapon;
        [SerializeField] private LocalizedString _returnItems;
        [SerializeField] private LocalizedString _goodLuckHero;
        [SerializeField] private LocalizedString _pleseSaveUs;

        public LocalizedString HelpHero => _helpHero;
        public LocalizedString Money => _money;
        public LocalizedString Fighters => _fighters;
        public LocalizedString BuyWeapon => _buyWeapon;
        public LocalizedString BuyTwoBranches => _buyTwoBranches;
        public LocalizedString MergeWeapon => _mergeWeapon;
        public LocalizedString DragAndDropWeapon => _dragAndDropWeapon;
        public LocalizedString ReturnItems => _returnItems;
        public LocalizedString GoodLuckHero => _goodLuckHero;
        public LocalizedString PleseSaveUs => _pleseSaveUs;
    }
}