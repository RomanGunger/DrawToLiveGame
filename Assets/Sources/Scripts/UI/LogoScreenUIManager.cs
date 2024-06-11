using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoScreenUIManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    private async void Start()
    {
        var animation = FindAnimation(animator, "Logo_Animation");
        animator.SetTrigger("Play");

        var asyncSceneLoad = SceneManager.LoadSceneAsync("Loading_Screen", LoadSceneMode.Single);
        asyncSceneLoad.allowSceneActivation = false;
        await Task.Delay((int)animation.length * 1000 + 2000);

        asyncSceneLoad.allowSceneActivation = true;
    }

    public AnimationClip FindAnimation(Animator animator, string name)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }

        return null;
    }
}
