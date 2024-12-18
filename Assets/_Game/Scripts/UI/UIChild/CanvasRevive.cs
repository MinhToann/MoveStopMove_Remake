using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasRevive : UICanvas
{
    [SerializeField] TextMeshProUGUI countdownNumber;
    [SerializeField] Image timeCircle;
    private float initTime;
    private bool isMinusTime = false;
    public override void Setup()
    {
        base.Setup();
        UIManager.Ins.CloseUI<CanvasGameplay>(0.1f);       
        LevelManager.Ins.SetGameState(GameState.Revive);  
    }
    public override void Open()
    {
        base.Open();
        initTime = 5f;
        SetNumberText(initTime);
        Invoke(nameof(DelayCountDown), 1f);
    }
    public void SetNumberText(float number)
    {
        countdownNumber.text = number.ToString();
    }
    public void CountDown()
    {
        if(LevelManager.Ins.GetGameState() == GameState.Revive)
        {
            if (initTime > 0)
            {
                if (isMinusTime)
                {
                    initTime -= 1;
                    SetNumberText(initTime);
                    AudioManager.Ins.PlayCountdownAudio();
                    isMinusTime = false;
                    Invoke(nameof(DelayCountDown), 1f);
                }

            }
            else
            {
                Close(0);
                UIManager.Ins.OpenUI<CanvasLose>();
            }
        }
        
    }    
    public void DelayCountDown()
    {
        isMinusTime = true;      
        CountDown();
    }    

    public void ExitButton()
    {
        Close(0);
        UIManager.Ins.OpenUI<CanvasLose>();
    }    
    private void Update()
    {
        timeCircle.transform.Rotate(0, 0, -6, Space.World);
    }
}
