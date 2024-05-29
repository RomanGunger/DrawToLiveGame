using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenUIManager : MonoBehaviour
{
    Slider progressBar;

    void Start()
    {
        progressBar = GetComponent<Slider>();
        StartCoroutine(LoadScene("Main_Menu"));
    }

    IEnumerator LoadScene(string sceneName)
    {
        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        var asyncSceneLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (!asyncSceneLoad.isDone)
        {
            progressBar.value = asyncSceneLoad.progress;
            yield return null;
        }
    }
}
