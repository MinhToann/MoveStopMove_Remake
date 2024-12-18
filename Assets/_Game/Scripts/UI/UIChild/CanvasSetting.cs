using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSetting : UICanvas
{
    [SerializeField] Image onSound;
    [SerializeField] Image offSound;
    [SerializeField] Image onVib;
    [SerializeField] Image offVib;
    public override void Setup()
    {
        base.Setup();
        LevelManager.Ins.SetGameState(GameState.Setting);
        //offSound.gameObject.SetActive(false);
        //offVib.gameObject.SetActive(false);
    }
    public override void Open()
    {
        base.Open();
        ChangeAnim(Constant.ANIM_ON);
    }
    public void OpenMainMenu()
    {
        ChangeAnim(Constant.ANIM_OFF);
        Invoke(nameof(DelayMainMenu), 0.5f);
    }    
    public void ContinuePlay()
    {
        ChangeAnim(Constant.ANIM_OFF);
        Invoke(nameof(DelayGamePlay), 0.5f);
    }    
    public void OnSound()
    {
        AudioManager.Ins.TurnOnAudio();
        onSound.gameObject.SetActive(true);
        offSound.gameObject.SetActive(false);
    }    
    public void OffSound()
    {
        AudioManager.Ins.TurnOffAudio();
        offSound.gameObject.SetActive(true);
        onSound.gameObject.SetActive(false);
    }    
    public void OnVib()
    {
        offVib.gameObject.SetActive(false);
        onVib.gameObject.SetActive(true);
    }    
    public void OffVib()
    {
        offVib.gameObject.SetActive(true);
        onVib.gameObject.SetActive(false);
    }    
    public void DelayMainMenu()
    {
        Close(0);
        //LevelManager.Ins.OnDespawnAll();
        LevelManager.Ins.OnInit();
        //UIManager.Ins.OpenUI<CanvasMainMenu>();
    }    
    public void DelayGamePlay()
    {
        Close(0);
        UIManager.Ins.OpenUI<CanvasGameplay>();
    }    
}
