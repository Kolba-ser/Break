using UnityEngine;
using UnityEngine.Assertions;

public sealed class LevelState : IState
{

    private LevelView menuView;
    private IGameService gameService;

    public LevelState(IGameService gameService)
    {
        this.gameService = gameService;
    }

    public void OnEnter()
    {
        menuView = Object.FindObjectOfType<LevelView>();
        Assert.IsNotNull(menuView, "Main menu view is not found");
        menuView.Play += OnMainMenu;
    }

    public void OnExit()
    {
        menuView.Play -= OnMainMenu;
    }

    private void OnMainMenu()
    {
        gameService.Fire(GameTrigger.MainMenu);
    }
}

