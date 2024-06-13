using System;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    public static Action AnimationFinished;

    public void InvokeAnimationFinished()
    {
        AnimationFinished?.Invoke();
    }
}
