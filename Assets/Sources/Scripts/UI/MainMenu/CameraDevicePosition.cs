using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDevicePosition : MonoBehaviour
{
    [SerializeField] Vector3 tabletCameraPosition;

    private void Awake()
    {
        float ratio = Screen.height / Screen.width;

        if (ratio > 1.95f)
        {
        }
        else if (ratio < 1.1f)
        {
            GetComponent<Transform>().position = tabletCameraPosition;
        }
    }
}
