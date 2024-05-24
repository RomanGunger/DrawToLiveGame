using UnityEngine;
using UnityEngine.UI;

public abstract class LevelButtonBase : MonoBehaviour
{
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnClickAction);
        button.onClick.AddListener(OnClickAnimation);
    }

    public abstract void OnClickAction();
    public virtual void OnClickAnimation()
    {

    }
}
