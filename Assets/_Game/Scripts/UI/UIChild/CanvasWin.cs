using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasWin : UICanvas
{
    [SerializeField] TextMeshProUGUI textZoneNumber;
    [SerializeField] TextMeshProUGUI textGainCoin;
    public override void Setup()
    {
        base.Setup();
        UIManager.Ins.CloseUI<CanvasGameplay>(0.1f);
        LevelManager.Ins.SetGameState(GameState.Win);        
        LevelManager.Ins.ChangeViewCinemachine();
        SetZoneText(LevelManager.Ins.GetLevelNumber + 2);
        SetGainCoin(LevelManager.Ins.currentPlayer.TotalGainCoin);
        UIManager.Ins.coinBtn.gameObject.SetActive(false);
    }
    public void NextLevel()
    {
        Close(0);
        UIManager.Ins.OpenUI<CanvasLoadingScene>();
    }
    public void SetZoneText(int number)
    {
        textZoneNumber.text = number.ToString();
    }
    public void SetGainCoin(int coin)
    {
        textGainCoin.text = coin.ToString();
    }
}
