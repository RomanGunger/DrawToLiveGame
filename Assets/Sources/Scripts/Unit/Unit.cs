using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector] public UnitPosition unitPosition;

    [SerializeField] float rearrangeSpeed = 10f;

    private float startTime;
    private float journeyLength;

    Vector3 startMarker;
    Vector3 endMarker;

    bool rearranging = false;


    void Update()
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

        endMarker = new Vector3(movePos.x, 1, movePos.z);

        journeyLength = Vector3.Distance(startMarker, endMarker);
        rearranging = true;
    }
}
