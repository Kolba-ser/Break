using UnityEngine;
using UnityEngine.Assertions;

public sealed class LevelState : IState
{

    private LevelView menuView;
    private RestartView restartView;
    private IGameService gameService;

    public LevelState(IGameService gameService)
    {
        this.gameService = gameService;
    }

    public void OnEnter()
    {
        menuView = Object.FindObjectOfType<LevelView>();
        restartView = Object.FindObjectOfType<RestartView>();
        Assert.IsNotNull(menuView, "Main menu view is not found");
        Assert.IsNotNull(restartView, "Restart view is not found");
        menuView.Play += OnMainMenu;
        restartView.Restart += OnRestart;
    }

    public void OnExit()
    {
        menuView.Play -= OnMainMenu;
        restartView.Restart -= OnRestart;

    }

    private void OnMainMenu()
    {
        gameService.Fire(GameTrigger.MainMenu);
    }
    
    private void OnRestart()
    {
        gameService.Fire(GameTrigger.Restart);
    }
}

