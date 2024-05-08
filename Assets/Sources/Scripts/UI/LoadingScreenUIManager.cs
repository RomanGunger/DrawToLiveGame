using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadingScreenUIManager : MonoBehaviour
{

    UIDocument uiDocument;
    VisualElement rootElement;
    ProgressBar progressBar;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        rootElement = uiDocument.rootVisualElement;

        progressBar = rootElement.Q<ProgressBar>("loading-bar");

        StartCoroutine(LoadScene("Main_Menu"));
    }

    IEnumerator LoadScene(string sceneName)
    {
        var asyncSceneLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (!asyncSceneLoad.isDone)
        {
            progressBar.value = asyncSceneLoad.progress;
            yield return null;
        }
    }
}
