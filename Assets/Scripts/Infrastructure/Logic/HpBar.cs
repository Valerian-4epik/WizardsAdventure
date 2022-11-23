using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Logic
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private CanvasGroup _canvasGroup;

        public void SetValue(float current, float max)
        {
            _slider.value = current / max;
            
            if(_slider.value == 0)
                _canvasGroup.alpha = 0;
        }
    }
}