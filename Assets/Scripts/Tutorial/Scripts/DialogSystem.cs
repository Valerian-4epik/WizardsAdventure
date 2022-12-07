using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tutorial.Scripts;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private DialogInfo _dialogInfo;

    private void Show(string value) => _text.text = value;

    public void HelpHero() => Show(_dialogInfo.HelpHero.GetLocalizedString());
    public void Money() => Show(_dialogInfo.Money.GetLocalizedString());
    public void Fighters() => Show(_dialogInfo.Fighters.GetLocalizedString());
    public void BuyWeapon() => Show(_dialogInfo.BuyWeapon.GetLocalizedString());
    public void BuyTwoBranches() => Show(_dialogInfo.BuyTwoBranches.GetLocalizedString());
    public void MergeWeapon() => Show(_dialogInfo.MergeWeapon.GetLocalizedString());
    public void DragAndDropWeapon() => Show(_dialogInfo.DragAndDropWeapon.GetLocalizedString());
    public void ReturnItems() => Show(_dialogInfo.ReturnItems.GetLocalizedString());
    public void GoodLuck() => Show(_dialogInfo.GoodLuckHero.GetLocalizedString());
    public void SaveUs() => Show(_dialogInfo.PleseSaveUs.GetLocalizedString());
}
