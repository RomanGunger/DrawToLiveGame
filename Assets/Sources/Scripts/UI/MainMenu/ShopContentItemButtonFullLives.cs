using UnityEngine;

public class ShopContentItemButtonFullLives : ShopContentItemButton
{
    [SerializeField] MenuLivesBox menuLivesBox;
    [SerializeField] MenuCurrencyBox menuCurrencyBox;

    [SerializeField] PopupsDatabase popupsDatabase;
    [SerializeField] Canvas popupParrentCanvas;

    protected override void OnClickAction()
    {
        base.OnClickAction();

        if(saveFile._currency < price)
        {
            XmlManager xmlManager = new XmlManager();
            SaveFile saveFile = xmlManager.Load();

            GameObject outOfLivesPopup = popupsDatabase.GetPopup("outOfCurrencyPopup");
            if (outOfLivesPopup.TryGetComponent<BasePopupWindow>(out BasePopupWindow popup))
            {
                popup.InstantiatePopup(popupParrentCanvas.transform);
            }
            return;
        }

        if (saveFile._currency >= price && saveFile._lives < saveFile._maxLives)
        {
            saveFile._currency -= price;
            saveFile._lives = saveFile._maxLives;

            xmlManager.Save(saveFile);

            menuLivesBox.UpdateLives(saveFile._lives);
            menuCurrencyBox.UpdateCurrency(saveFile._currency);
        }
    }
}
