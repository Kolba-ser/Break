﻿using System;
using UnityEngine;

public sealed class LevelMenu : UIMenu
{

    [SerializeField] private Canvas levelMenu;

    public override Type Type => typeof(LevelMenu);

    private void Start()
    {
        InputController.Instance.OnLevelMenu(Show, Hide);
        EventHolder.Instance.OnEndGame.Subscribe(_ =>
        {
            InputController.Instance.OnLevelMenu(Show, Hide, false);
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

