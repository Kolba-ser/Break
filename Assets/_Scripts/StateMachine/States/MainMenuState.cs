
using Unity.Assertions;
using UnityEngine;

public sealed class MainMenuState : IState
{

    private MainMenuView menuView;

    private IGameService gameService;
    private ILevelsService levelsService;

    public MainMenuState(IGameService gameService, ILevelsService levelsService)
    {
        this.gameService = gameService;
        this.levelsService = levelsService;
    }

    public void OnEnter()
    {
        menuView = Object.FindObjectOfType<MainMenuView>();
        Assert.IsNotNull(menuView, "Main menu view is not found");
        menuView.Play += OnPlay;
    }

    public void OnExit()
    {
        menuView.Play -= OnPlay;
    }

    private void OnPlay()
    {
        gameService.ActiveLevel = levelsService.Levels[0];
        gameService.Fire(GameTrigger.Play);
    }
}

