using UnityEngine;

public class ShopContentItemButtonFullLives : ShopContentItemButton
{
    [SerializeField] MenuLivesBox menuLivesBox;
    [SerializeField] MenuCurrencyBox menuCurrencyBox;

    protected override void OnClickAction()
    {
        base.OnClickAction();

        if (saveFile._currency >= price && saveFile._lives < saveFile._maxLives)
        {
            saveFile._currency -= price;
            saveFile._lives = saveFile._maxLives;
            menuLivesBox.UpdateLives(saveFile._lives);
            menuCurrencyBox.UpdateCurrency(saveFile._currency);

            xmlManager.Save(saveFile);
        }
    }
}
