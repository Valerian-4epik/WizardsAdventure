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

    public void HelpHero() => Show(_dialogInfo.HelpHero);
    public void Fighters() => Show(_dialogInfo.Fighters);
    public void BuyWeapon() => Show(_dialogInfo.BuyWeapon);
    public void MergeWeapon() => Show(_dialogInfo.MergeWeapon);
    public void DragAndDropWeapon() => Show(_dialogInfo.DragAndDropWeapon);
    public void ReturnItems() => Show(_dialogInfo.ReturnItems);
    public void GoodLuck() => Show(_dialogInfo.GoodLuckHero);
    public void SaveUs() => Show(_dialogInfo.PleseSaveUs);
}
