using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    private void Start()
    {
        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        SetSoundFXVolume(saveFile._soundON);
        SetSoundMusicVolume(saveFile._musicON);

    }

    public void SetSoundFXVolume(bool on)
    {
        float volume = !on ? -80f : 0f;
        audioMixer.SetFloat("SoundFX", volume);
        Debug.Log($"SoundFX {on}");
    }

    public void SetSoundMusicVolume(bool on)
    {
        float volume = !on ? -80f : 0f;
        audioMixer.SetFloat("Music", volume);
        Debug.Log($"Music {on}");
    }
}
