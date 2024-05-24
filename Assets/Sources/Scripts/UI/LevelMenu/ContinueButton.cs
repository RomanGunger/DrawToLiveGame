using UnityEngine;

public class ContinueButton : LevelButtonBase
{
    [SerializeField] MenuPanel pauseMenuPanel;

    public override async void OnClickAction()
    {
        Debug.Log("Continue");
        await pauseMenuPanel.Close(1f);
        Time.timeScale = 1;
    }
}
