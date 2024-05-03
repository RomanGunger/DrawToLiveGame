using System;
using DG.Tweening;
using UnityEngine;

public class CurrencyItem : MonoBehaviour
{
    public static Action ScoreItemCollected;

    [SerializeField] float floatHight = .6f;
    [SerializeField] float cycleTime = 1.4f;

    private void Start()
    {
        transform.DOLocalMove(transform.localPosition + new Vector3(0, floatHight, 0), cycleTime)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        transform.DORotate(new Vector3(0, 360, 0), cycleTime * 2f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ScoreItemCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
