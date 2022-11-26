using TMPro;
using UnityEngine;

namespace RewardSystem
{
    public class RewardCoinsCell : MonoBehaviour
    {
        [SerializeField] private TMP_Text _amountCoin;

        public void FillCell(int value) => _amountCoin.text = value.ToString();
    }
}
