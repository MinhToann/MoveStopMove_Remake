using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasShop : UICanvas
{
    public override void Setup()
    {
        base.Setup();
        LevelManager.Ins.SetGameState(GameState.ShoppingView);
        UIManager.Ins.coinBtn.gameObject.SetActive(true);
    }
}
