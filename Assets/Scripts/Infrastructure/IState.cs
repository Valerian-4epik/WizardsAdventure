public interface IState : IExitableState
{
    void Enter();
}

public interface IPayloadedState<TPayload> : IExitableState
{
    void Enter(TPayload payload);
}

public interface IPayloadedState2<TPayload, TPayload1> : IExitableState
{
    void Enter(TPayload payload, TPayload1 payload1);
}

public interface IExitableState // интерфейс сигригатион прирнцип
{
    void Exit();
}
