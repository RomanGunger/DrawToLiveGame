using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonPlay : MenuButtonBase
{
    [SerializeField] MenuLevelsPanel menuLevelsPanel;

    protected override void OnClickAction()
    {
        menuLevelsPanel.gameObject.SetActive(true);
    }
}
