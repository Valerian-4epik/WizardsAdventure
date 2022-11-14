using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardForMoney : MonoBehaviour
{
    private WizardsSpawner _wizardsSpawner;

    public void SetWizardSpawner(WizardsSpawner wizardsSpawner) =>
        _wizardsSpawner = wizardsSpawner;

    public void BuyWizard() =>
        _wizardsSpawner.AddWizard();
}

