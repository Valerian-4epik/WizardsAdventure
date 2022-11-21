using Infrastructure;
using NodeCanvas.Tasks.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        private const string GAMEBOOTSTRAPPER = "GameBootstrapper";

        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _continueGameButton;
        
        private void Start()
        {
            var gameBootstrapper = GameObject.FindGameObjectWithTag(GAMEBOOTSTRAPPER);
        }
    }
}
