using UnityEngine;

public class StarsHandler : MonoBehaviour
{
    public int Stars { get; private set; }

    [SerializeField] ScoreHandler scoreHandler;
    [SerializeField] Transform currencyItems;

    float currencyItemsCount = 0;

    private void Awake()
    {
        FinishLine.FinishLineReached += HandleStars;
        GetBaseLevelInfo();
    }

    void HandleStars()
    {
        float percentageComplete = 100f * scoreHandler.CurrentScore / currencyItemsCount;

        if (percentageComplete < 30f)
            Stars = 0;
        else if (percentageComplete > 30f && percentageComplete < 50f)
            Stars = 1;
        else if (percentageComplete > 50f && percentageComplete < 75f)
            Stars = 2;
        else if (percentageComplete > 75f)
            Stars = 3;

    }


    void GetBaseLevelInfo()
    {
        foreach (Transform item in currencyItems)
        {
            if (item.TryGetComponent<CurrencyItem>(out CurrencyItem currencyItem))
            {
                currencyItemsCount++;
            }
        }
    }
}
