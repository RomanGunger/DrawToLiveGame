using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class Unit : MonoBehaviour
{
    [SerializeField] AudioClip explosionSound;

    public int UnitsCount { get; private set; } = 1;

    private void Start()
    {
        FinishLine.FinishLineReached += OnFinishLineReached;
    }

    public void AddUnitsCount(int unitsCount)
    {
        UnitsCount += unitsCount;
    }

    public void MinusUnitsCount()
    {
        UnitsCount--;
    }

    void OnFinishLineReached()
    {
        GetComponent<CapsuleCollider>().enabled = false;
    }

    public async Task Rearrange(Vector3 movePos)
    {
        Vector3 startMarker = transform.localPosition;
        Vector3 endMarker = new Vector3(movePos.x, 0, movePos.z);
        float journeyLength = Vector3.Distance(startMarker, endMarker);

        await transform.DOLocalMove(endMarker, .1f * journeyLength).AsyncWaitForCompletion();
    }

    public void DestroyUnit()
    {
        UnitsList.instance.UnitKilled(UnitsCount);

        if (explosionSound != null)
            SoundFXManager.instance.PlaySoundFXClip(explosionSound, transform, 1f);
        else
            Debug.LogError("No explosionSound assigned: Unit");

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        FinishLine.FinishLineReached -= OnFinishLineReached;
    }
}
