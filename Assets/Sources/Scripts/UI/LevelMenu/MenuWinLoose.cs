using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MenuWinLoose : MenuPanel
{
    [SerializeField] protected AudioClip panelStartSound;

    public async override Task Open(float durration)
    {
        if (panelStartSound != null)
            SoundFXManager.instance.PlaySoundFXClip(panelStartSound, transform, 1f);
        else
            Debug.LogError("No panelStartSound assigned: MenuPanel");

        await base.Open(durration);
    }
}
