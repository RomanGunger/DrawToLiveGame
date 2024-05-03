using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum Direction
{
    Up,
    Forward,
    Down,
    Backward
}

public class RotationOverTime : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 2f;
    public Direction direction;

    public bool rotateVs = false;

    [SerializeField] Transform cannon;

    Vector3 dir = Vector3.zero;

    void Rotate()
    {
        switch (direction)
        {
            case Direction.Up:
                dir = Vector3.up;
                break;
            case Direction.Forward:
                dir = Vector3.forward;
                break;
            case Direction.Down:
                dir = Vector3.down;
                break;
            case Direction.Backward:
                dir = Vector3.back;
                break;
        }

        if (rotateVs)
            cannon.DOLocalRotate(dir * 180, rotationSpeed).SetLoops(-1, LoopType.Yoyo);
        else
            transform.DOLocalRotate(dir * 360, rotationSpeed, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
    }

    private void Start()
    {
        Rotate();
    }
}
