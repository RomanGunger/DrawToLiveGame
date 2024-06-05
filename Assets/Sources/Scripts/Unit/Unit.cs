using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using TMPro;

public class Unit : MonoBehaviour
{
    [SerializeField] float rearrangeSpeed = 10f;
    [SerializeField] AudioClip explosionSound;

    [Header("Units Count")]
    [SerializeField] TextMeshProUGUI unitsCountText;
    [SerializeField] Canvas unitsOverheadCanvas;

    public int UnitsCount { get; private set; } = 1;

    private float journeyLength;
    Vector3 endMarker;

    private void Start()
    {
        FinishLine.FinishLineReached += OnFinishLineReached;
    }

    public void AddUnitsCount(int unitsCount)
    {
        UnitsCount += unitsCount;

        if (UnitsCount > 0)
        {
            unitsCountText.text = UnitsCount.ToString();
            unitsOverheadCanvas.gameObject.SetActive(true);
        }
        else
        {
            unitsOverheadCanvas.gameObject.SetActive(false);
        }
    }

    public void MinusUnitsCount()
    {
        UnitsCount--;

        if (UnitsCount > 0)
        {
            unitsCountText.text = UnitsCount.ToString();
            unitsOverheadCanvas.gameObject.SetActive(true);
        }
        else
        {
            unitsOverheadCanvas.gameObject.SetActive(false);
        }
    }

    void OnFinishLineReached()
    {
        GetComponent<CapsuleCollider>().enabled = false;
    }

    public async Task Rearrange(Vector3 movePos)
    {
        Vector3 startMarker = transform.localPosition;

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
