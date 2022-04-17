
using UnityEngine;

public sealed class ServiceInstaller : MonoSingleton<ServiceInstaller>
{

    [SerializeField] private LevelsDatabase levelsDatabase;

    private IGameService gameService;

    public IGameService GameService => gameService;
    public ILevelsService LevelsService => levelsDatabase;

    protected override void LateAwake()
    {
        gameService = new GameService(LevelsService);
    }


}

