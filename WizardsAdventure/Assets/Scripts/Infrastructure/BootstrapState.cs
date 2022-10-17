using System;
using Infrastructure;
using UnityEngine;

public class BootstrapState : IState //начальный стейт с которого все начинается.
{
    private const string Initial = "Initial";
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;

    //бутстрапу нужна сылка на стейт машну чтобы после завершение своего функцианала сообщить машине поехали дальше
    public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
    }

    public void Enter() //задача бутстрапа привести нас в началло и регистрировать сервисы 
    {
        RegisterServices();
        _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
    }

    public void Exit()
    {
    }

    private void EnterLoadLevel() // этот переход в следующий стейт
        => _gameStateMachine.Enter<LoadLevelState, string>("Main");

    private void RegisterServices()
    {
        Debug.Log("Я зарегестрировал сервисы");
        //допустим зарегестрировать импут сервис если он у вас есть
    }
}