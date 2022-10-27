using UnityEngine;

namespace Enemy
{
    public class Idle : MonoBehaviour
    {
        [SerializeField] private GameObject _aggroZone;
        [SerializeField] private GameObject _attackZone;

        public void SwitchOnStartFight()
        {
            _aggroZone.SetActive(true);
            _attackZone.SetActive(true);
        }
    }
}