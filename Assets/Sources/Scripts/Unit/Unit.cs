using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] float rearrangeSpeed = 10f;
    [SerializeField] AudioClip explosionSound;

    private float startTime;
    private float journeyLength;

    Vector3 startMarker;
    Vector3 endMarker;

    bool rearranging = false;

    private void Start()
    {
        FinishLine.FinishLineReached += OnFinishLineReached;
    }

    void OnFinishLineReached()
    {
        GetComponent<CapsuleCollider>().enabled = false;
    }

    void FixedUpdate()
    {
        if (rearranging)
        {
            float distCovered = (Time.time - startTime) * rearrangeSpeed;
            float fractionOfJourney = distCovered / journeyLength;

            transform.localPosition = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);

            if (fractionOfJourney >= 1)
            {
                rearranging = false;
            }
        }
    }

    public void Rearrange(Vector3 movePos)
    {
        startTime = Time.time;
        startMarker = transform.localPosition;

        endMarker = new Vector3(movePos.x, 0, movePos.z);

        journeyLength = Vector3.Distance(startMarker, endMarker);
        rearranging = true;
    }

    public void DestroyUnit()
    {
        if (explosionSound != null)
            SoundFXManager.instance.PlaySoundFXClip(explosionSound, transform, 1f);
        else
            Debug.LogError("No collectSound assigned: CorrencyItem");

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        FinishLine.FinishLineReached -= OnFinishLineReached;
    }
}
