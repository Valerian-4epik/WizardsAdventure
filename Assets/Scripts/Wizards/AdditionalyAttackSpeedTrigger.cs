using Data;
using UnityEngine;

public class AdditionalyAttackSpeedTrigger : MonoBehaviour
{
    private WizardsSpawner _wizardsSpawner;
    private PlayerProgress _playerProgress;
    
    public void SetWizardSpawner(WizardsSpawner wizardsSpawner)
    {
        _wizardsSpawner = wizardsSpawner;
        _playerProgress = _wizardsSpawner.PlayerProgress;
    }

    
}