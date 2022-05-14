using System;
using UnityEngine;
using Zenject;

public sealed class LevelMenu : UIMenu
{

    [SerializeField] private Canvas levelMenu;

    [Inject] private EventHolder eventHolder;

    public override Type Type => typeof(LevelMenu);

    protected override void Start()
    {
        inputService.OnLevelMenu(Show, Hide);
        eventHolder.OnEndGame.Subscribe(_ =>
        {
            inputService.OnLevelMenu(Show, Hide, false);
            Show();
        });
    }

    public override void Show()
    {
        levelMenu.enabled = true;
        base.Show();
    }

    public override void Hide()
    {
        levelMenu.enabled = false;
        base.Hide();
    }
}

