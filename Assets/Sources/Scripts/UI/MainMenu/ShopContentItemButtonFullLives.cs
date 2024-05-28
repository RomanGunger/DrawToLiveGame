using UnityEngine;

public class ShopContentItemButtonFullLives : ShopContentItemButton
{
    [SerializeField] MenuLivesBox menuLivesBox;

    protected override void OnClickAction()
    {
        base.OnClickAction();

        if (saveFile._currency >= price)
        {
            saveFile._lives = saveFile._maxLives;
            menuLivesBox.UpdateLives();
        }
    }
}
