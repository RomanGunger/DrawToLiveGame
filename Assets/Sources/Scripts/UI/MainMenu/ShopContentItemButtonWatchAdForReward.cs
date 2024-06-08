using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopContentItemButtonWatchAdForReward : ShopContentItemButton
{
    public static Action RewardedAdButtonPressed;

    protected override void OnClickAction()
    {
        base.OnClickAction();

        //RewardedAdButtonPressed?.Invoke();
    }
}
