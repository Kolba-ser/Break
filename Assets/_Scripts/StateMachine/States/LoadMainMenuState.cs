
using Cysharp.Threading.Tasks;
using Unity.Assertions;

public class LoadMainMenuState : LoadLevelBase, IState
{
    private IGameService gameService;

    public LoadMainMenuState(IGameService gameService)
    {
        this.gameService = gameService;
    }

    public void OnEnter()
    {
        LoadMainMenu().Forget();
    }

    private async UniTaskVoid LoadMainMenu()
    {
        await LoadScene("MainMenu");
        gameService.Fire(GameTrigger.MainMenu);
    }

    public void OnExit()
    {

    }
}

