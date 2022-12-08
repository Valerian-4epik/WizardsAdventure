using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsInfo : MonoBehaviour
{
    [SerializeField] private CanvasGroup _statsPanel;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _asText;

    public void ActivateStatsPanel(int hp, float attackSpeed)
    {
        _statsPanel.alpha = 1;

        if (hp != 0)
        {
            _hpText.gameObject.SetActive(true);
            FillHpText(hp, _hpText);
        }
        else
            _hpText.gameObject.SetActive(false);

        if (attackSpeed != 0)
        {
            _asText.gameObject.SetActive(true);
            FillAsText(attackSpeed, _asText);
        }
        else
            _asText.gameObject.SetActive(false);
    }

    private void FillHpText(int value, TMP_Text text) => text.text = $"HP + {value.ToString()}";
    private void FillAsText(float value, TMP_Text text) => text.text = $"AS + {value.ToString()}";
}
