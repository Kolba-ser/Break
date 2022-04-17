using System;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelsDatabase")]
public sealed class LevelsDatabase : ScriptableObject, ILevelsService
{
    [SerializeField] private LevelDefinition[] levels;

    LevelDefinition[] ILevelsService.Levels => levels;
}

