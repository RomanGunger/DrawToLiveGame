using UnityEngine;

public class PauseButton : MenuButtonSidePanel
{
    protected override void OnClickAction()
    {
        base.OnClickAction();

        Time.timeScale = 0;
    }
}
