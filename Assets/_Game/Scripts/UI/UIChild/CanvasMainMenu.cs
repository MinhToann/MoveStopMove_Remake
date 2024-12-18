using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public override void Setup()
    {
        base.Setup();
        UIManager.Ins.OpenUI<JoystickControl>();
        LevelManager.Ins.SetGameState(GameState.MainMenu);
        LevelManager.Ins.ChangeViewCinemachine();
        UIManager.Ins.coinBtn.gameObject.SetActive(true);
        
    }
    public override void Open()
    {
        base.Open();
        ChangeAnim(Constant.ANIM_IN);
        LevelManager.Ins.currentPlayer.gameObject.SetActive(true);
        LevelManager.Ins.currentPlayer.ChangeIdleAnim();
    }
    public void PlayGameButton()
    {
        ChangeAnim(Constant.ANIM_OUT);
        Invoke(nameof(DelayGameplay), 0.2f);
    }
       
    public void OpenWeaponShop()
    {
        ChangeAnim(Constant.ANIM_OUT);
        Invoke(nameof(DelayWeaponShop), 0.2f);
    }    
    public void OpenSkinShop()
    {
        ChangeAnim(Constant.ANIM_OUT);
        Invoke(nameof(DelaySkinShop), 0.2f);
    }
    private void DelayGameplay()
    {
        Close(0);
        UIManager.Ins.OpenUI<CanvasGameplay>();
    }
    private void DelayWeaponShop()
    {
        Close(0);
        UIManager.Ins.OpenUI<CanvasWeaponShop>();
    }
    private void DelaySkinShop()
    {
        Close(0);
        UIManager.Ins.OpenUI<CanvasSkinShop>();
    }
}
