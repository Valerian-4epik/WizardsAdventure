using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Logic
{
    public class ArmorBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private CanvasGroup _canvasGroup;

        public void SetLevelValue(int value) =>
            _text.text = value.ToString();

        public void SetValue(float current, float max)
        {
            if (max != 0)
            {
                _slider.normalizedValue = current / max;
                _canvasGroup.alpha = 1;
            }
            else
                _slider.value = 0;

            if (_slider.value == 0)
                _canvasGroup.alpha = 0;
        }

        private float GetValue(float volume) =>
            Mathf.Lerp(0f, 1f, volume);
    }
}