using UnityEngine;

public class ContinueButton : LevelButtonBase
{
    [SerializeField] MenuPanel pauseMenuPanel;

    public override async void OnClickAction()
    {
        await pauseMenuPanel.Close(.3f);
        Time.timeScale = 1;
    }
}
