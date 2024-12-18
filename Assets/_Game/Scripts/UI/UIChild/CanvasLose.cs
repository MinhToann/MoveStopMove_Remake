using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasLose : UICanvas
{
    [SerializeField] Text killerNameText;
    [SerializeField] Text killerNameShadowText;

    [SerializeField] Text rankNumberText;
    [SerializeField] Text rankNumberShadowText;
    public override void Setup()
    {
        base.Setup();
        
        LevelManager.Ins.SetGameState(GameState.Lose);
        SetKillerName(LevelManager.Ins.GetKillerName);
        SetRankNumer(LevelManager.Ins.GetTotalCharacter + 1);
        UIManager.Ins.coinBtn.gameObject.SetActive(false);
    }
    public override void Open()
    {
        base.Open();
        AudioManager.Ins.PlayLoseAudio();
    }
    public void SetKillerName(string killerName)
    {
        killerNameText.text = killerName;
        killerNameShadowText.text = killerName;
    }    

    public void SetRankNumer(int rank)
    {
        rankNumberText.text = rank.ToString();
        rankNumberShadowText.text = rank.ToString();
    }    
    public void ButtonRetry()
    {
        Close(0);
        UIManager.Ins.OpenUI<CanvasLoadingScene>();
    }    
}
