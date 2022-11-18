using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Logic
{
    public class ArmorBar: MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void SetValue(float current, float max)
        {
            if (_slider.value <= current / max)
            {
                gameObject.SetActive(false);
            }
            
            _slider.value = current / max;
        }
    }
}