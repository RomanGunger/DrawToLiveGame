using System;
using UnityEngine;

public class ShopContentItemButtonFullLives : ShopContentItemButton
{
    [SerializeField] float price;

    [SerializeField] MenuLivesBox menuLivesBox;

    [SerializeField] PopupsDatabase popupsDatabase;
    [SerializeField] Canvas popupParrentCanvas;

    public static Action<int> CurrencyAdded;

    protected override void OnClickAction()
    {
        base.OnClickAction();

        if(saveFile._currency < price)
        {
            GameObject outOfLivesPopup = popupsDatabase.GetPopup("outOfCurrencyPopup");
            if (outOfLivesPopup.TryGetComponent<BasePopupWindow>(out BasePopupWindow popup))
            {
                popup.InstantiatePopup(popupParrentCanvas.transform);
            }
            return;
        }

        if(saveFile._lives >= saveFile._maxLives)
        {
            GameObject outOfLivesPopup = popupsDatabase.GetPopup("fullEnergyPopup");
            if (outOfLivesPopup.TryGetComponent<BasePopupWindow>(out BasePopupWindow popup))
            {
                popup.InstantiatePopup(popupParrentCanvas.transform);
            }
            return;
        }

        if (saveFile._currency >= price && saveFile._lives < saveFile._maxLives)
        {
            saveFile._currency -= (int)price;
            saveFile._lives = saveFile._maxLives;

            xmlManager.Save(saveFile);

            menuLivesBox.UpdateLives(saveFile._lives);
            CurrencyAdded?.Invoke(saveFile._currency);
        }
    }
}
