using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class Unit : MonoBehaviour
{
    [SerializeField] float rearrangeSpeed = 10f;
    [SerializeField] AudioClip explosionSound;

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

    public async Task Rearrange(Vector3 movePos)
    {
        startMarker = transform.localPosition;

        endMarker = new Vector3(movePos.x, 0, movePos.z);

        journeyLength = Vector3.Distance(startMarker, endMarker);

        await transform.DOLocalMove(endMarker, .1f * journeyLength).AsyncWaitForCompletion();
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
