using UnityEngine;

public class ContinueButton : MenuButtonBase
{
    [SerializeField] MenuPanel pauseMenuPanel;

    protected override async void OnClickAction()
    {
        await pauseMenuPanel.Close(.3f);
        Time.timeScale = 1;
    }
}
