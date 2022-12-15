using Infrastructure.Services;
using Infrastructure.States;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine GameStateMachine;

        public Game(ICoroutineRunner coroutineRunner, Curtain loadingCurtain)
        {
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container);
        }
    }
}