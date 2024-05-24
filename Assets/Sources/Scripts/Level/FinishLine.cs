using System;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public static Action FinishLineReached;

    [SerializeField] Collider baseCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FinishLineReached?.Invoke();
            baseCollider.enabled = false;
        }
    }
}
