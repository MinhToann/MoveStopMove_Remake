using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasGameplay : UICanvas
{
    [SerializeField] TextMeshProUGUI textAlive;
    [field: SerializeField] public Canvas canvas { get; private set; }
    public override void Setup()
    {
        base.Setup();
        LevelManager.Ins.SetGameState(GameState.Gameplay);
        LevelManager.Ins.ChangeViewCinemachine();
        LevelManager.Ins.currentPlayer.currentModel.ActiveLine();
        SetNumberCharacterAlive(LevelManager.Ins.GetTotalCharacter);
        UIManager.Ins.coinBtn.gameObject.SetActive(false);
        
    }
    public void OpenSetting()
    {
        Close(0);
        UIManager.Ins.OpenUI<CanvasSetting>();
    }    
    public void SetNumberCharacterAlive(int number)
    {
        textAlive.text = number.ToString();
    }
}
