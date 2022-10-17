using Infrastructure;
using Infrastructure.Logic;
using UnityEngine;

public class LoadLevelState : IPayloadedState<string> //так как интерфейс пайлоадед принимает в себя условие
{
    private const string Initialpointheroesspawner = "InitialPointHeroesSpawner";
    private const string HeroesSpawnerPath = "Prefabs/HeroesSpawner";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _loadingCurtain = loadingCurtain;
    }
    
    public void Enter(string nameScene)
    {
        _loadingCurtain.Show(); //перед загрузкой покажем.
        _sceneLoader.Load(nameScene, OnLoaded);
    }

    public void Exit() => 
        _loadingCurtain.Hide();

    private void OnLoaded()//тут я зягружаю все обьекты и поведения которые должны быть на сцене 
    {
        GameObject initialPoint = GameObject.FindWithTag(Initialpointheroesspawner);
        GameObject heroesSpawner = Instantiate(HeroesSpawnerPath, initialPoint.transform.position);// создаем at и передаем его в метод
        
        //shop
        //spawner
        //cameraFolower
        _stateMachine.Enter<GameLoopState>();
    }

    private static GameObject Instantiate(string path, Vector3 at) //также мне нужно задать точку где создавать обьект
    {
        var prefab = Resources.Load<GameObject>(path); //указываем путь 
        return Object.Instantiate(prefab, at, Quaternion.identity); //а потом инстантиируем
    }

    private static GameObject Instantiate(string path)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab);
    }

}