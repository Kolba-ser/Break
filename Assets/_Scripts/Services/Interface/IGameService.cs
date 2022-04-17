
public interface IGameService
{
    public void Fire(GameTrigger gameTrigger);
    public LevelDefinition ActiveLevel { get; set; }
}

