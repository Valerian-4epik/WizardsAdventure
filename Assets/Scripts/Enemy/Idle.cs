using UnityEngine;

namespace Enemy
{
    public class Idle : MonoBehaviour
    {
        [SerializeField] private GameObject _aggroZone;
        [SerializeField] private GameObject _attackZone;
        [SerializeField] private Health _health;

        private CanvasGroup _hpBar;
        
        public void SwitchOnStartFight()
        {
            ShowHPBar();
            _aggroZone.SetActive(true);
            _attackZone.SetActive(true);
        }

        private void ShowHPBar()
        {
            _hpBar = gameObject.GetComponentInChildren<CanvasGroup>();
            _hpBar.alpha = 1;
            _health.CheckReadiness();
        }
    }
}