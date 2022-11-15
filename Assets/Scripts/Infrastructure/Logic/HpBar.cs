using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Logic
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void SetValue(float current, float max) => 
            _slider.value = current/max;
    }
}