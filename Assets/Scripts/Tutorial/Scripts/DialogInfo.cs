using UnityEngine;

namespace Tutorial.Scripts
{
    [CreateAssetMenu(fileName = "DialogInfo", menuName = " DialogInfo/DialogInfo")]
    public class DialogInfo : ScriptableObject
    {
        [SerializeField] private string _helpHero;
        [SerializeField] private string _money;
        [SerializeField] private string _fighters;
        [SerializeField] private string _buyWeapon;
        [SerializeField] private string _buyTwoBranches;
        [SerializeField] private string _mergeWeapon;
        [SerializeField] private string _dragAndDropWeapon;
        [SerializeField] private string _returnItems;
        [SerializeField] private string _goodLuckHero;
        [SerializeField] private string _pleseSaveUs;

        public string HelpHero => _helpHero;
        public string Money => _money;
        public string Fighters => _fighters;
        public string BuyWeapon => _buyWeapon;
        public string BuyTwoBranches => _buyTwoBranches;
        public string MergeWeapon => _mergeWeapon;
        public string DragAndDropWeapon => _dragAndDropWeapon;
        public string ReturnItems => _returnItems;
        public string GoodLuckHero => _goodLuckHero;
        public string PleseSaveUs => _pleseSaveUs;
    }
}