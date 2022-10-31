using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Logic;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.States
{
    public class GameStateMachine : MonoBehaviour
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services) // sceneloader понадобиться в нескольких сценах поэтому мы прокиним его сразу
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services), //добавляем наше первое состояние this - мы передаем ссылку на машину
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, services.Single<IGameFactory>()), //состояние загрузки первого уровня
                [typeof(GameLoopState)] = new GameLoopState(this, sceneLoader, loadingCurtain), 
            };
        }

        //сделаем интерфейс к которому будет удобно обращаться
        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> // TPayload передаваемый параметр, дженериковый параметр
        {
            TState state = ChangeState<TState>();
            state.Enter(payload); //пайловад передаваемый параметр
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit(); //стейт в котором находится машина и выходим из него ?. проверка на null
            //выбирает стейт и отправляет туда машину
            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState; //as TState - это даункаст 
    }   
}