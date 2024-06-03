using System;
using UnityEngine;

public class PauseButton : MenuButtonSidePanel
{
    public static Action PausePressed;

    protected override void OnClickAction()
    {
        base.OnClickAction();

        Time.timeScale = 0;

        PausePressed?.Invoke();
    }
}
