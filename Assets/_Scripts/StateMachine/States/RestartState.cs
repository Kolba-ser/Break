
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

public sealed class RestartState : IState
{
    private IGameService gameService;

    [Inject] private EventHolder eventHolder;

    public RestartState(IGameService gameService)
    {
        this.gameService = gameService;
    }

    public void OnEnter()
    {
        eventHolder.OnLevelChanged.Invoke();
        gameService.Fire(GameTrigger.Restart);
    }

    public void OnExit()
    {

    }
}

