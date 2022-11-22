using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WizardForMoney : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    private WizardsSpawner _wizardsSpawner;
    private int _price;

    public int Price
    {
        get => _price;
        set
        {
            _price = value;
            Show();
        }
    }

    public void SetWizardSpawner(WizardsSpawner wizardsSpawner, int price)
    {
        _wizardsSpawner = wizardsSpawner;
        Price = price;
        Show();
    }

    public void BuyWizard()
    {
        _wizardsSpawner.AddWizard();
    }

    private void Show() => _text.text = _price.ToString();
}

