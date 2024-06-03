using UnityEngine;
using DG.Tweening;
using System;

public class Health : MonoBehaviour
{
    public static Action<Vector3> UnitAdded;

    [SerializeField] AudioClip collectSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SoundFXManager.instance.PlaySoundFXClip(collectSound, transform, 1f);

            var unit = other.gameObject.GetComponent<Unit>();
            var point = other.ClosestPoint(transform.position);

            UnitAdded?.Invoke(new Vector3(point.x, unit.transform.position.y, point.z));
            Destroy(gameObject);
        }
    }
}
