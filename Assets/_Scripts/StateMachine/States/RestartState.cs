﻿
using UnityEngine;
using UnityEngine.Assertions;

public sealed class RestartState : IState
{
    private IGameService gameService;

    public RestartState(IGameService gameService)
    {
        this.gameService = gameService;
    }

    public void OnEnter()
    {
        EventHolder.Instance.OnLevelChanged.Invoke();
        gameService.Fire(GameTrigger.Restart);
    }

    public void OnExit()
    {

    }
}

