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

public enum LeftRight
{
    Left,
    Right
}

public class RotationOverTime : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 20f;
    public Direction direction;
    public LeftRight leftRight;

    public bool rotateVs = false;

    Vector3 dir = Vector3.zero;

    void Update()
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

        transform.Rotate(dir * (rotationSpeed * Time.deltaTime));

        if (rotateVs)
        {
            switch (leftRight)
            {
                case LeftRight.Left:
                    if (transform.rotation.y < -0.7f || transform.rotation.y > 0)
                    {
                        rotationSpeed *= -1;
                    }
                    break;
                case LeftRight.Right:
                    if (transform.rotation.y > 0.7f || transform.rotation.y < 0)
                    {
                        rotationSpeed *= -1;
                    }
                    break;
            }
        }
    }
}
