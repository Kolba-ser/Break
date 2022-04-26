using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

public sealed class LoadLevelState : LoadLevelBase, IState
{
    private IGameService gameService;

    public LoadLevelState(IGameService gameService)
    {
        this.gameService = gameService;
    }

    public void OnEnter()
    {
        var activeLevel = gameService.ActiveLevel;
        Assert.IsNotNull(activeLevel, "Active level is not defined");
        LoadLevel(activeLevel).Forget();
    }

    private async UniTaskVoid LoadLevel(LevelDefinition levelDefinition)
    {
        await LoadScene(levelDefinition.Scene);
        gameService.Fire(GameTrigger.Play);
    }

    public void OnExit()
    {
        
    }
}

