using System;
using System.Threading.Tasks;
using UnityEngine;

public class MenuPause : MenuPanel
{
    public static Action PauseEnd;

    public async override Task Close(float durration)
    {
        base.Close(durration);

        PauseEnd?.Invoke();
    }
}
