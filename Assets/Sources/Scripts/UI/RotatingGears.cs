using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotatingGears : MonoBehaviour
{
    [SerializeField] List<Transform> gears;
    [SerializeField] List<Transform> counterGears;

    private void OnEnable()
    {
        RotateGears();
    }

    public void RotateGears()
    {
        if (gears.Count > 0)
        foreach (var item in gears)
        {
            item.DORotate(item.transform.eulerAngles + new Vector3(0, 0, 360), 3f, RotateMode.FastBeyond360)
                    .SetUpdate(true)
                    .SetEase(Ease.OutBounce);
        }

        if(counterGears.Count > 0)
        foreach (var item in counterGears)
        {
            item.DORotate(item.transform.eulerAngles + new Vector3(0, 0, -360), 3f, RotateMode.FastBeyond360)
                    .SetUpdate(true)
                    .SetEase(Ease.OutBounce);
        }
    }
}
