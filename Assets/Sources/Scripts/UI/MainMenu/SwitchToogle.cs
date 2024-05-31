using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class SwitchToogle : MonoBehaviour
{
    [SerializeField] RectTransform handleRectTransform;
    [SerializeField] Sprite backgroundActiveSprite;
    [SerializeField] Sprite handleActiveSprite;
    [Header("---------------------------------")]
    [SerializeField] Sprite backgroundDefaultSprite;
    [SerializeField] Sprite handleDefaultSprite;
    [Header("---------------------------------")]
    [SerializeField] Image backgroundImage;
    [SerializeField] Image handleImage;

    [SerializeField] SoundMixerManager soundMixerManager;

    public enum SwitchType
    {
        SoundToogle,
        MusicToogle
    }

    Toggle toggle;

    Vector2 handlePos;

    public SwitchType switchType;

    private void Start()
    {
        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        toggle = GetComponent<Toggle>();
        handlePos = handleRectTransform.anchoredPosition;
        toggle.onValueChanged.AddListener(OnSwitch);
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(toggle);
        });

        switch (switchType)
        {
            case SwitchType.SoundToogle:
                if (saveFile._soundON)
                    toggle.isOn = true;
                else
                    toggle.isOn = false;
                break;
            case SwitchType.MusicToogle:
                if (saveFile._musicON)
                    toggle.isOn = true;
                else
                    toggle.isOn = false;
                break;
        }

        handleRectTransform.anchoredPosition =  toggle.isOn ? handlePos : handlePos * -1;
    }

    private void ToggleValueChanged(Toggle change)
    {
        switch (switchType)
        {
            case SwitchType.SoundToogle:
                TurnSound(change.isOn);
                break;
            case SwitchType.MusicToogle:
                TurnMusic(change.isOn);
                break;
        }
    }

    void TurnSound(bool isOn)
    {
        soundMixerManager.SetSoundFXVolume(isOn);

        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        saveFile._soundON = isOn ? true : false;

        xmlManager.Save(saveFile);
    }

    void TurnMusic(bool isOn)
    {
        soundMixerManager.SetSoundMusicVolume(isOn);

        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        saveFile._musicON = isOn ? true : false;

        xmlManager.Save(saveFile);
    }

    void OnSwitch(bool isOn)
    {
        handleRectTransform.DOAnchorPos(isOn ? handlePos : handlePos * -1, 0.2f).SetEase(Ease.InOutBack);
        backgroundImage.sprite = isOn ? backgroundActiveSprite : backgroundDefaultSprite;
        handleImage.sprite = isOn ? handleActiveSprite : handleDefaultSprite;

        switch (switchType)
        {
            case SwitchType.SoundToogle:
                int audioOn = isOn ? 1 : 0;
                PlayerPrefs.SetInt("audioOn", audioOn);
                break;
            case SwitchType.MusicToogle:
                int vibrationOn = isOn ? 1 : 0;
                PlayerPrefs.SetInt("vibrationOn", vibrationOn);
                break;
        }
    }
}
