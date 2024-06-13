using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoScreenUIManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    AsyncOperation sceneLoad;

    private void Start()
    {
        EventListener.AnimationFinished += OnAnimationFinished;
        var animation = FindAnimation(animator, "Logo_Animation");
        animator.SetTrigger("Play");

        sceneLoad = SceneManager.LoadSceneAsync("Loading_Screen", LoadSceneMode.Single);
        sceneLoad.allowSceneActivation = false;

    }

    async void OnAnimationFinished()
    {
        await Task.Delay(500);
        sceneLoad.allowSceneActivation = true;
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
