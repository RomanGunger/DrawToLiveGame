using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class SettingsUI : MonoBehaviour
{
    UIDocument uiDocument;
    VisualElement rootElement;
    Button soundToogle;
    Button musicToogle;
    Button closeButton;

    XmlManager xmlManager;
    SaveFile saveFile;

    private void OnEnable()
    {
        xmlManager = new XmlManager();
        saveFile = xmlManager.Load();

        uiDocument = GetComponent<UIDocument>();
        rootElement = uiDocument.rootVisualElement;

        soundToogle = rootElement.Q<Button>("settings-sound-toogle");
        soundToogle.RegisterCallback<ClickEvent>(SoundToogle);
        soundToogle.style.alignSelf = saveFile.settingsSoundToogle ? Align.FlexEnd : Align.FlexStart;

        musicToogle = rootElement.Q<Button>("settings-music-toogle");
        musicToogle.RegisterCallback<ClickEvent>(MusicToogle);
        soundToogle.style.alignSelf = saveFile.settingsmusicToogle ? Align.FlexEnd : Align.FlexStart;

        closeButton = rootElement.Q<Button>("close-button");
        closeButton.RegisterCallback<ClickEvent>(CloseButton);
    }

    void SoundToogle(ClickEvent evt)
    {
        var toogle = soundToogle.style.alignSelf;

        if (toogle == Align.FlexStart)
        {
            soundToogle.style.alignSelf = Align.FlexEnd;
            saveFile.settingsSoundToogle = true;
        }
        else
        {
            soundToogle.style.alignSelf = Align.FlexStart;
            saveFile.settingsSoundToogle = false;
        }

        Debug.Log(saveFile.settingsSoundToogle);
        xmlManager.Save(saveFile);
    }

    void MusicToogle(ClickEvent evt)
    {
        var toogle = musicToogle.style.alignSelf;

        if (toogle == Align.FlexStart)
        {
            musicToogle.style.alignSelf = Align.FlexEnd;
            saveFile.settingsmusicToogle = true;
        }
        else
        {
            musicToogle.style.alignSelf = Align.FlexStart;
            saveFile.settingsmusicToogle = false;
        }

        xmlManager.Save(saveFile);
    }

    void MoveToogle(bool turnedON)
    {

    }

    void CloseButton(ClickEvent evt)
    {
        uiDocument.gameObject.SetActive(false);
    }
}
