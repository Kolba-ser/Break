
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameService : StateMachine<GameTrigger>, IGameService
{
    public LevelDefinition ActiveLevel { get; set; }

    public GameService(ILevelsService levelsService)
    {
        DefineState(() => new MainMenuState(this, levelsService));
        DefineState(() => new LoadLevelState(this));
        DefineState(() => new LevelState(this));
        DefineState(() => new LoadMainMenuState(this));
        DefineState(() => new RestartState(this));

        DefineStartTransition<MainMenuState>(GameTrigger.MainMenu);
        DefineTransition<MainMenuState, LoadLevelState>(GameTrigger.Play);
        DefineTransition<LoadLevelState, LevelState>(GameTrigger.Play);
        DefineTransition<LevelState, LoadMainMenuState>(GameTrigger.MainMenu);
        DefineTransition<LoadMainMenuState, MainMenuState>(GameTrigger.MainMenu);

        DefineTransition<LevelState, RestartState>(GameTrigger.Restart);
        DefineTransition<RestartState, LoadLevelState>(GameTrigger.Restart);

        Validate();

    }

    private void Validate()
    {
        var activeScene = SceneManager.GetActiveScene();

        if(activeScene.IsValid() && activeScene.name == "MainMenu")
        {
            Fire(GameTrigger.MainMenu);
        }
        else
        {
            Debug.LogError("Unsupported first scene");
        }
    }
}

public enum GameTrigger
{
    Play, 
    MainMenu,
    Restart
}

