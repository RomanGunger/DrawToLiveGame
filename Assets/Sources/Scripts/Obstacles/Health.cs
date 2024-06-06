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
            if (collectSound != null)
                SoundFXManager.instance.PlaySoundFXClip(collectSound, transform, 1f);
            else
                Debug.LogError("No collectSound assigned: Health");

            var unit = other.gameObject.GetComponent<Unit>();
            var point = other.ClosestPoint(transform.position);

            UnitAdded?.Invoke(new Vector3(point.x, unit.transform.position.y, point.z));
            UnitsList.instance.UnitAdded(1);
            Destroy(gameObject);
        }
    }
}
