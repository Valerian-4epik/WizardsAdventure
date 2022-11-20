using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Logic
{
    public class ArmorBar: MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _text;

        private void Start()
        {
            // Debug.Log("Выключаем на старте");
            // if(_slider.value <= 0)
            //     gameObject.SetActive(false);
        }

        public void SetLevelValue(int value) => 
            _text.text = value.ToString();

        public void SetValue(float current, float max)
        {
            if (current <= 0)
            {
                gameObject.SetActive(false);
            }

            _slider.value = current / max;
        }
    }
}