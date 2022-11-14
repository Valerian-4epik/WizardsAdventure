using Infrastructure.Logic;
using Infrastructure.Services;
using Infrastructure.States;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine GameStateMachine; //нужна публична потомучто мы будем к ней обращаться

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)//при создании игры мы создаем стейтМашину
        {
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container); //у машины должен быть сцен лоудер мы его создаем
        }
    }
}