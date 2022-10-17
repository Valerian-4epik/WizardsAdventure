using Infrastructure.Logic;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine GameStateMachine; //нужна публична потомучто мы будем к ней обращаться

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)//при создании игры мы создаем стейтМашину
        {
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain); //у машины должен быть сцен лоудер мы его создаем
        }
    }
}